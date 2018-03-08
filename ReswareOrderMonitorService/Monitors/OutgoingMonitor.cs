using System;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.CompletedActionEvents;
using ReswareOrderMonitorService.ReswareActionEvent;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Monitors
{
    internal class OutgoingMonitor : IOutgoingMonitor
    {
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;
        private readonly ReceiveActionEventServiceClient _receiveActionEventServiceClient;
        private readonly IParentClientCompletedActionEventFactory _parentClientCompletedActionEventFactory;

        public OutgoingMonitor() : this (new OrderPlacementServiceClient(), new ReceiveActionEventServiceClient(), ReswareOrderDependencyFactory.Resolve<IParentClientCompletedActionEventFactory>()) { }

        internal OutgoingMonitor(OrderPlacementServiceClient orderPlacementServiceClient, ReceiveActionEventServiceClient receiveActionEventServiceClient, IParentClientCompletedActionEventFactory parentClientCompletedActionEventFactory)
        {
            _orderPlacementServiceClient = orderPlacementServiceClient;
            _receiveActionEventServiceClient = receiveActionEventServiceClient;
            _parentClientCompletedActionEventFactory = parentClientCompletedActionEventFactory;
        }

        public void MonitorOrders()
        {
            try
            {
                var orders = _orderPlacementServiceClient.GetAllOrders();

                if (orders.Length == 0) return;

                orders.ForEach(order =>
                {
                    var actionEvents = _receiveActionEventServiceClient.GetAllActionEvents().Where(ae =>
                        string.Equals(ae.FileNumber, order.FileNumber, StringComparison.CurrentCultureIgnoreCase) &&
                        ae.ActionCompleted && ae.ActionCompletedDateTime != null).ToList();

                    if (actionEvents.Count == 0) return;

                    var clientCompletedActionEventFactory = _parentClientCompletedActionEventFactory.ResolveClientCompletedActionEventFactory(order.ClientId);

                    actionEvents.ForEach(actionEvent =>
                    {
                        var result = clientCompletedActionEventFactory 
                            ?.ResolveCompletedActionEventStatusSenderFactory(actionEvent.ActionEventCode, order.CustomerId, order.FileNumber)
                            ?.ResolveStatusSender(order)
                            ?.SendStatusUpdate();
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

using System;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.CompletedActionEvents;
using ReswareOrderMonitorService.Repositories;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Monitors
{
    internal class OutgoingMonitor : IOutgoingMonitor
    {
        private readonly IOrderPlacementRepository _orderPlacementRepository;
        private readonly IReceiveActionEventRepository _receiveActionEventRepository;
        private readonly IParentClientCompletedActionEventFactory _parentClientCompletedActionEventFactory;

        public OutgoingMonitor() : this (ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>(), ReswareOrderDependencyFactory.Resolve<IReceiveActionEventRepository>(), ReswareOrderDependencyFactory.Resolve<IParentClientCompletedActionEventFactory>()) { }
         
        internal OutgoingMonitor(IOrderPlacementRepository orderPlacementRepository, IReceiveActionEventRepository receiveActionEventServiceClient, IParentClientCompletedActionEventFactory parentClientCompletedActionEventFactory)
        {
            _orderPlacementRepository = orderPlacementRepository;
            _receiveActionEventRepository = receiveActionEventServiceClient;
            _parentClientCompletedActionEventFactory = parentClientCompletedActionEventFactory;
        }

        public void MonitorOrders()
        {
            try
            {
                var orders = _orderPlacementRepository.GetAllOrders();

                if (orders.Length == 0) return;

                orders.ForEach(order =>
                {
                    var actionEvents = _receiveActionEventRepository.GetAllActionEvents().Where(ae =>
                        string.Equals(ae.FileNumber, order.FileNumber, StringComparison.CurrentCultureIgnoreCase) &&
                        ae.ActionCompleted && ae.ActionCompletedDateTime != null).ToList();

                    if (actionEvents.Count == 0) return;

                    var clientCompletedActionEventFactory = _parentClientCompletedActionEventFactory.ResolveClientCompletedActionEventFactory(order.ClientId);

                    actionEvents.ForEach(actionEvent =>
                    {
                        clientCompletedActionEventFactory?.ResolveCompletedActionEventStatusSenderFactory(actionEvent.ActionEventCode, order.CustomerId, order.FileNumber)
                            ?.ResolveStatusSender(order)
                            ?.SendStatusUpdate(order);
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

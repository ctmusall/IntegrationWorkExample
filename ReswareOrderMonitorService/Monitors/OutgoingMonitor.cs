using System;
using System.Diagnostics;
using System.Linq;
using Resware.Data.ActionEvent.Repository;
using Resware.Data.Order.Repository;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.CompletedActionEvents;

namespace ReswareOrderMonitorService.Monitors
{
    internal class OutgoingMonitor : IOutgoingMonitor
    {
        private readonly OrderRepository _orderPlacementRepository;
        private readonly ActionEventRepository _receiveActionEventRepository;
        private readonly IParentClientCompletedActionEventFactory _parentClientCompletedActionEventFactory;

        public OutgoingMonitor() : this (DependencyFactory.Resolve<OrderRepository>(), DependencyFactory.Resolve<ActionEventRepository>(), DependencyFactory.Resolve<IParentClientCompletedActionEventFactory>()) { }
         
        internal OutgoingMonitor(OrderRepository orderPlacementRepository, ActionEventRepository receiveActionEventServiceClient, IParentClientCompletedActionEventFactory parentClientCompletedActionEventFactory)
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

                if (orders.Count == 0) return;

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
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
            }
        }
    }
}

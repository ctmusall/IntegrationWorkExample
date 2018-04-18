using System;
using System.Diagnostics;
using Resware.Core.ActionEvent.Readers.ActionEvents;
using Resware.Data.Order.Repository;
using ReswareOrderMonitorService.Factories.DependencyFactory;

namespace ReswareOrderMonitorService.Monitors.OrderActionEvents
{
    internal class OrderActionEventMonitor 
    {
        private readonly OrderRepository _orderPlacementRepository;
        private readonly ActionEventReader _actionEventReader;

        public OrderActionEventMonitor(): this(DependencyFactory.Resolve<OrderRepository>(), DependencyFactory.Resolve<ActionEventReader>()) { }

        internal OrderActionEventMonitor(OrderRepository orderPlacementRepository, ActionEventReader actionEventReader)
        {
            _orderPlacementRepository = orderPlacementRepository;
            _actionEventReader = actionEventReader;
        }

        public void MonitorOrderActionEvents()
        {
            try
            {
                var orders = _orderPlacementRepository.GetAllOrders();

                if (orders.Count == 0) return;
                
                orders.ForEach(order =>
                {
                     _actionEventReader.CompleteActions(order);
                }); 
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
            }
        }
    }
}

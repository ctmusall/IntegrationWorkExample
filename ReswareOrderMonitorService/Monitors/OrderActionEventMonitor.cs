using System;
using System.Diagnostics;
using Resware.Data.Order.Repository;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Readers;
using Unity.Interception.Utilities;


namespace ReswareOrderMonitorService.Monitors
{
    internal class OrderActionEventMonitor : IOrderActionEventMonitor
    {
        private readonly OrderRepository _orderPlacementRepository;
        private readonly IActionEventReader _actionEventReader;

        public OrderActionEventMonitor(): this(DependencyFactory.Resolve<OrderRepository>(), DependencyFactory.Resolve<IActionEventReader>()) { }

        internal OrderActionEventMonitor(OrderRepository orderPlacementRepository, IActionEventReader actionEventReader)
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

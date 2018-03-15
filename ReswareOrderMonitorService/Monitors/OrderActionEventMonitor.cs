using System;
using System.Diagnostics;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Readers;
using ReswareOrderMonitorService.Repositories;
using Unity.Interception.Utilities;


namespace ReswareOrderMonitorService.Monitors
{
    internal class OrderActionEventMonitor : IOrderActionEventMonitor
    {
        private readonly IOrderPlacementRepository _orderPlacementRepository;
        private readonly IActionEventReader _actionEventReader;

        public OrderActionEventMonitor(): this(ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>(), ReswareOrderDependencyFactory.Resolve<IActionEventReader>()) { }

        internal OrderActionEventMonitor(IOrderPlacementRepository orderPlacementRepository, IActionEventReader actionEventReader)
        {
            _orderPlacementRepository = orderPlacementRepository;
            _actionEventReader = actionEventReader;
        }

        public void MonitorOrderActionEvents()
        {
            try
            {
                var orders = _orderPlacementRepository.GetAllOrders();

                if (orders.Length == 0) return;
                
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

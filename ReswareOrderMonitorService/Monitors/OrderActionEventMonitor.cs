using System;
using System.Diagnostics;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Readers;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;


namespace ReswareOrderMonitorService.Monitors
{
    internal class OrderActionEventMonitor : IOrderActionEventMonitor
    {
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;
        private readonly IActionEventReader _actionEventReader;

        internal OrderActionEventMonitor(): this(ReswareOrderDependencyFactory.Resolve<OrderPlacementServiceClient>(), ReswareOrderDependencyFactory.Resolve<IActionEventReader>()) { }

        internal OrderActionEventMonitor(OrderPlacementServiceClient orderPlacementServiceClient, IActionEventReader actionEventReader)
        {
            _orderPlacementServiceClient = orderPlacementServiceClient;
            _actionEventReader = actionEventReader;
        }

        public void MonitorOrderActionEvents()
        {
            try
            {
                var orders = _orderPlacementServiceClient.GetAllOrders();
                
                orders.ForEach(order =>
                {
                     _actionEventReader.CompleteActions(order);
                }); 
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
            }
        }
    }
}

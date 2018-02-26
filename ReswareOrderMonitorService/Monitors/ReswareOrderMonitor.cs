using System;
using System.Diagnostics;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Readers;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;


namespace ReswareOrderMonitorService.Monitors
{
    internal class ReswareOrderMonitor : IOrderMonitor
    {
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;
        private readonly IActionEventReader _actionEventReader;

        internal ReswareOrderMonitor(): this(ReswareOrderDependencyFactory.Resolve<OrderPlacementServiceClient>(), ReswareOrderDependencyFactory.Resolve<IActionEventReader>()) { }

        internal ReswareOrderMonitor(OrderPlacementServiceClient orderPlacementServiceClient, IActionEventReader actionEventReader)
        {
            _orderPlacementServiceClient = orderPlacementServiceClient;
            _actionEventReader = actionEventReader;
        }

        public void MonitorOrders()
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

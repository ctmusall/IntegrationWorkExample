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
                var orders = _orderPlacementServiceClient.GetAllOrders().Where(order => !order.Processed && order.ProcessedDateTime == null);
                
                orders.ForEach(order =>
                {
                    var result = _actionEventReader.CompleteActions(order);
                    if (!result) return;
                    order.Processed = true;
                    order.ProcessedDateTime = DateTime.Now;
                    _orderPlacementServiceClient.UpdateOrder(order);
                }); 
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
            }
        }
    }
}

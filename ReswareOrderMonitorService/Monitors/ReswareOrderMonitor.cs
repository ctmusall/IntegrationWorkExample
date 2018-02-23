using System;
using System.Diagnostics;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;


namespace ReswareOrderMonitorService.Monitors
{
    internal class ReswareOrderMonitor : IOrderMonitor
    {
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;
        private readonly ActionEventReaderFactory _actionEventFactory;

        internal ReswareOrderMonitor(): this(ReswareOrderDependencyFactory.Resolve<OrderPlacementServiceClient>(), ReswareOrderDependencyFactory.Resolve<ActionEventReaderFactory>()) { }

        internal ReswareOrderMonitor(OrderPlacementServiceClient orderPlacementServiceClient, ActionEventReaderFactory actionEventFactory)
        {
            _orderPlacementServiceClient = orderPlacementServiceClient;
            _actionEventFactory = actionEventFactory;
        }

        public void MonitorOrders()
        {
            try
            {
                // Monitor orders and start processing orders by non-processed
                var orders = _orderPlacementServiceClient.GetAllOrders()
                    .Where(order => !order.Processed && order.ProcessedDateTime == null);
                
                // For each order, check action event table by file number, most non-recent -> recent
                orders.ForEach(order =>
                {
                    var result = _actionEventFactory.ResolveActionReader(order.CustomerId).CompleteAction(order);
                });



            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
            }
        }
    }
}

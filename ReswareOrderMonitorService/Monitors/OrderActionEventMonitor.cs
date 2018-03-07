using System;
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

        public OrderActionEventMonitor(): this(new OrderPlacementServiceClient(), ReswareOrderDependencyFactory.Resolve<IActionEventReader>()) { }

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

                if (orders.Length == 0) return;
                
                orders.ForEach(order =>
                {
                     _actionEventReader.CompleteActions(order);
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

using System;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.OrderStatusSenders;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Monitors
{
    internal class OutgoingMonitor : IOutgoingMonitor
    {
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;
        private readonly IntegrationServiceClient _integrationServiceClient;
        private readonly IClientOrderStatusSenderFactory _clientOrderStatusReaderFactory;

        public OutgoingMonitor() : this (new OrderPlacementServiceClient(), new IntegrationServiceClient(), ReswareOrderDependencyFactory.Resolve<IClientOrderStatusSenderFactory>()) { }

        internal OutgoingMonitor(OrderPlacementServiceClient orderPlacementServiceClient, IntegrationServiceClient integrationServiceClient, IClientOrderStatusSenderFactory clientOrderStatusReaderFactory)
        {
            _orderPlacementServiceClient = orderPlacementServiceClient;
            _integrationServiceClient = integrationServiceClient;
            _clientOrderStatusReaderFactory = clientOrderStatusReaderFactory;
        }

        public void MonitorOrders()
        {
            try
            {
                var orders = _orderPlacementServiceClient.GetAllOrders();
                if (orders.Length == 0) return;

                orders.ForEach(order =>
                {
                    var eClosingOrder = _integrationServiceClient.GetOrder(order.CustomerId, order.FileNumber);

                    if (eClosingOrder.Outcome == OutcomeEnum.Fail || eClosingOrder.Order == null) return;

                    if (string.Equals(order.Status, eClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return;

                    if (string.IsNullOrWhiteSpace(order.Status))
                    {
                        order.Status = eClosingOrder.Order.Status;
                        _orderPlacementServiceClient.UpdateOrder(order);
                        return;
                    }
                    
                    var result = _clientOrderStatusReaderFactory.ResolveClientOrderStatusReaderFactory(order.ClientId)?.ResolveOrderStatusSender(order.Status, eClosingOrder.Order.Status)?.SendStatusUpdate();
                    if (result == null || !result.Value) return;
                    order.Status = eClosingOrder.Order.Status;
                    _orderPlacementServiceClient.UpdateOrder(order);
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

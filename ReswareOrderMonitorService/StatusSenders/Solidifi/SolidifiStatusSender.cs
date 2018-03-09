using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal abstract class SolidifiStatusSender : IStatusSender
    {
        internal GetOrderResult EClosingOrder;
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;


        internal SolidifiStatusSender(GetOrderResult eClosingOrder) : this(new OrderPlacementServiceClient())
        {
            EClosingOrder = eClosingOrder;
        }

        internal SolidifiStatusSender(OrderPlacementServiceClient orderPlacementServiceClient)
        {
            _orderPlacementServiceClient = orderPlacementServiceClient;
        }

        public abstract void SendStatusUpdate(OrderResult order);

        protected internal void UpdateOrder(OrderResult order)
        {
            _orderPlacementServiceClient.UpdateOrder(order);
        }
    }
}

using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal abstract class SolidifiStatusSender : IStatusSender
    {
        private readonly OrderPlacementServiceClient _orderPlacementServiceClient;

        internal SolidifiStatusSender() : this (new OrderPlacementServiceClient()) { }

        internal SolidifiStatusSender(OrderPlacementServiceClient orderPlacementServiceClient)
        {
            _orderPlacementServiceClient = orderPlacementServiceClient;
        }

        public abstract bool SendStatusUpdate(OrderResult order);

        protected internal void UpdateOrder(OrderResult order)
        {
            _orderPlacementServiceClient.UpdateOrder(order);
        }
    }
}

using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal abstract class SolidifiUpdateOrderStatus : IStatusSender
    {
        internal readonly OrderPlacementServiceClient OrderPlacementServiceClient;
        internal readonly string NewStatus;

        internal SolidifiUpdateOrderStatus(string newStatus) : this(new OrderPlacementServiceClient()) { NewStatus = newStatus; }

        internal SolidifiUpdateOrderStatus(OrderPlacementServiceClient orderPlacementServiceClient)
        {
            OrderPlacementServiceClient = orderPlacementServiceClient;
        }

        public abstract void SendStatusUpdate(OrderResult order);
    }
}

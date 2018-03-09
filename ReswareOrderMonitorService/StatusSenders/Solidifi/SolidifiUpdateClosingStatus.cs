using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateClosingStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateClosingStatus(string newStatus) : base(newStatus) { }
        public override void SendStatusUpdate(OrderResult order)
        {
            order.ClosingStatus = NewStatus;
            OrderPlacementServiceClient.UpdateOrder(order);
        }
    }
}

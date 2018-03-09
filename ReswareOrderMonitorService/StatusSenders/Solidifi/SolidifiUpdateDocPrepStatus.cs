using ReswareOrderMonitorService.ReswareOrders;
namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateDocPrepStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateDocPrepStatus(string newStatus) : base(newStatus) { }

        public override void SendStatusUpdate(OrderResult order)
        {
            order.DocPrepStatus = NewStatus;
            OrderPlacementServiceClient.UpdateOrder(order);
        }
    }
}

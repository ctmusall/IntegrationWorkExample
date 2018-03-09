using System;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateTitleOpinionStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateTitleOpinionStatus(string newStatus) : base(newStatus) { }
        public override void SendStatusUpdate(OrderResult order)
        {
            order.TitleOpinionStatus = NewStatus;
            OrderPlacementServiceClient.UpdateOrder(order);
        }
    }
}

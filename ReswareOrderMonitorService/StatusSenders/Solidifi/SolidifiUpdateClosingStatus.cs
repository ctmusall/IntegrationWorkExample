using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateClosingStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateClosingStatus(string newStatus, IOrderPlacementRepository orderPlacementRepository) : base(newStatus, orderPlacementRepository) { }
        public override void SendStatusUpdate(OrderResult order)
        {
            order.ClosingStatus = NewStatus;
            OrderPlacementRepository.UpdateOrder(order);
        }
    }
}

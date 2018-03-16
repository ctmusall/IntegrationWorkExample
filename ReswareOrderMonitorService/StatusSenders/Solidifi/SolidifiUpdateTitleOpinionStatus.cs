using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateTitleOpinionStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateTitleOpinionStatus(string newStatus, IOrderPlacementRepository orderPlacementRepository) : base(newStatus, orderPlacementRepository) { }
        public override void SendStatusUpdate(OrderResult order)
        {
            order.TitleOpinionStatus = NewStatus;
            OrderPlacementRepository.UpdateOrder(order);
        }
    }
}

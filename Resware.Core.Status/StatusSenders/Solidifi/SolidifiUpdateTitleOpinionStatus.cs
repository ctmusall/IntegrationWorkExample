using Resware.Core.Status.StatusSenders.Solidifi;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateTitleOpinionStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateTitleOpinionStatus(string newStatus, OrderRepository orderPlacementRepository) : base(newStatus, orderPlacementRepository) { }
        public override void SendStatusUpdate(Order order)
        {
            order.TitleOpinionStatus = NewStatus;
            OrderPlacementRepository.UpdateOrder(order);
        }
    }
}

using Resware.Data.Order.Repository;
using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateClosingStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateClosingStatus(string newStatus, OrderRepository orderPlacementRepository) : base(newStatus, orderPlacementRepository) { }
        public override void SendStatusUpdate(Order order)
        {
            order.ClosingStatus = NewStatus;
            OrderPlacementRepository.UpdateOrder(order);
        }
    }
}

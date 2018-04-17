using Resware.Core.Status.StatusSenders.Solidifi;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateDocPrepStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateDocPrepStatus(string newStatus, OrderRepository orderPlacementRepository) : base(newStatus, orderPlacementRepository) { }

        public override void SendStatusUpdate(Order order)
        {
            order.DocPrepStatus = NewStatus;
            OrderPlacementRepository.UpdateOrder(order);
        }
    }
}

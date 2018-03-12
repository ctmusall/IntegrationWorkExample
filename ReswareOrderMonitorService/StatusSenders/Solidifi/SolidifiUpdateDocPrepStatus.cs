using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiUpdateDocPrepStatus : SolidifiUpdateOrderStatus
    {
        internal SolidifiUpdateDocPrepStatus(string newStatus, IOrderPlacementRepository orderPlacementRepository) : base(newStatus, orderPlacementRepository) { }

        public override void SendStatusUpdate(OrderResult order)
        {
            order.DocPrepStatus = NewStatus;
            OrderPlacementRepository.UpdateOrder(order);
        }
    }
}

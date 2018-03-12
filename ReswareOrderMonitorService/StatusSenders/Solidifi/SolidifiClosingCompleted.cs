using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiClosingCompleted : SolidifiStatusSender
    {
        internal SolidifiClosingCompleted(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility, IOrderPlacementRepository orderPlacementRepository) : base(eClosingOrder, statusDocumentUtility, orderPlacementRepository) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            reswareOrder.ClosingStatus = eClosingOrder.Order.Status;
        }
    }
}

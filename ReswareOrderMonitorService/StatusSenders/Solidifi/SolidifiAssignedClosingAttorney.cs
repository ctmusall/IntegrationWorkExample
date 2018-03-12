using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedClosingAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedClosingAttorney(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility, IOrderPlacementRepository orderPlacementRepository) : base(eClosingOrder, statusDocumentUtility, orderPlacementRepository) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            reswareOrder.ClosingStatus = eClosingOrder.Order.Status;
        }
    }
}

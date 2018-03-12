using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedTitleOpinionAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedTitleOpinionAttorney(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility, IOrderPlacementRepository orderPlacementRepository) : base(eClosingOrder, statusDocumentUtility, orderPlacementRepository) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            reswareOrder.TitleOpinionStatus = eClosingOrder.Order.Status;
        }
    }
}

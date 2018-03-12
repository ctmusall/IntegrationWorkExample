using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedDocPrepAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedDocPrepAttorney(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility, IOrderPlacementRepository orderPlacementRepository) : base(eClosingOrder, statusDocumentUtility, orderPlacementRepository) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            reswareOrder.DocPrepStatus = eClosingOrder.Order.Status;
        }

        protected internal override bool SendDocumentToResware()
        {
            // TODO - Send document to resware
            return true;
        }
    }
}

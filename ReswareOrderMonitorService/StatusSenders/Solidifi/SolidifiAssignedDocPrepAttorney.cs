using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedDocPrepAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedDocPrepAttorney(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility) : base(eClosingOrder, statusDocumentUtility) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.DocPrepStatus = EClosingOrder.Order.Status;
        }

        protected internal override bool SendDocumentToResware()
        {
            // TODO - Send document to resware
            return true;
        }
    }
}

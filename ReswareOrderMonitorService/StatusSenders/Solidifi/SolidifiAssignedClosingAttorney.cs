using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedClosingAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedClosingAttorney(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility) : base(eClosingOrder, statusDocumentUtility) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.ClosingStatus = EClosingOrder.Order.Status;
        }

        protected internal override bool SendDocumentToResware()
        {
            // TODO - Send document to resware
            return true;
        }
    }
}

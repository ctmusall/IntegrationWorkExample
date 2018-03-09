using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedClosingAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedClosingAttorney(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.ClosingStatus = EClosingOrder.Order.Status;
            OrderPlacementServiceClient.UpdateOrder(reswareOrder);
        }

        protected internal override bool SendDocumentToResware()
        {
            throw new System.NotImplementedException();
        }

        protected internal override void BuildStatusUpdateDocument()
        {
            throw new System.NotImplementedException();
        }
    }
}

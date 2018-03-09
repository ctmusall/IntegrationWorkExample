using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedTitleOpinionAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedTitleOpinionAttorney(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.TitleOpinionStatus = EClosingOrder.Order.Status;
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

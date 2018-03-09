using System;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedDocPrepAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedDocPrepAttorney(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.DocPrepStatus = EClosingOrder.Order.Status;
            OrderPlacementServiceClient.UpdateOrder(reswareOrder);
        }

        protected internal override bool SendDocumentToResware()
        {
            throw new NotImplementedException();
        }

        protected internal override void BuildStatusUpdateDocument(OrderResult reswareOrder)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedTitleOpinionAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedTitleOpinionAttorney(GetOrderResult eClosingOrder, IStatusDocumentUtility statusDocumentUtility) : base(eClosingOrder, statusDocumentUtility) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.TitleOpinionStatus = EClosingOrder.Order.Status;
            OrderPlacementServiceClient.UpdateOrder(reswareOrder);
        }

        protected internal override bool SendDocumentToResware()
        {
            throw new System.NotImplementedException();
        }
    }
}

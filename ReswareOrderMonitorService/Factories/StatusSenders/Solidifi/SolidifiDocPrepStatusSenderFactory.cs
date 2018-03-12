using System;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiDocPrepStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiDocPrepStatusSenderFactory(GetOrderResult order) : base(order)
        {
        }

        public override IStatusSender ResolveStatusSender(OrderResult reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.DocPrepStatus)) return new SolidifiUpdateDocPrepStatus(EClosingOrder.Order.Status);

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            return AssignedClosingAttorney(reswareOrder.DocPrepStatus, EClosingOrder.Order.Status) ? new SolidifiAssignedTitleOpinionAttorney(EClosingOrder, new AssignedAttorneyStatusDocumentUtility(), new OrderPlacementRepository()) : null;
        }
    }
}

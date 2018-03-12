using System;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiTitleOpinionStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiTitleOpinionStatusSenderFactory(GetOrderResult order) : base(order) { }

        public override IStatusSender ResolveStatusSender(OrderResult reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.TitleOpinionStatus)) return new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Order.Status);

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            return AssignedClosingAttorney(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status) ? new SolidifiAssignedTitleOpinionAttorney(EClosingOrder, new AssignedAttorneyStatusDocumentUtility()) : null;
        }
    }
}

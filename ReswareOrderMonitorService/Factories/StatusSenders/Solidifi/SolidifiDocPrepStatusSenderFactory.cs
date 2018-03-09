using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

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

            return AssignedClosingAttorney(reswareOrder.DocPrepStatus, EClosingOrder.Order.Status) ? new SolidifiAssignedTitleOpinionAttorney(EClosingOrder) : null;
        }
    }
}

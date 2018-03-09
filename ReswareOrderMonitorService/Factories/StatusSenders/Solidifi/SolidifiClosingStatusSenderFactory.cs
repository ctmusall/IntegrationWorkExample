using System;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiClosingStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiClosingStatusSenderFactory(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        public override IStatusSender ResolveStatusSender(OrderResult order)
        {
            if (InvalidOrder()) return null;

            if (AssignedClosingAttorney(order.ClosingStatus, EClosingOrder.Order.Status)) return new SolidifiAssignedClosingAttorney(EClosingOrder);

            return ClosingCompleted(EClosingOrder.Order.Status) ? new SolidifiClosingCompleted(EClosingOrder) : null;
        }

        private static bool ClosingCompleted(string currentOrderStatus)
        {
            return !string.IsNullOrWhiteSpace(currentOrderStatus) && string.Equals(currentOrderStatus, EClosingOrderStatusConstants.Closed, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

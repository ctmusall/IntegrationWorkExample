using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.OrderStatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.Factories.OrderStatusSenders
{
    internal class SolidifiOrderStatusSenderFactory : OrderStatusSenderFactory
    {
        public override IStatusSender ResolveOrderStatusSender(string previousOrderStatus, string currentOrderStatus)
        {
            if (string.Equals(previousOrderStatus, EClosingOrderStatusConstants.Pending) && string.Equals(currentOrderStatus, EClosingOrderStatusConstants.Scheduled)) return new SolidifiAssignedAttorney();

            return string.Equals(currentOrderStatus, EClosingOrderStatusConstants.Closed) ? new SolidifiClosingCompleted() : null;
        }
    }
}

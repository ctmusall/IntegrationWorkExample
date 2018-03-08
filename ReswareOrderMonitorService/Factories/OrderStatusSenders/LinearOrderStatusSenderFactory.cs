using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.OrderStatusSenders;
using ReswareOrderMonitorService.StatusSenders.Linear;

namespace ReswareOrderMonitorService.Factories.OrderStatusSenders
{
    internal class LinearOrderStatusSenderFactory : OrderStatusSenderFactory
    {
        public override IStatusSender ResolveOrderStatusSender(string previousOrderStatus, string currentOrderStatus)
        {
            if (string.Equals(previousOrderStatus, EClosingOrderStatusConstants.Pending) && string.Equals(currentOrderStatus, EClosingOrderStatusConstants.Scheduled)) return new LinearAssignedAttorney();

            return string.Equals(currentOrderStatus, EClosingOrderStatusConstants.Closed) ? new LinearClosingCompleted() : null;
        }
    }
}

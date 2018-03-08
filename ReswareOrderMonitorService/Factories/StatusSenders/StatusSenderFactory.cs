using System;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;

namespace ReswareOrderMonitorService.Factories.StatusSenders
{
    internal abstract class StatusSenderFactory : IStatusSenderFactory
    {
        protected internal GetOrderResult Order;

        internal StatusSenderFactory(GetOrderResult order)
        {
            Order = order;
        }

        public abstract IStatusSender ResolveStatusSender(OrderResult order);

        protected internal bool InvalidOrder()
        {
            return Order.Outcome == OutcomeEnum.Fail || Order.Order == null;
        }

        protected internal bool AssignedClosingAttorney(string previousOrderStatus, string currentOrderStatus)
        {
            if (string.IsNullOrWhiteSpace(previousOrderStatus) || string.IsNullOrWhiteSpace(currentOrderStatus)) return false;

            return string.Equals(previousOrderStatus, EClosingOrderStatusConstants.Pending, StringComparison.CurrentCultureIgnoreCase) && string.Equals(Order.Order.Status, EClosingOrderStatusConstants.Scheduled);
        }
    }
}

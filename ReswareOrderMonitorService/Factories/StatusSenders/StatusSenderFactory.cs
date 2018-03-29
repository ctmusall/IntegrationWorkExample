using System;
using Resware.Entities.Orders;
using ReswareCommon;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.StatusSenders;

namespace ReswareOrderMonitorService.Factories.StatusSenders
{
    internal abstract class StatusSenderFactory : IStatusSenderFactory
    {
        protected internal GetOrderResult EClosingOrder;

        internal StatusSenderFactory(GetOrderResult eClosingOrder)
        {
            EClosingOrder = eClosingOrder;
        }

        public abstract IStatusSender ResolveStatusSender(Order order);

        protected internal bool InvalidOrder()
        {
            return EClosingOrder.Outcome == OutcomeEnum.Fail || EClosingOrder.Order == null;
        }

        protected internal bool OrderHasAssignedAttorney(string previousOrderStatus, string currentOrderStatus)
        {
            if (string.IsNullOrWhiteSpace(currentOrderStatus)) return false;

            return string.Equals(previousOrderStatus, OrderStatusConstants.Pending, StringComparison.CurrentCultureIgnoreCase) && string.Equals(EClosingOrder.Order.Status, OrderStatusConstants.Scheduled, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

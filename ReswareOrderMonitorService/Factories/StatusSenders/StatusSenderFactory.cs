using System;
using Resware.Entities.Orders;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.StatusSenders;

namespace ReswareOrderMonitorService.Factories.StatusSenders
{
    internal abstract class StatusSenderFactory : IStatusSenderFactory
    {
        protected internal EClosingOrder EClosingOrder;

        internal StatusSenderFactory(EClosingOrder eClosingOrder)
        {
            EClosingOrder = eClosingOrder;
        }

        public abstract IStatusSender ResolveStatusSender(Order order);

        protected internal bool InvalidOrder()
        {
            return EClosingOrder == null;
        }

        protected internal bool OrderHasAssignedAttorney(string previousOrderStatus, string currentOrderStatus)
        {
            if (string.IsNullOrWhiteSpace(currentOrderStatus)) return false;

            return string.Equals(previousOrderStatus, OrderStatusConstants.Pending, StringComparison.CurrentCultureIgnoreCase) && string.Equals(EClosingOrder.Status, OrderStatusConstants.Scheduled, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

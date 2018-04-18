using System;
using eClosings.Entities.Orders;
using Resware.Core.Status.StatusSenders;
using ReswareCommon.Constants;

namespace Resware.Core.Status.Factories.StatusSender
{
    public abstract class StatusSenderFactory
    {
        protected internal Order EClosingOrder;

        internal StatusSenderFactory() { }

        internal StatusSenderFactory(Order eClosingOrder)
        {
            EClosingOrder = eClosingOrder;
        }

        public abstract IStatusSender ResolveStatusSender(Entities.Orders.Order order);

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

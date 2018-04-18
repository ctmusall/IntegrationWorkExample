using System;
using Resware.Core.Builders.StatusDocument.AssignedAttorney;
using Resware.Core.Builders.StatusDocument.ClosingCompleted;
using Resware.Core.Status.StatusSenders;
using Resware.Data.Order.Repository;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace Resware.Core.Status.Factories.StatusSender.ClosingStatus
{
    public class SolidifiClosingStatusSenderFactory : StatusSenderFactory
    {
        public SolidifiClosingStatusSenderFactory(eClosings.Entities.Orders.Order eClosingOrder) : base(eClosingOrder) { }

        public override IStatusSender ResolveStatusSender(Entities.Orders.Order reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.Equals(OrderStatusConstants.Complete, reswareOrder.ClosingStatus)) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.ClosingStatus)) return new SolidifiUpdateClosingStatus(EClosingOrder.Status, DependencyFactory.DependencyFactory.Resolve<OrderRepository>());

            if (string.Equals(reswareOrder.ClosingStatus, EClosingOrder.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            if (OrderHasAssignedAttorney(reswareOrder.ClosingStatus, EClosingOrder.Status)) return new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateClosingStatus(EClosingOrder.Status, DependencyFactory.DependencyFactory.Resolve<OrderRepository>()));

            return ClosingCompleted(EClosingOrder.Status) ? new SolidifiStatusSender(EClosingOrder, new ClosingCompletedStatusDocumentBuilder(), new SolidifiUpdateClosingStatus(OrderStatusConstants.Complete, DependencyFactory.DependencyFactory.Resolve<OrderRepository>())) : null;
        }

        private static bool ClosingCompleted(string currentOrderStatus)
        {
            return !string.IsNullOrWhiteSpace(currentOrderStatus) && string.Equals(currentOrderStatus, OrderStatusConstants.Closed, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

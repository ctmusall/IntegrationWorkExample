using System;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;
using ReswareOrderMonitorService.StatusDocumentBuilders;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiClosingStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiClosingStatusSenderFactory(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        public override IStatusSender ResolveStatusSender(Order reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.Equals(OrderStatusConstants.Complete, reswareOrder.ClosingStatus)) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.ClosingStatus)) return new SolidifiUpdateClosingStatus(EClosingOrder.Order.Status, DependencyFactory.Resolve<OrderRepository>());

            if (string.Equals(reswareOrder.ClosingStatus, EClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            if (OrderHasAssignedAttorney(reswareOrder.ClosingStatus, EClosingOrder.Order.Status)) return new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateClosingStatus(EClosingOrder.Order.Status, DependencyFactory.Resolve<OrderRepository>()));

            return ClosingCompleted(EClosingOrder.Order.Status) ? new SolidifiStatusSender(EClosingOrder, new ClosingCompletedStatusDocumentBuilder(), new SolidifiUpdateClosingStatus(OrderStatusConstants.Complete, DependencyFactory.Resolve<OrderRepository>())) : null;
        }

        private static bool ClosingCompleted(string currentOrderStatus)
        {
            return !string.IsNullOrWhiteSpace(currentOrderStatus) && string.Equals(currentOrderStatus, OrderStatusConstants.Closed, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

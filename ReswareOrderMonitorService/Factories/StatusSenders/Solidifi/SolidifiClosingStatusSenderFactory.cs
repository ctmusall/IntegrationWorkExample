using System;
using ReswareCommon;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;
using ReswareOrderMonitorService.StatusDocumentBuilders;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiClosingStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiClosingStatusSenderFactory(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        public override IStatusSender ResolveStatusSender(OrderResult reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.ClosingStatus)) return new SolidifiUpdateClosingStatus(EClosingOrder.Order.Status, ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>());

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            if (OrderHasAssignedAttorney(reswareOrder.ClosingStatus, EClosingOrder.Order.Status)) return new SolidifiStatusSender(EClosingOrder, new AssignedClosingAttorneyStatusDocumentBuilder(), new SolidifiUpdateClosingStatus(EClosingOrder.Order.Status, ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>()));

            return ClosingCompleted(EClosingOrder.Order.Status) ? new SolidifiStatusSender(EClosingOrder, new ClosingCompletedStatusDocumentBuilder(), new SolidifiUpdateClosingStatus(EClosingOrder.Order.Status, ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>())) : null;
        }

        private static bool ClosingCompleted(string currentOrderStatus)
        {
            return !string.IsNullOrWhiteSpace(currentOrderStatus) && string.Equals(currentOrderStatus, EClosingOrderStatusConstants.Closed, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

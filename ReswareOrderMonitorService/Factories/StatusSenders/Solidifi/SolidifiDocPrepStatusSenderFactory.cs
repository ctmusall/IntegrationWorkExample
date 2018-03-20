using System;
using ReswareOrderMonitorService.StatusDocumentBuilders;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiDocPrepStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiDocPrepStatusSenderFactory(GetOrderResult order) : base(order) { }

        public override IStatusSender ResolveStatusSender(OrderResult reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.DocPrepStatus)) return new SolidifiUpdateDocPrepStatus(EClosingOrder.Order.Status, ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>());

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            return OrderHasAssignedAttorney(reswareOrder.DocPrepStatus, EClosingOrder.Order.Status) ? new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateClosingStatus(EClosingOrder.Order.Status, ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>())) : null;
        }
    }
}

using System;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.StatusDocumentBuilders;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiDocPrepStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiDocPrepStatusSenderFactory(GetOrderResult order) : base(order) { }

        public override IStatusSender ResolveStatusSender(Order reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.DocPrepStatus)) return new SolidifiUpdateDocPrepStatus(EClosingOrder.Order.Status, DependencyFactory.Resolve<OrderRepository>());

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            return OrderHasAssignedAttorney(reswareOrder.DocPrepStatus, EClosingOrder.Order.Status) ? new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateClosingStatus(EClosingOrder.Order.Status, DependencyFactory.Resolve<OrderRepository>())) : null;
        }
    }
}

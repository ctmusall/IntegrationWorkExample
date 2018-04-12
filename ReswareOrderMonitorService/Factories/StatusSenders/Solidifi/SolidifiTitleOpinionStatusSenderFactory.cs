using System;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.StatusDocumentBuilders;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiTitleOpinionStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiTitleOpinionStatusSenderFactory(GetOrderResult order) : base(order) { }

        public override IStatusSender ResolveStatusSender(Order reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.TitleOpinionStatus)) return new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Order.Status, DependencyFactory.Resolve<OrderRepository>());

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            if (OrderHasAssignedAttorney(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status)) return new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Order.Status, DependencyFactory.Resolve<OrderRepository>()));

            return new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Order.Status, DependencyFactory.Resolve<OrderRepository>());
        }
    }
}

using System;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.StatusDocumentBuilders;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiTitleOpinionStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiTitleOpinionStatusSenderFactory(EClosingOrder order) : base(order) { }

        public override IStatusSender ResolveStatusSender(Order reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.TitleOpinionStatus)) return new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Status, DependencyFactory.Resolve<OrderRepository>());

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            if (OrderHasAssignedAttorney(reswareOrder.TitleOpinionStatus, EClosingOrder.Status)) return new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Status, DependencyFactory.Resolve<OrderRepository>()));

            return new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Status, DependencyFactory.Resolve<OrderRepository>());
        }
    }
}

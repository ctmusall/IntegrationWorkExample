using System;
using Resware.Core.Builders.StatusDocument.AssignedAttorney;
using Resware.Core.Status.StatusSenders;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace Resware.Core.Status.Factories.StatusSender.TitleOpinion
{
    public class SolidifiTitleOpinionStatusSenderFactory : StatusSenderFactory
    {
        public SolidifiTitleOpinionStatusSenderFactory(eClosings.Entities.Orders.Order order) : base(order) { }

        public override IStatusSender ResolveStatusSender(Order reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.TitleOpinionStatus)) return new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Status, DependencyFactory.DependencyFactory.Resolve<OrderRepository>());

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            if (OrderHasAssignedAttorney(reswareOrder.TitleOpinionStatus, EClosingOrder.Status)) return new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Status, DependencyFactory.DependencyFactory.Resolve<OrderRepository>()));

            return new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Status, DependencyFactory.DependencyFactory.Resolve<OrderRepository>());
        }
    }
}

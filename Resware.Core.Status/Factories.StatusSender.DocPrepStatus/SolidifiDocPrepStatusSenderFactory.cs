using System;
using Resware.Core.Builders.StatusDocument.AssignedAttorney;
using Resware.Core.Status.StatusSenders;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace Resware.Core.Status.Factories.StatusSender.DocPrepStatus
{
    public class SolidifiDocPrepStatusSenderFactory : StatusSenderFactory
    {
        public SolidifiDocPrepStatusSenderFactory(eClosings.Entities.Orders.Order order) : base(order) { }

        public override IStatusSender ResolveStatusSender(Order reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.DocPrepStatus)) return new SolidifiUpdateDocPrepStatus(EClosingOrder.Status, DependencyFactory.DependencyFactory.Resolve<OrderRepository>());

            if (string.Equals(reswareOrder.DocPrepStatus, EClosingOrder.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            if (OrderHasAssignedAttorney(reswareOrder.DocPrepStatus, EClosingOrder.Status)) return new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateDocPrepStatus(EClosingOrder.Status, DependencyFactory.DependencyFactory.Resolve<OrderRepository>()));

            return new SolidifiUpdateDocPrepStatus(EClosingOrder.Status, DependencyFactory.DependencyFactory.Resolve<OrderRepository>());
        }
    }
}

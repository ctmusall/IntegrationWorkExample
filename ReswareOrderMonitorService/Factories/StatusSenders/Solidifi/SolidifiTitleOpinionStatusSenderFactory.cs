﻿using System;
using ReswareOrderMonitorService.StatusDocumentBuilders;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.Factories.StatusSenders.Solidifi
{
    internal class SolidifiTitleOpinionStatusSenderFactory : StatusSenderFactory
    {
        internal SolidifiTitleOpinionStatusSenderFactory(GetOrderResult order) : base(order) { }

        public override IStatusSender ResolveStatusSender(OrderResult reswareOrder)
        {
            if (InvalidOrder()) return null;

            if (string.IsNullOrWhiteSpace(reswareOrder.TitleOpinionStatus)) return new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Order.Status, ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>());

            if (string.Equals(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status, StringComparison.CurrentCultureIgnoreCase)) return null;

            return AssignedAttorney(reswareOrder.TitleOpinionStatus, EClosingOrder.Order.Status) ? new SolidifiStatusSender(EClosingOrder, new AssignedAttorneyStatusDocumentBuilder(), new SolidifiUpdateTitleOpinionStatus(EClosingOrder.Order.Status, ReswareOrderDependencyFactory.Resolve<IOrderPlacementRepository>())) : null;
        }
    }
}

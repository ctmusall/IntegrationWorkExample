﻿using System;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.ReswareSigning;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Solidifi
{
    internal class SolidifiRequestClosing : RequestOrder
    {
        private const string CustomerContact = "TEAM CLOSINGS";
        private const string DocsToAttorney = "eDoc";

        internal SolidifiRequestClosing(IServiceUtility orderServiceUtility) : base(orderServiceUtility) { }

        internal override RequestMessage BuildRequestMessage(OrderResult order, SigningServiceResult signing)
        {
            return new RequestMessage
            {
                OrderId = order.FileNumber,
                CustomerId = order.CustomerId,
                CustomerContact = CustomerContact,
                LenderName = order.LenderName,
                Product = order.Product,
                CustomerProduct = order.CustomerProduct,
                FileNumber = order.FileNumber,
                OrderRequestedDate = DateTime.Now.ToShortDateString(),
                OrderRequestedTime = DateTime.Now.ToShortTimeString(),
                DocsToAttorney = DocsToAttorney,
                ClosingDate = signing.ClosingDateTime?.ToShortDateString() ?? DateTime.Now.ToShortDateString(),
                ClosingTime = signing.ClosingDateTime?.ToShortTimeString() ?? DateTime.Now.ToShortTimeString(),
                ClosingAddress1 = signing.ClosingAddress,
                ClosingCity = signing.ClosingCity,
                ClosingState = signing.ClosingState,
                ClosingZipCode = signing.ClosingZip,
                ClosingCounty = signing.ClosingCounty
            };
        }
    }
}
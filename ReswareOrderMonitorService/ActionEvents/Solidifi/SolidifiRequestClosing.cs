using System;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Solidifi
{
    internal class SolidifiRequestClosing : RequestOrder
    {
        private const string CustomerContact = "TEAM CLOSINGS";
        private const string DocsToAttorney = "eDoc";

        internal SolidifiRequestClosing(SigningRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient, IServiceUtility orderServiceUtility) : base(receiveSigningServiceRepository, mirthServiceClient,orderServiceUtility) { }

        internal override RequestMessage BuildRequestMessage(Order order, Signing signing)
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
                ClosingDate = signing.ClosingDateTime?.ToShortDateString() ?? order.ClosingDateTime?.ToShortDateString()?? DateTime.Now.ToShortDateString(),
                ClosingTime = signing.ClosingDateTime?.ToShortTimeString() ?? order.ClosingDateTime?.ToShortTimeString() ?? DateTime.Now.ToShortTimeString(),
                ClosingAddress1 = signing.ClosingAddress,
                ClosingCity = signing.ClosingCity,
                ClosingState = signing.ClosingState,
                ClosingZipCode = signing.ClosingZip,
                ClosingCounty = signing.ClosingCounty
            };
        }
    }
}
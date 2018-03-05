using System;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.ReswareSigning;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearRequestDocPrep : RequestOrder
    {
        private const string CustomerContact = "DOC DEED";

        public LinearRequestDocPrep(IOrderServiceUtility orderServiceUtility) : base(orderServiceUtility)
        {
        }

        internal override RequestMessage BuildRequestMessage(OrderResult order, SigningServiceResult signing)
        {
            return new RequestMessage
            {
                OrderId = $"{order.FileNumber}-D",
                CustomerId = order.CustomerId,
                CustomerContact = CustomerContact,
                LenderName = order.LenderName,
                Product = order.Product,
                CustomerProduct = order.CustomerProduct,
                FileNumber = order.FileNumber,
                OrderRequestedDate = DateTime.Now.ToShortDateString(),
                OrderRequestedTime = DateTime.Now.ToShortTimeString(),
                ClosingAddress1 = signing.ClosingAddress,
                ClosingCity = signing.ClosingCity,
                ClosingState = signing.ClosingState,
                ClosingZipCode = signing.ClosingZip,
                ClosingCounty = signing.ClosingCounty
            };
        }
    }
}

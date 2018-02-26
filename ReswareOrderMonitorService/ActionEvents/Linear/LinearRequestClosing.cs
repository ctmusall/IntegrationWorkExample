using System;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearRequestClosing : RequestClosing
    {
        internal override bool PerformAction(OrderResult order)
        {
            var linearClosingOrderMessage = new RequestClosingMessage
            {
                OrderId = order.FileNumber,
                CustomerId = order.CustomerId,
                CustomerContact = order.CustomerContact,
                LenderName = order.LenderName,
                Product = order.Product,
                FileNumber = order.FileNumber,
                OrderRequestedDate = DateTime.Now.ToShortDateString(),
                OrderRequestedTime = DateTime.Now.ToShortTimeString(),
            };

            // Retrieve borrower/sellers from buyers and sellers
            AssignBorrowerInformation(linearClosingOrderMessage, order.BuyersAndSellers);

            // Retrieve signing info for closing date/time



            // Build order closing details XML message
            // Match XML formatting on mirth
            // Send XML message to mirth channel 'Linear Closing'
            throw new InvalidOperationException();
        }

        internal override bool SendUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}

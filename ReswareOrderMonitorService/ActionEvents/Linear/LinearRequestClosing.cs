using System;
using ReswareOrderMonitorService.Common;
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
                DocsToAttorney = order.DeliveryMethod
            };

            AssignBorrowerInformation(linearClosingOrderMessage, order.BuyersAndSellers);

            AssignClosingInformation(linearClosingOrderMessage, order.FileNumber);

            AssignServices(linearClosingOrderMessage);

            return SendUpdate(linearClosingOrderMessage);
        }

        internal override void AssignServices(RequestClosingMessage requestClosingMessage)
        {
            // TODO - Based on product/state -- waiting for requirements
        }

        internal override bool SendUpdate(RequestClosingMessage requestClosingMessage)
        {
            return MirthServiceClient.SendMessageToMirth(ModelSerializer.SerializeXml(requestClosingMessage), 3412, "10.250.161.135");
        }
    }
}

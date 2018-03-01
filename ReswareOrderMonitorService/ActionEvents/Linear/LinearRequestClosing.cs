using System;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Properties;
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
                CustomerProduct = order.CustomerProduct,
                FileNumber = order.FileNumber,
                OrderRequestedDate = DateTime.Now.ToShortDateString(),
                OrderRequestedTime = DateTime.Now.ToShortTimeString(),
                DocsToAttorney = order.DeliveryMethod
            };

            AssignBorrowerInformation(linearClosingOrderMessage, order.BuyersAndSellers);

            AssignClosingInformation(linearClosingOrderMessage, order.FileNumber);

            ClosingServiceUtility.AssignServices(linearClosingOrderMessage);

            return MirthServiceClient.SendMessageToMirth(ModelSerializer.SerializeXml(linearClosingOrderMessage), Settings.Default.MirthLinearClosingPort, Settings.Default.MirthIPAddress);
        }
    }
}

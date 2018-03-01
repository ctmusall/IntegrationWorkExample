using System;
using System.Linq;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Properties;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearRequestClosing : RequestClosing
    {
        private const string CustomerContact = "TEAM CLOSINGS";
        private const string DocsToAttorney = "eDoc";

        internal LinearRequestClosing(IOrderServiceUtility orderServiceUtility) : base(orderServiceUtility) { }

        internal override bool PerformAction(OrderResult order)
        {
            var signing = SigningServiceClient.GetAllSignings().FirstOrDefault(s => string.Equals(s.FileNumber, order.FileNumber, StringComparison.CurrentCultureIgnoreCase));

            if (signing == null) return false;

            var linearClosingOrderMessage = new RequestClosingMessage
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
                DocsToAttorney = DocsToAttorney
            };

            AssignClosingInformation(linearClosingOrderMessage, signing);

            AssignBorrowerInformation(linearClosingOrderMessage, order.BuyersAndSellers);

            OrderServiceUtility.AssignServices(linearClosingOrderMessage);

            return MirthServiceClient.SendMessageToMirth(ModelSerializer.SerializeXml(linearClosingOrderMessage), Settings.Default.MirthLinearClosingPort, Settings.Default.MirthIPAddress);
        }
    }
}

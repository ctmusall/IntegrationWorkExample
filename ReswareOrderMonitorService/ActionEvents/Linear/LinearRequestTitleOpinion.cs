using System;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Properties;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearRequestTitleOpinion : RequestTitleOpinion
    {
        private const string CustomerContact = "KRISTEN MILLER";

        internal override bool PerformAction(OrderResult order)
        {
            var requestMessage = new RequestMessage
            {
                OrderId = $"{order.FileNumber}-T",
                CustomerId = order.CustomerId,
                CustomerContact = CustomerContact,
                LenderName = order.LenderName,
                Product = order.Product,
                FileNumber = order.FileNumber,
                OrderRequestedDate = DateTime.Now.ToShortDateString(),
                OrderRequestedTime = DateTime.Now.ToShortTimeString(),
            };

            // TODO - Closing information 

            // TODO - Services

            // TODO - Borrower information

            return MirthServiceClient.SendMessageToMirth(ModelSerializer.SerializeXml(requestMessage), Settings.Default.MirthLinearTitleOpinionPort, Settings.Default.MirthIPAddress);
        }
    }
}

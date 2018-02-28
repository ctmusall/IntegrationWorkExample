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
                CustomerProduct = order.CustomerProduct,
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
            if (!string.Equals(requestClosingMessage.ClosingState, requestClosingMessage.BorrowerState, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Notes += $"Did not apply any services because Closing state {requestClosingMessage.ClosingState} is not equal to borrower state {requestClosingMessage.BorrowerState}.";
                return;
            }

            switch (requestClosingMessage.ClosingState)
            {
                // TODO - Move to constants
                case "DE":
                    DetermineDelawareServices(requestClosingMessage);
                    return;
                case "GA":
                case "MA":
                case "NC":
                case "NY":
                case "VT":
                case "WV":
                default:
                    return;
            }
        }

        internal override bool SendUpdate(RequestClosingMessage requestClosingMessage)
        {
            // TODO - move to settings
            return MirthServiceClient.SendMessageToMirth(ModelSerializer.SerializeXml(requestClosingMessage), 3412, "10.250.161.135");
        }

        // TODO - Move to external utility?
        private void DetermineDelawareServices(RequestClosingMessage requestClosingMessage)
        {
            switch (requestClosingMessage.Product)
            {
                // TODO - Move to constants
                case "Refinance":
                    requestClosingMessage.Service1 = "Attorney Assisted Closing";
                    requestClosingMessage.Service2 = "eDocs";
                    requestClosingMessage.Service3 = "Disbursement";
                    return;
                case "Investment":
                    requestClosingMessage.Service1 = "Attorney Assisted Closing";
                    requestClosingMessage.Service2 = "eDocs";
                    requestClosingMessage.Service3 = "Disbursement";
                    requestClosingMessage.Service4 = "Attorney Provided Faxed Docs";
                    return;
                case "Purchase":

                default:
                    return;
            }
        }
    }
}

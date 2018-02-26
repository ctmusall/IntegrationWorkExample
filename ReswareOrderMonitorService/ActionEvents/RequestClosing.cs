using System;
using System.Collections.Generic;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.ReswareSigning;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class RequestClosing : ActionEvent
    {
        private readonly ReceiveSigningServiceClient _signingServiceClient;

        protected internal RequestClosing() : this(ReswareOrderDependencyFactory.Resolve<ReceiveSigningServiceClient>()) { }

        protected internal RequestClosing(ReceiveSigningServiceClient signingServiceClient)
        {
            _signingServiceClient = signingServiceClient;
        }

        protected internal SigningServiceResult RetrieveOrderSigningData(string fileNumber)
        {
            return _signingServiceClient.GetAllSignings().FirstOrDefault(s => string.Equals(s.FileNumber, fileNumber, StringComparison.CurrentCultureIgnoreCase));
        }

        protected internal static void AssignBorrowerInformation(RequestClosingMessage requestClosingMessage, IEnumerable<BuyerSellerResult> buyerSellerResults)
        {
            var buyers = buyerSellerResults.Where(bs => bs.Type == BuyerSellerEnum.Buyer);

            // How to handle married borrowers?





            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.ReswareSigning;
using Unity.Injection;
using Unity.Resolution;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class RequestClosing : ActionEvent
    {
        private readonly ReceiveSigningServiceClient _signingServiceClient;
        private readonly MirthServiceClient _mirthServiceClient;

        protected internal RequestClosing() : this(ReswareOrderDependencyFactory.Resolve<ReceiveSigningServiceClient>(), ReswareOrderDependencyFactory.Resolve<MirthServiceClient>()) { }

        protected internal RequestClosing(ReceiveSigningServiceClient signingServiceClient, MirthServiceClient mirthServiceClient)
        {
            _signingServiceClient = signingServiceClient;
            _mirthServiceClient = mirthServiceClient;
        }

        protected internal void AssignClosingInformation(RequestClosingMessage requestClosingMessage, string fileNumber)
        {
            var signing = _signingServiceClient.GetAllSignings().OrderByDescending(s => s.CreatedDateTime).FirstOrDefault(s => string.Equals(s.FileNumber, fileNumber, StringComparison.CurrentCultureIgnoreCase));

            if (signing == null) return;

            if (signing.ClosingDateTime != null)
            {
                requestClosingMessage.ClosingDate = signing.ClosingDateTime.Value.ToShortDateString();
                requestClosingMessage.ClosingTime = signing.ClosingDateTime.Value.ToShortTimeString();
            }

            requestClosingMessage.ClosingAddress1 = signing.ClosingAddress;
            requestClosingMessage.ClosingCity = signing.ClosingCity;
            requestClosingMessage.ClosingState = signing.ClosingState;
            requestClosingMessage.ClosingZipCode = signing.ClosingZip;
            requestClosingMessage.ClosingCounty = signing.ClosingCounty;
        }

        protected internal static void AssignBorrowerInformation(RequestClosingMessage requestClosingMessage, ICollection<BuyerSellerResult> buyerSellerResults)
        {
            var borrower = buyerSellerResults.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && !b.Spouse);

            if (borrower == null) return;

            requestClosingMessage.BorrowerFirstName = borrower.FirstName;
            requestClosingMessage.BorrowerLastName = borrower.LastName;
            requestClosingMessage.BorrowerMiddleName = borrower.MiddleName;
            requestClosingMessage.BorrowerSuffix = borrower.Suffix;
            requestClosingMessage.BorrowerPhone1 = borrower.Phone;
            requestClosingMessage.BorrowerEmail = borrower.Email;

            var coBorrower = buyerSellerResults.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && b.Spouse);

            if (coBorrower == null) return;

            requestClosingMessage.CoBorrowerFirstName = coBorrower.FirstName;
            requestClosingMessage.CoBorrowerLastName = coBorrower.LastName;
            requestClosingMessage.CoBorrowerMiddleName = coBorrower.MiddleName;
            requestClosingMessage.CoBorrowerSuffix = coBorrower.Suffix;
            requestClosingMessage.BorrowerPhone2 = coBorrower.Phone;

            var address = borrower.Address.FirstOrDefault(a => a.BuyerSellerId == borrower.Id);

            if (address == null) return;

            requestClosingMessage.BorrowerAddress1 = address.AddressStreetInfo;
            requestClosingMessage.BorrowerCity = address.City;
            requestClosingMessage.BorrowerState = address.State;
            requestClosingMessage.BorrowerZipCode = address.Zip;
        }

        internal abstract void AssignServices(RequestClosingMessage requestClosingMessage);

        internal virtual bool SendUpdate(RequestClosingMessage requestClosingMessage)
        {
            return _mirthServiceClient.SendMessageToMirth(ModelSerializer.SerializeXml(requestClosingMessage));
        }
    }
}

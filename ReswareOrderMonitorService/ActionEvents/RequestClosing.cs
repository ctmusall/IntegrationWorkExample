using System;
using System.Collections.Generic;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.ReswareSigning;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class RequestClosing : ActionEvent
    {
        private readonly IReceiveSigningService _signingServiceClient;
        protected internal readonly IMirthServiceClient MirthServiceClient;

        protected internal RequestClosing() : this(ReswareOrderDependencyFactory.Resolve<IReceiveSigningService>(), ReswareOrderDependencyFactory.Resolve<IMirthServiceClient>()) { }

        protected internal RequestClosing(IReceiveSigningService signingServiceClient, IMirthServiceClient mirthServiceClient)
        {
            _signingServiceClient = signingServiceClient;
            MirthServiceClient = mirthServiceClient;
        }

        internal virtual void AssignClosingInformation(RequestClosingMessage requestClosingMessage, string fileNumber)
        {
            var signing = _signingServiceClient.GetAllSignings().FirstOrDefault(s => string.Equals(s.FileNumber, fileNumber, StringComparison.CurrentCultureIgnoreCase));

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

        internal virtual void AssignBorrowerInformation(RequestClosingMessage requestClosingMessage, ICollection<BuyerSellerResult> buyerSellerResults)
        {
            var borrower = buyerSellerResults.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && !b.Spouse);

            if (borrower == null) return;

            requestClosingMessage.BorrowerFirstName = string.IsNullOrWhiteSpace(borrower.EntityName) ? borrower.FirstName : borrower.EntityName;
            requestClosingMessage.BorrowerLastName = borrower.LastName;
            requestClosingMessage.BorrowerMiddleName = borrower.MiddleName;
            requestClosingMessage.BorrowerSuffix = borrower.Suffix;
            requestClosingMessage.BorrowerPhone1 = borrower.Phone;
            requestClosingMessage.BorrowerEmail = borrower.Email;

            var coBorrower = buyerSellerResults.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && b.Spouse);

            if (coBorrower != null)
            {
                requestClosingMessage.CoBorrowerFirstName = string.IsNullOrWhiteSpace(coBorrower.EntityName) ? coBorrower.FirstName : coBorrower.EntityName;
                requestClosingMessage.CoBorrowerLastName = coBorrower.LastName;
                requestClosingMessage.CoBorrowerMiddleName = coBorrower.MiddleName;
                requestClosingMessage.CoBorrowerSuffix = coBorrower.Suffix;
                requestClosingMessage.BorrowerPhone2 = coBorrower.Phone;
            }

            var address = borrower.Address.FirstOrDefault(a => a.BuyerSellerId == borrower.Id);

            if (address == null) return;

            requestClosingMessage.BorrowerAddress1 = address.AddressStreetInfo;
            requestClosingMessage.BorrowerCity = address.City;
            requestClosingMessage.BorrowerState = address.State;
            requestClosingMessage.BorrowerZipCode = address.Zip;
        }

        internal abstract void AssignServices(RequestClosingMessage requestClosingMessage);

        internal abstract bool SendUpdate(RequestClosingMessage requestClosingMessage);
    }
}

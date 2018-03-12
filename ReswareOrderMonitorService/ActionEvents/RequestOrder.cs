using System;
using System.Collections.Generic;
using System.Linq;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Properties;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.ReswareSigning;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class RequestOrder : ActionEvent
    {
        private readonly IReceiveSigningServiceRepository _receiveSigningServiceRepository;
        private readonly IMirthServiceClient _mirthServiceClient;
        private readonly IServiceUtility _orderServiceUtility;

        internal RequestOrder(IReceiveSigningServiceRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient, IServiceUtility orderServiceUtility)
        {
            _receiveSigningServiceRepository = receiveSigningServiceRepository;
            _mirthServiceClient = mirthServiceClient;
            _orderServiceUtility = orderServiceUtility;
        }

        internal abstract RequestMessage BuildRequestMessage(OrderResult order, SigningServiceResult signing);

        internal bool SendRequestMessage(RequestMessage requestMessage)
        {
            return _mirthServiceClient.SendMessageToMirth(ModelSerializer.SerializeXml(requestMessage), Settings.Default.MirthSolidifiRequestPort, Settings.Default.MirthIPAddress);
        }

        internal override bool PerformAction(OrderResult order)
        {
            var signing = _receiveSigningServiceRepository.GetAllSignings().OrderByDescending(s => s.CreatedDateTime).FirstOrDefault(s => string.Equals(s.FileNumber, order.FileNumber, StringComparison.CurrentCultureIgnoreCase));

            if (signing == null) return false;

            var requestMessage = BuildRequestMessage(order, signing);

            AssignBorrowerInformation(requestMessage, order.BuyersAndSellers);

            _orderServiceUtility.AssignServices(requestMessage);

            return SendRequestMessage(requestMessage);
        }

        private static void AssignBorrowerInformation(RequestMessage requestClosingMessage, ICollection<BuyerSellerResult> buyerSellerResults)
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
            requestClosingMessage.BorrowerCounty = address.County;
        }
    }
}

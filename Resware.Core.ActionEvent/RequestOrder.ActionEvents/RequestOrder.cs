﻿using System;
using System.Collections.Generic;
using System.Linq;
using eClosings.Mirth.Clients;
using eClosings.Mirth.Messages;
using Resware.Core.Services.Utilities.ServiceUtilities;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Orders.BuyerSellers;
using Resware.Entities.Signings;
using ReswareCommon;
using ReswareCommon.Enums;

namespace Resware.Core.ActionEvent.RequestOrder.ActionEvents
{
    internal abstract class RequestOrder : ActionEvent.ActionEvents.ActionEvent
    {
        private readonly SigningRepository _receiveSigningServiceRepository;
        private readonly IMirthServiceClient _mirthServiceClient;
        private readonly ServiceUtility _orderServiceUtility;

        internal RequestOrder(SigningRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient, ServiceUtility orderServiceUtility)
        {
            _receiveSigningServiceRepository = receiveSigningServiceRepository;
            _mirthServiceClient = mirthServiceClient;
            _orderServiceUtility = orderServiceUtility;
        }

        internal abstract ReswareRequestOrder BuildRequestMessage(Order order, Signing signing);

        internal override bool PerformAction(Order order)
        {
            var signing = _receiveSigningServiceRepository.GetAllSignings().OrderByDescending(s => s.CreatedDateTime).FirstOrDefault(s => string.Equals(s.FileNumber, order.FileNumber, StringComparison.CurrentCultureIgnoreCase));

            if (signing == null) return false;

            var requestMessage = BuildRequestMessage(order, signing);

            AssignBorrowerInformation(requestMessage, order.BuyerAndSellers);

            _orderServiceUtility.AssignServices(requestMessage);

            return _mirthServiceClient.SendMessageToMirth(ModelSerializer.SerializeXml(requestMessage), 4412);
        }

        private static void AssignBorrowerInformation(ReswareRequestOrder requestClosingMessage, ICollection<BuyerSeller> buyerSellerResults)
        {
            var borrower = buyerSellerResults?.FirstOrDefault(b => b.Type == BuyerSellerEnum.Buyer && !b.Spouse);

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

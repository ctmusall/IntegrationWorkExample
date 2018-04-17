using System;
using eClosings.Core.Utilities;
using eClosings.Mirth.Clients;
using eClosings.Mirth.Messages;
using Resware.Core.Services.Utilities.ServiceUtilities;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareCommon.Constants;

namespace Resware.Core.ActionEvent.RequestDocPrep.ActionEvents
{
    internal class SolidifiRequestDocPrep : RequestOrder.ActionEvents.RequestOrder
    {
        private readonly IDateTimeUtility _dateTimeUtility;

        internal SolidifiRequestDocPrep(SigningRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient, ServiceUtility orderServiceUtility, IDateTimeUtility dateTimeUtility) : base(receiveSigningServiceRepository, mirthServiceClient, orderServiceUtility)
        {
            _dateTimeUtility = dateTimeUtility;
        }

        internal override ReswareRequestOrder BuildRequestMessage(Order order, Signing signing)
        {
            var requstMessage = new ReswareRequestOrder
            {
                OrderId = $"{order.FileNumber}-D",
                CustomerId = order.CustomerId,
                CustomerContact = CustomerContactConstants.DocDeed,
                LenderName = order.LenderName,
                CustomerProduct = order.CustomerProduct,
                FileNumber = order.FileNumber,
                OrderRequestedDate = DateTime.Now.ToShortDateString(),
                OrderRequestedTime = DateTime.Now.ToShortTimeString(),
                ClosingAddress1 = signing.ClosingAddress,
                ClosingCity = signing.ClosingCity,
                ClosingState = signing.ClosingState,
                ClosingZipCode = signing.ClosingZip,
                ClosingCounty = signing.ClosingCounty
            };

            SetClosingDateTime(requstMessage);

            SetProduct(requstMessage);

            return requstMessage;
        }

        private void SetClosingDateTime(ReswareRequestOrder request)
        {
            var closingDateTime = _dateTimeUtility.ResolveDocPrepClosingDateTime();

            request.ClosingDate = closingDateTime.ToShortDateString();
            request.ClosingTime = closingDateTime.ToShortTimeString();
        }

        private static void SetProduct(ReswareRequestOrder request)
        {
            if (StateConstants.OneHourReviewStates.Contains(request.ClosingState) || StateConstants.InternalPreparationStates.Contains(request.ClosingState))
            {
                request.Product = ProductNameConstants.EClosingsProductNames.DeedPrepInternal;
            }
            else
            {
                request.Product = ProductNameConstants.EClosingsProductNames.DeedPrepExternal;
            }
        }
    }
}

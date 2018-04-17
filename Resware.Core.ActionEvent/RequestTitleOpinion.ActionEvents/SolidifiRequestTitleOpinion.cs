using System;
using eClosings.Core.Utilities;
using eClosings.Mirth.Clients;
using eClosings.Mirth.Messages;
using Resware.Core.Services.Utilities.ServiceUtilities;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareCommon.Constants;

namespace Resware.Core.ActionEvent.RequestTitleOpinion.ActionEvents
{
    internal class SolidifiRequestTitleOpinion : RequestOrder.ActionEvents.RequestOrder
    {
        private readonly IDateTimeUtility _dateTimeUtility;

        internal SolidifiRequestTitleOpinion(SigningRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient, ServiceUtility orderServiceUtility, IDateTimeUtility dateTimeUtility) : base(receiveSigningServiceRepository, mirthServiceClient, orderServiceUtility)
        {
            _dateTimeUtility = dateTimeUtility;
        }

        internal override ReswareRequestOrder BuildRequestMessage(Order order, Signing signing)
        {
            var requestMessage = new ReswareRequestOrder
            {
                OrderId = $"{order.FileNumber}-T",
                CustomerId = order.CustomerId,
                CustomerContact = CustomerContactConstants.KristenMiller,
                LenderName = order.LenderName,
                Product = ProductNameConstants.EClosingsProductNames.IntSearchOpinion,
                FileNumber = order.FileNumber,
                OrderRequestedDate = DateTime.Now.ToShortDateString(),
                OrderRequestedTime = DateTime.Now.ToShortTimeString(),
                ClosingAddress1 = signing.ClosingAddress,
                ClosingCity = signing.ClosingCity,
                ClosingState = signing.ClosingState,
                ClosingZipCode = signing.ClosingZip,
                ClosingCounty = signing.ClosingCounty
            };

            SetClosingDateTime(requestMessage, DateTime.Now);

            return requestMessage;
        }

        private void SetClosingDateTime(ReswareRequestOrder request, DateTime requestedClosingDateTime)
        {
            var closingDateTime = _dateTimeUtility.ResolveTitleOpinionClosingDateTime(requestedClosingDateTime);

            request.ClosingDate = closingDateTime.ToShortDateString();
            request.ClosingTime = closingDateTime.ToShortTimeString();
        }
    }
}

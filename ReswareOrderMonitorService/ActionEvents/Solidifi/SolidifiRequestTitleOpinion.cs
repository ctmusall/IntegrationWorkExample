using System;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Solidifi
{
    internal class SolidifiRequestTitleOpinion : RequestOrder
    {
        private readonly IDateTimeUtility _dateTimeUtility;

        internal SolidifiRequestTitleOpinion(SigningRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient, IServiceUtility orderServiceUtility, IDateTimeUtility dateTimeUtility) : base(receiveSigningServiceRepository, mirthServiceClient, orderServiceUtility)
        {
            _dateTimeUtility = dateTimeUtility;
        }

        internal override RequestMessage BuildRequestMessage(Order order, Signing signing)
        {
            var requestMessage = new RequestMessage
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

        private void SetClosingDateTime(RequestMessage requestMessage, DateTime requestedClosingDateTime)
        {
            var closingDateTime = _dateTimeUtility.ResolveTitleOpinionClosingDateTime(requestedClosingDateTime);

            requestMessage.ClosingDate = closingDateTime.ToShortDateString();
            requestMessage.ClosingTime = closingDateTime.ToShortTimeString();
        }
    }
}

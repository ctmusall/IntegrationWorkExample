using System;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.ReswareSigning;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Solidifi
{
    internal class SolidifiRequestDocPrep : RequestOrder
    {
        private readonly IDateTimeUtility _dateTimeUtility;

        private const string CustomerContact = "DOC DEED";

        public SolidifiRequestDocPrep(IServiceUtility orderServiceUtility) : this(orderServiceUtility, ReswareOrderDependencyFactory.Resolve<IDateTimeUtility>())
        {
        }

        internal SolidifiRequestDocPrep(IServiceUtility orderServiceUtility, IDateTimeUtility dateTimeUtility) : base(orderServiceUtility)
        {
            _dateTimeUtility = dateTimeUtility;
        }

        internal override RequestMessage BuildRequestMessage(OrderResult order, SigningServiceResult signing)
        {
            var requstMessage =  new RequestMessage
            {
                OrderId = $"{order.FileNumber}-D",
                CustomerId = order.CustomerId,
                CustomerContact = CustomerContact,
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

        private void SetClosingDateTime(RequestMessage requestMessage)
        {
            var closingDateTime = _dateTimeUtility.ResolveDocPrepClosingDateTime();

            requestMessage.ClosingDate = closingDateTime.ToShortDateString();
            requestMessage.ClosingTime = closingDateTime.ToShortTimeString();
        }

        private static void SetProduct(RequestMessage requestMessage)
        {
            if (StateConstants.OneHourReviewStates.Contains(requestMessage.ClosingState) || StateConstants.InternalPreparationStates.Contains(requestMessage.ClosingState))
            {
                requestMessage.Product = ProductNameConstants.EClosingsProductNames.DeedPrepInternal;
            }
            else
            {
                requestMessage.Product = ProductNameConstants.EClosingsProductNames.DeedPrepExternal;
            }
        }
    }
}

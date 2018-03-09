using System;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.ReswareSigning;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Solidifi
{
    internal class SolidifiRequestTitleOpinion : RequestOrder
    {
        private readonly IDateTimeUtility _dateTimeUtility;

        private const string CustomerContact = "KRISTEN MILLER";
        private const string Product = "INT--Search/Opinion";

        internal SolidifiRequestTitleOpinion(IServiceUtility orderServiceUtility) : this(orderServiceUtility, ReswareOrderDependencyFactory.Resolve<IDateTimeUtility>())
        {

        }

        internal SolidifiRequestTitleOpinion(IServiceUtility orderServiceUtility, IDateTimeUtility dateTimeUtility) : base(orderServiceUtility)
        {
            _dateTimeUtility = dateTimeUtility;
        }

        internal override RequestMessage BuildRequestMessage(OrderResult order, SigningServiceResult signing)
        {
            var requestMessage = new RequestMessage
            {
                OrderId = $"{order.FileNumber}-T",
                CustomerId = order.CustomerId,
                CustomerContact = CustomerContact,
                LenderName = order.LenderName,
                Product = Product,
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

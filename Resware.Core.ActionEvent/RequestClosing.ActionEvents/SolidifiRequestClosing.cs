using System;
using eClosings.Mirth.Clients;
using eClosings.Mirth.Messages;
using Resware.Core.Services.Utilities.ServiceUtilities;
using Resware.Data.Signing.Repository;
using Resware.Entities.Orders;
using Resware.Entities.Signings;
using ReswareCommon.Constants;

namespace Resware.Core.ActionEvent.RequestClosing.ActionEvents
{
    internal class SolidifiRequestClosing : RequestOrder.ActionEvents.RequestOrder
    {
        internal SolidifiRequestClosing(SigningRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient, ServiceUtility orderServiceUtility) : base(receiveSigningServiceRepository, mirthServiceClient, orderServiceUtility) { }

        internal override ReswareRequestOrder BuildRequestMessage(Order order, Signing signing)
        {
            return new ReswareRequestOrder
            {
                OrderId = order.FileNumber,
                CustomerId = order.CustomerId,
                CustomerContact = CustomerContactConstants.TeamClosings,
                LenderName = order.LenderName,
                Product = order.Product,
                CustomerProduct = order.CustomerProduct,
                FileNumber = order.FileNumber,
                OrderRequestedDate = DateTime.Now.ToShortDateString(),
                OrderRequestedTime = DateTime.Now.ToShortTimeString(),
                DocsToAttorney = ProductNameConstants.EClosingsProductNames.EDoc,
                ClosingDate = signing.ClosingDateTime?.ToShortDateString() ?? order.ClosingDateTime?.ToShortDateString()?? DateTime.Now.ToShortDateString(),
                ClosingTime = signing.ClosingDateTime?.ToShortTimeString() ?? order.ClosingDateTime?.ToShortTimeString() ?? DateTime.Now.ToShortTimeString(),
                ClosingAddress1 = signing.ClosingAddress,
                ClosingCity = signing.ClosingCity,
                ClosingState = signing.ClosingState,
                ClosingZipCode = signing.ClosingZip,
                ClosingCounty = signing.ClosingCounty
            };
        }
    }
}
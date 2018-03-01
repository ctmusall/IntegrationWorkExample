using System;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities
{
    internal class LinearClosingServiceUtility : ClosingServiceUtility
    {
        internal override void DetermineDelawareServices(RequestClosingMessage requestClosingMessage)
        {
            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.Refinance, StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.Refinance, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.RefinanceInvestment, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.Investment, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.Purchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.Purchase,StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.PurchaseBuyersSide,StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseReoBuyerSide;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.DeedInLieu,StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.ModAssumptionDil,StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedDeedInLieu;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.PhoneSigning,StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.ConferenceCall, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.ConferenceCallClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Disbursement;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.ESigning, StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.Online, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedEsignClosingF2F;
                requestClosingMessage.Service2 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service3 = ServiceNameConstants.EdocsReducedPackage;
            }
        }
    }
}

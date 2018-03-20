using System;
using ReswareOrderMonitorService.Models;
using ReswareCommon;

namespace ReswareOrderMonitorService.Utilities.Solidifi
{
    internal class SolidifiClosingServiceUtility : ServiceUtility
    {
        public override void AssignServices(RequestMessage requestClosingMessage)
        {
            if (!string.Equals(requestClosingMessage.ClosingState, requestClosingMessage.BorrowerState, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Notes += $"Did not apply any services because the Closing state '{requestClosingMessage.ClosingState}' is not equal to the borrower state '{requestClosingMessage.BorrowerState}'.";
                return;
            }

            switch (requestClosingMessage.ClosingState)
            {
                case StateConstants.Delaware:
                    DetermineDelawareServices(requestClosingMessage);
                    return;
                case StateConstants.Georgia:
                    DetermineGeorgiaServices(requestClosingMessage);
                    return;
                case StateConstants.Massachusetts:
                    DetermineMassachusettsServices(requestClosingMessage);
                    return;
                case StateConstants.NorthCarolina:
                    DetermineNorthCarolinaServices(requestClosingMessage);
                    return;
                case StateConstants.NewYork:
                    DetermineNewYorkServices(requestClosingMessage);
                    return;
                case StateConstants.Vermont:
                    DetermineVermontServices(requestClosingMessage);
                    return;
                case StateConstants.WestVirginia:
                    DetermineWestVirginiaServices(requestClosingMessage);
                    return;
                default:
                    requestClosingMessage.Notes += $"Did not apply any services because the closing state is '{requestClosingMessage.ClosingState}'.";
                    return;
            }
        }

        private static void DetermineDelawareServices(RequestMessage requestClosingMessage)
        {
            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase,StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.BuyerSidePurchase,StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseReoBuyerSide;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.DeedInLieu,StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ModAssumptionDil,StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedDeedInLieu;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ConferenceCall,StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.ConferenceCallClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Disbursement;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ESignRefinance, StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Online, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedEsignClosingF2F;
                requestClosingMessage.Service2 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service3 = ServiceNameConstants.EdocsReducedPackage;
            }
        }

        private static void DetermineGeorgiaServices(RequestMessage requestClosingMessage)
        {
            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase)
                            && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
                requestClosingMessage.Service5 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
                requestClosingMessage.Service5 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.BuyerSidePurchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseReoBuyerSide;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.DeedInLieu, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ModAssumptionDil, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedDeedInLieu;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.ConferenceCallClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Disbursement;
            }
        }

        private static void DetermineMassachusettsServices(RequestMessage requestClosingMessage)
        {
            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
                requestClosingMessage.Service5 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
                requestClosingMessage.Service5 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.BuyerSidePurchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseReoBuyerSide;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
                requestClosingMessage.Service5 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.DeedInLieu, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ModAssumptionDil, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedDeedInLieu;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.ConferenceCallClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Disbursement;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ESignRefinance, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Online, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedEsignClosingF2F;
                requestClosingMessage.Service2 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service3 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service4 = ServiceNameConstants.RecordingSupervision;
            }
        }

        private static void DetermineNorthCarolinaServices(RequestMessage requestClosingMessage)
        {
            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.BuyerSidePurchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseReoBuyerSide;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.DeedInLieu, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ModAssumptionDil, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedDeedInLieu;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.ConferenceCallClosing;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ESignRefinance, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Online, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedEsignClosingF2F;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
            }
        }

        private static void DetermineNewYorkServices(RequestMessage requestClosingMessage)
        {
            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.BuyerSidePurchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseReoBuyerSide;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.DeedInLieu, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ModAssumptionDil, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedDeedInLieu;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.ConferenceCallClosing;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ESignRefinance, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Online, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedEsignClosingF2F;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
            }
        }

        private static void DetermineVermontServices(RequestMessage requestClosingMessage)
        {
            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.BuyerSidePurchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseReoBuyerSide;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.DeedInLieu, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ModAssumptionDil, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedDeedInLieu;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.ConferenceCallClosing;
            }
        }

        private static void DetermineWestVirginiaServices(RequestMessage requestClosingMessage)
        {
            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Refinance, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.InvestmentProperty, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.Disbursement;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Edocs;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.BuyerSidePurchase, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.Purchase, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedPurchaseReoBuyerSide;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.DisbursementPurchase;
                requestClosingMessage.Service4 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.DeedInLieu, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ModAssumptionDil, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.AttorneyAssistedDeedInLieu;
                requestClosingMessage.Service2 = ServiceNameConstants.EdocsReducedPackage;
                requestClosingMessage.Service3 = ServiceNameConstants.AttorneyProvidedFaxedDocs;
                return;
            }

            if (string.Equals(requestClosingMessage.CustomerProduct, ProductNameConstants.SolidifiProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase)
                && string.Equals(requestClosingMessage.Product, ProductNameConstants.EClosingsProductNames.ConferenceCall, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Service1 = ServiceNameConstants.ConferenceCallClosing;
                requestClosingMessage.Service2 = ServiceNameConstants.Disbursement;
            }
        }
    }
}

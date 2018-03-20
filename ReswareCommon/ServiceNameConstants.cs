using System.Collections.Generic;

namespace ReswareCommon
{
    public static class ServiceNameConstants
    {
        public const string AttorneyAssistedClosing = "Attorney Assisted Closing";
        public const string AttorneyAssistedPurchaseClosing = "Attorney Assisted Purchase Closing";
        public const string AttorneyAssistedPurchaseReoBuyerSide = "Attorney Assisted Purchase/REO - Buyer Side";
        public const string AttorneyAssistedDeedInLieu = "Attorney Assisted Deed In Lieu";
        public const string ConferenceCallClosing = "Conference Call Closing";
        public const string AttorneyAssistedEsignClosingF2F = "Attorney Assisted eSign Closing (F2F)";
        public const string Edocs = "eDocs";
        public const string Disbursement = "Disbursement";
        public const string AttorneyProvidedFaxedDocs = "Attorney Provided Faxed Docs";
        public const string DisbursementPurchase = "Disbursement - Purchase";
        public const string EdocsReducedPackage = "eDocs - Reduced Package";
        public const string RecordingSupervision = "Recording Supervision";
        public const string TitleOpinionLetter = "Title Opinion Letter";
        public const string TitleOpinionPreparationAndReview = "Title Opinion Preparation and Review";
        public const string MaMarketableTitleLetter = "MA Marketable Title Letter";
        public const string DeedPreparationAndReview = "Deed Preparation & Review";
        public const string DeedPreparation = "Deed Preparation";
        public const string NotaryFee = "Notary Fee";
        public const string DisbursementSupervision = "Disbursement Supervision";

        public static ICollection<string> AdditionalAttorneyServices = new List<string> { NotaryFee, Disbursement, DisbursementPurchase, RecordingSupervision, DisbursementSupervision };
        public static ICollection<string> TitleOpinionAndDocPrepServices = new List<string> { TitleOpinionLetter, TitleOpinionPreparationAndReview, MaMarketableTitleLetter, DeedPreparation, DeedPreparationAndReview };
    }
}

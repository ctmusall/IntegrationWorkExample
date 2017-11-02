using System.Deployment.Internal;

namespace PCN_Integration.Services.Common
{
    internal static class PcnIntegrationServicesConstants
    {
        internal static class ServiceNames
        {
            internal const string FassMonitorFullyQualified = "PCN_Integration.Services.Services.FassMonitor";
            internal const string MirthServiceFullyQualified = "PCN_Integration.Services.Services.MirthService";
        }

        internal static class OrderStatus
        {
            internal const string Pending = "Pending";
            internal const string Scheduled = "Scheduled";
            internal const string Cancelled = "Cancelled";
            internal const string UnableToFill = "Unable to Fill";
            internal const string Closed = "Closed";
            internal const string Adjourned = "Adjourned";
        }

        internal static class OrderValidationMessages
        {
            internal const string OrderFound = "Order found";
        }
        
        internal static class FassCancellationReasons
        {
            internal const string NotaryBackedOutofSigningAppointment = "Notary Backed Out of Signing Appointment";
            internal const string BorrowerCancelledSigningNotaryDidNotMakeATrip = "Borrower Cancelled Signing, Notary Did Not Make a Trip";
            internal const string FassCancelledSigningAppointment = "FASS Cancelled Signing Appointment";
            internal const string AppointmentRescheduled = "Appointment Rescheduled";
            internal const string LoanDocumentsNotAvailable = "Loan Documents Not Available";
            internal const string EscrowLenderCancelledSigningAppointment = "Escrow/Lender Cancelled Signing Appointment";
            internal const string NotaryDidNotShowToSigningAppointment = "Notary Did Not Show to Signing Appointment";
            internal const string NotaryUnableToScheduleLoanModSigning = "Notary unable to schedule Loan Modification signing--Order cancelled on Deadline Date";
        }

        internal static class PcnCancellationReasons
        {
            internal const string AssociateChange = "Associate Change";
            internal const string AttorneyCancelled = "Attorney Cancelled";
            internal const string AttorneyCancelledDueToAFamilyEmergency = "Attorney Cancelled due to a family emergency";
            internal const string AttorneyCancelledDueToCourt = "Attorney Cancelled due to court";
            internal const string AttorneyCancelledDueToASchedulingConflict = "Attorney Cancelled due to a scheduling conflict";
            internal const string AttorneyCancelledUnapproved = "Attorney Cancelled - unapproved";
            internal const string AttorneyCancelledDueToInclementWeather = "Attorney Cancelled due to Inclement Weather";
            internal const string AttorneyCancelledFaxbacks = "Attorney Cancelled--faxbacks";
            internal const string AutomatedOrderUpdate = "AUTOMATED Order Update";
            internal const string BorrowerCancelled = "Borrower Cancelled";
            internal const string ClientCancelled = "Client Cancelled";
            internal const string ClientError = "Client error";
            internal const string ClientRescheduledForANewDateTime = "Client re-scheduled for a new date/time";
            internal const string ClientRescheduledForANewClosingLocation = "Client re-scheduled for new closing location";
            internal const string DocumentsChangingFromBorrowerOvernightToEdocs = "Documents changing from Borrower Overnight to Edocs";
            internal const string DocumentsNotReadyInTime = "Documents not ready in time";
            internal const string LenderCancelled = "Lender Cancelled";
            internal const string NoClientResponseDocuments = "No client response--Documents";
            internal const string NotaryDidNotCompleteConferenceCall = "Notary did not complete conference call";
            internal const string OrderEntryError = "Order entry error";
            internal const string PcLawFileCancellation = "PC Law file cancellation";
            internal const string PcnCancelled = "PCN Cancelled";
            internal const string PcnSchedulingError = "PCN Scheduling Error";
            internal const string AttorneyNoShow = "Attorney No Show";
            internal const string ModCancellationAttorneyCouldNotScheduleWithBorrower = "Mod Cancellation--Attorney could not schedule with Borrower";
        }

        internal static class PcnCancellationReasonsLongDescriptions
        {
            internal const string AssociateChangeLongDescription = "PCN has assigned this appointment to a new closer.";
            internal const string AttorneyCancelledLongDescription = "Attorney Cancelled; PCN is working to locate a new attorney";
            internal const string AttorneyCancelledDueToAFamilyEmergencyLongDescription = "Attorney Cancelled due to a family emergency; PCN is working to locate a new attorney.";
            internal const string AttorneyCancelledDueToCourtLongDescription = "Attorney Cancelled due to court; PCN is working to locate a new attorney.";
            internal const string AttorneyCancelledDueToASchedulingConflictLongDescription = "Attorney cancelled due to a scheduling conflict; PCN is working to locate a new attorney.";
            internal const string AttorneyCancelledUnapprovedLongDescription = "Attorney cancelled due to a scheduling conflict; PCN is working to locate a new attorney.";
            internal const string AttorneyCancelledDueToInclementWeatherLongDescription = "Attorney cancelled due to weather conditions that prohibit travel.";
            internal const string AttorneyCancelledFaxbacksLongDescription = "Attorney cancelled, not able to meet faxback time requirementt; PCN is working to locate a new attorney.";
            internal const string AutomatedOrderUpdateLongDescription = "Automated order already exists for same date/time/location.";
            internal const string BorrowerCancelledLongDescription = "Borrower cancelled scheduled closing appointment with attorney.";
            internal const string ClientCancelledLongDescription = "Client cancelled closing appointment.";
            internal const string ClientErrorLongDescription = "Client cancelled closing appointment.";
            internal const string ClientRescheduledForANewDateTimeLongDescription = "Client re-scheduled the closing for a new date/time.";
            internal const string ClientRescheduledForANewClosingLocationLongDescription = "Client re-scheduled the closing to a new closing location.";
            internal const string DocumentsChangingFromBorrowerOvernightToEdocsLongDescription = "Documents changing from Borrower Overnight to Edocs.";
            internal const string DocumentsNotReadyInTimeLongDescription = "Closing documents are not available.";
            internal const string LenderCancelledLongDescription = "Lender cancelled closing appointment.";
            internal const string NoClientResponseDocumentsLongDescription = "Closing documents are not available.";
            internal const string NotaryDidNotCompleteConferenceCallLongDescription = "Notary did not communicate with the assigned PC Law attorney to complete the conference call.";
            internal const string OrderEntryErrorLongDescription = "The order was entered incorrectly.";
            internal const string PcLawFileCancellationLongDescription = "PC Law file cancellation.";
            internal const string PcnCancelledLongDescription = "PCN will be assigning a new attorney.";
            internal const string PcnSchedulingErrorLongDescription = "PCN will be assigning a new attorney.";
            internal const string AttorneyNoShowLongDescription = "Attorney Did not Show to Signing Appointment.";
            internal const string ModCancellationAttorneyCouldNotScheduleWithBorrowerLongDescription = "Mod Cancellation--Attorney could not schedule with Borrower.";
        }
    }
}

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
    }
}

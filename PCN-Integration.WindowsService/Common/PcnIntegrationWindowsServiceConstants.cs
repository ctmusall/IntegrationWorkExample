namespace PCN_Integration.WindowsService.Common
{
    internal static class PcnIntegrationWindowsServiceConstants
    {
        #region ServiceProperties
        internal static class PcnIntegrationServiceProperies
        {
            internal const string ServiceName = "PCN-Integration";
        }
        #endregion

        #region ServiceStatusMessages
        internal static class PcnIntegrationServiceStatusMessages
        {
            internal const string IntegrationStarted = "PCN-Integration Windows Service has started.";
            internal const string IntegrationStopped = "PCN-Integration Windows Service has stopped.";
        }
        #endregion
    }
}

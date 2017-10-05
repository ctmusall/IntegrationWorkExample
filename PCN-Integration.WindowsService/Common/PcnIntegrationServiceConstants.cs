namespace PCN_Integration.WindowsService.Common
{
    internal static class PcnIntegrationServiceConstants
    {
        #region ServiceProperties
        internal static class PcnIntegrationServiceProperies
        {
            internal static string ServiceName = "PCN-Integration";
        }
        #endregion

        #region ServiceStatusMessages
        internal static class PcnIntegrationServiceStatusMessages
        {
            internal static string IntegrationStarted = "PCN-Integration Windows Service has started.";
            internal static string IntegrationStopped = "PCN-Integration Windows Service has stopped.";
        }
        #endregion
    }
}

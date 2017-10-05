﻿using System.ServiceProcess;
using PCN_Integration.Services;
using PCN_Integration.WindowsService.Common;

namespace PCN_Integration.WindowsService
{
    internal partial class IntegrationService : ServiceBase
    {
        internal IntegrationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(PcnIntegrationServiceConstants.PcnIntegrationServiceStatusMessages.IntegrationStarted);
        }    

        protected override void OnStop()
        {
            EventLog.WriteEntry(PcnIntegrationServiceConstants.PcnIntegrationServiceProperies.ServiceName);
        }

        private void CreateAndStartFassIntegrationService()
        {
            var fass = new FassMonitor();
            fass.TrackNewFassOrders();
            fass.SendFassNotificationOnUpdate();
        }
    }
}

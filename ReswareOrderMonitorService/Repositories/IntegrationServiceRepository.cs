using System;
using System.Diagnostics;
using ReswareOrderMonitorService.eClosingIntegrationService;

namespace ReswareOrderMonitorService.Repositories
{
    internal class IntegrationServiceRepository : IIntegrationServiceRepository
    {
        private readonly IIntegrationService _integrationServiceClient;

        public IntegrationServiceRepository(): this(new IntegrationServiceClient()) { }

        internal IntegrationServiceRepository(IIntegrationService integrationServiceClient)
        {
            _integrationServiceClient = integrationServiceClient;
        }

        public GetOrderResult GetOrder(string customerId, string fileNumber)
        {
            try
            {
                return _integrationServiceClient.GetOrder(customerId, fileNumber);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
                return null;
            }
        }
    }
}

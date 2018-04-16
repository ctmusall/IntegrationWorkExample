using System;
using System.Diagnostics;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.Readers;

namespace ReswareOrderMonitorService.Repositories
{
    internal class IntegrationServiceRepository : IIntegrationServiceRepository
    {
        private readonly IIntegrationService _integrationServiceClient;
        private readonly IEClosingOrderReader _eClosingOrderReader;

        public IntegrationServiceRepository(): this(new IntegrationServiceClient(), DependencyFactory.Resolve<IEClosingOrderReader>()) { }

        internal IntegrationServiceRepository(IIntegrationService integrationServiceClient, IEClosingOrderReader eClosingOrderReader)
        {
            _integrationServiceClient = integrationServiceClient;
            _eClosingOrderReader = eClosingOrderReader;
        }

        public EClosingOrder GetOrder(string customerId, string fileNumber)
        {
            try
            {
                var eClosingIntegrationOrderResult = _integrationServiceClient.GetOrder(customerId, fileNumber);
                return _eClosingOrderReader.MapEClosingOrder(eClosingIntegrationOrderResult);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
                return null;
            }
        }
    }
}

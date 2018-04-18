using System;
using System.Diagnostics;
using eClosings.Data.eClosingsIntegrationService;
using eClosings.Data.Readers.OrderReader;
using eClosings.Entities.Orders;

namespace eClosings.Data.IntegrationService.Repository
{
    public class IntegrationServiceRepository : IIntegrationServiceRepository
    {
        private readonly IIntegrationService _integrationServiceClient;
        private readonly EClosingOrderReader _eClosingOrderReader;

        public IntegrationServiceRepository(): this(new IntegrationServiceClient(), new EClosingOrderReader()) { }

        internal IntegrationServiceRepository(IIntegrationService integrationServiceClient, EClosingOrderReader eClosingOrderReader)
        {
            _integrationServiceClient = integrationServiceClient;
            _eClosingOrderReader = eClosingOrderReader;
        }

        public Order GetOrder(string customerId, string fileNumber)
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

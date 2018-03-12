using System;
using ReswareOrderMonitorService.eClosingIntegrationService;

namespace ReswareOrderMonitorService.Repositories
{
    internal class IntegrationServiceRepository : IIntegrationServiceRepository
    {
        private readonly IntegrationServiceClient _integrationServiceClient;

        internal IntegrationServiceRepository(): this(new IntegrationServiceClient()) { }

        internal IntegrationServiceRepository(IntegrationServiceClient integrationServiceClient)
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
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}

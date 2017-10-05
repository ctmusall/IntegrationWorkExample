using System;

namespace PCN_Integration.Services.Services.IntegrationServiceFactory
{
    public class ServiceFactory : IServiceFactory
    {
        public IntegrationServiceBase ResolveIntegrationService(Type type)
        {
            return new FassMonitor();
        }
    }
}

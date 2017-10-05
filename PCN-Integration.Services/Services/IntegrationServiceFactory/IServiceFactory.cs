using System;

namespace PCN_Integration.Services.Services.IntegrationServiceFactory
{
    public interface IServiceFactory
    {
        IntegrationServiceBase ResolveIntegrationService(Type type);
    }
}

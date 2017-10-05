using System;

namespace PCN_Integration.Services.Services.IntegrationServiceFactory
{
    public interface IServiceFactory
    {
        IIntegrationServiceBase ResolveIntegrationService(Type type);
    }
}

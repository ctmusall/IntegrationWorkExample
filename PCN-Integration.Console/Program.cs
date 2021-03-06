﻿using PCN_Integration.Services.Services;
using PCN_Integration.Services.Services.IntegrationServiceFactory;

namespace PCN_Integration.Console 
{
  class Program
  {
    static void Main()
    {
      var serviceFactory = new ServiceFactory();
      serviceFactory.ResolveIntegrationService(typeof(FassMonitor)).BeginIntegrationProcessing();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCN_Integration.DataModels;
using System.Data;
using PCN_Integration.Services;
using PCN_Integration.Services.Services;
using PCN_Integration.Services.Services.IntegrationServiceFactory;

namespace PCN_Integration.Console 
{
  class Program
  {
    static void Main(string[] args)
    {
      var serviceFactory = new ServiceFactory();
      serviceFactory.ResolveIntegrationService(typeof(FassMonitor)).BeginIntegrationProcessing();
    }
  }
}
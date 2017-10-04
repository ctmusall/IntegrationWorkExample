using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCN_Integration.Core.FassService;

namespace PCN_Integration.Core
{
  public class FassWebServiceInvoker : IDisposable
  {
    private FassService.GetPCNOrderStatusResponse2Body _client;

    public void Dispose()
    {
      // _getOrderResult = null;
      // _client = null;
    }
  }


}

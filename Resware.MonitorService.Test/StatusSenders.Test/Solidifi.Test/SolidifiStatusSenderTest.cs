using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace Resware.MonitorService.Test.StatusSenders.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiStatusSenderTest
    {
        private IStatusSender _solidifiStatusSender;
        private GetOrderResult _eClosingOrder;

        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void SendStatusUpdate_should_build_document_and_send_status_update_internally()
        {
            
        }
    }
}

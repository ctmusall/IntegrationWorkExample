using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Factories.StatusSenders.Solidifi;

namespace Resware.MonitorService.Test.Factories.Test.StatusSenders.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiDocPrepStatusSenderFactoryTest
    {
        private SolidifiDocPrepStatusSenderFactory _solidifiDocPrepStatusSenderFactory;
        private GetOrderResult _getOrderResult;
        private Order _reswareOrder;

        [TestInitialize]
        public void Setup()
        {
            _getOrderResult = new GetOrderResult
            {
                Order = new OutboundOrder()
            };
            _reswareOrder = new Order();

            _solidifiDocPrepStatusSenderFactory = new SolidifiDocPrepStatusSenderFactory(_getOrderResult);    
        }

        [TestMethod]
        public void ResolveStatusSender_should_return_null_order_is_invalid()
        {
            // Arrange
            _solidifiDocPrepStatusSenderFactory.EClosingOrder.Order = null;

            // Act
            var result = _solidifiDocPrepStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNull(result);
        }
    }
}

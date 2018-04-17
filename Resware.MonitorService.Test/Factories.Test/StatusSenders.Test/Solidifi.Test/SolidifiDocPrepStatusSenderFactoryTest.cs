using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Entities.Orders;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.Factories.StatusSenders.Solidifi;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.StatusSenders;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace Resware.MonitorService.Test.Factories.Test.StatusSenders.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiDocPrepStatusSenderFactoryTest
    {
        private SolidifiDocPrepStatusSenderFactory _solidifiDocPrepStatusSenderFactory;
        private EClosingOrder _getOrderResult;
        private Order _reswareOrder;
        
        [TestInitialize]
        public void Setup()
        {
            _getOrderResult = new EClosingOrder();
            _reswareOrder = new Order();

            _solidifiDocPrepStatusSenderFactory = new SolidifiDocPrepStatusSenderFactory(_getOrderResult);    
        }

        [TestMethod]
        public void ResolveStatusSender_should_return_null_order_is_invalid()
        {
            // Arrange
            _solidifiDocPrepStatusSenderFactory.EClosingOrder = null;

            // Act
            var result = _solidifiDocPrepStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveStatusSender_doc_prep_status_is_empty_should_return_solidifi_update_doc_prep_status()
        {
            // Arrange
            _reswareOrder.DocPrepStatus = string.Empty;

            // Act
            var result = _solidifiDocPrepStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiUpdateDocPrepStatus));
        }

        [TestMethod]
        public void ResolveStatusSender_order_has_assigned_attorney_should_return_solidifi_status_sender()
        {
            // Arrange
            _reswareOrder.DocPrepStatus = OrderStatusConstants.Pending;
            _solidifiDocPrepStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Scheduled;

            // Act
            var result = _solidifiDocPrepStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiStatusSender));
        }

        [TestMethod]
        public void ResolveStatusSender_status_is_the_same_return_null()
        {
            // Arrange
            _reswareOrder.DocPrepStatus = OrderStatusConstants.Scheduled;
            _solidifiDocPrepStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Scheduled;

            // Act
            var result = _solidifiDocPrepStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNull(result);
        }
    }
}

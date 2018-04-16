using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Entities.Orders;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Factories.StatusSenders.Solidifi;
using ReswareOrderMonitorService.Models;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace Resware.MonitorService.Test.Factories.Test.StatusSenders.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiTitleOpinionStatusSenderFactoryTest
    {
        private SolidifiTitleOpinionStatusSenderFactory _solidifiTitleOpinionStatusSenderFactory;
        private EClosingOrder _getOrderResult;
        private Order _reswareOrder;

        [TestInitialize]
        public void Setup()
        {
            _getOrderResult = new EClosingOrder();
            _reswareOrder = new Order();

            _solidifiTitleOpinionStatusSenderFactory = new SolidifiTitleOpinionStatusSenderFactory(_getOrderResult);    
        }

        [TestMethod]
        public void ResolveStatusSender_order_is_invalid_or_null_should_return_null()
        {
            // Arrange
            _solidifiTitleOpinionStatusSenderFactory.EClosingOrder = null;

            // Act
            var result = _solidifiTitleOpinionStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveStatusSender_resware_order_title_opinion_status_empty_should_return_solidifi_update_title_opinion_status()
        {
            // Arrange
            _reswareOrder.TitleOpinionStatus = string.Empty;

            // Act
            var result = _solidifiTitleOpinionStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiUpdateTitleOpinionStatus));
        }

        [TestMethod]
        public void ResolveStatusSender_current_resware_order_title_opinion_status_is_equal_to_the_eclosing_order_status_should_return_null()
        {
            // Arrange
            _reswareOrder.TitleOpinionStatus = OrderStatusConstants.Pending;
            _solidifiTitleOpinionStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Pending;

            // Act
            var result = _solidifiTitleOpinionStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveStatusSender_order_has_assigned_attorney_should_return_solidifi_status_sender()
        {
            // Arrange
            _reswareOrder.TitleOpinionStatus = OrderStatusConstants.Pending;
            _solidifiTitleOpinionStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Scheduled;

            // Act
            var result = _solidifiTitleOpinionStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiStatusSender));
        }

        [TestMethod]
        public void ResolveStatusSender_order_status_updates_from_scheduled_to_closed()
        {
            // Arrange
            _reswareOrder.TitleOpinionStatus = OrderStatusConstants.Scheduled;
            _solidifiTitleOpinionStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Closed;

            // Act
            var result = _solidifiTitleOpinionStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiUpdateTitleOpinionStatus));
        }
    }
}

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
    public class SolidifiClosingStatusSenderFactoryTest
    {
        private SolidifiClosingStatusSenderFactory _solidifiClosingStatusSenderFactory;
        private EClosingOrder _getOrderResult;
        private Order _reswareOrder;

        [TestInitialize]
        public void Setup()
        {
            _getOrderResult = new EClosingOrder();
            _reswareOrder = new Order();
            _solidifiClosingStatusSenderFactory = new SolidifiClosingStatusSenderFactory(_getOrderResult);
        }

        [TestMethod]
        public void ResolveStatusSender_order_result_is_invalid_should_return_null()
        {
            // Arrange
            _solidifiClosingStatusSenderFactory.EClosingOrder = null;

            // Act
            var result = _solidifiClosingStatusSenderFactory.ResolveStatusSender(_reswareOrder);
            
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveStatusSender_resware_closing_status_is_empty_should_return_solidifi_update_closing_status()
        {
            // Arrange
            _reswareOrder.ClosingStatus = string.Empty;
            _solidifiClosingStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Pending;
            
            // Act
            var result = _solidifiClosingStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiUpdateClosingStatus));
        }

        [TestMethod]
        public void ResolveStatusSender_resware_title_opinion_status_is_equal_to_eclosing_order_status_return_null()
        {
            // Arrange
            _reswareOrder.TitleOpinionStatus = OrderStatusConstants.Pending;
            _reswareOrder.ClosingStatus = OrderStatusConstants.Pending;
            _solidifiClosingStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Pending;

            // Act
            var result = _solidifiClosingStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveStatusSender_resware_closing_order_status_was_pending_and_current_closing_order_is_scheduled_should_return_solidifi_status_sender()
        {
            // Arrange
            _reswareOrder.ClosingStatus = OrderStatusConstants.Pending;
            _solidifiClosingStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Scheduled;

            // Act
            var result = _solidifiClosingStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiStatusSender));
        }

        [TestMethod]
        public void ResolveStatusSender_eclosing_closing_order_status_is_closed_should_return_solidifi_status_sender()
        {
            // Arrange
            _reswareOrder.ClosingStatus = OrderStatusConstants.Scheduled;
            _solidifiClosingStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Closed;

            // Act
            var result = _solidifiClosingStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiStatusSender));
        }

        [TestMethod]
        public void ResolveStatusSender_eclosing_closing_order_status_is_not_updated_should_return_null()
        {
            // Arrange 
            _reswareOrder.ClosingStatus = OrderStatusConstants.Scheduled;
            _solidifiClosingStatusSenderFactory.EClosingOrder.Status = OrderStatusConstants.Scheduled;

            // Act
            var result = _solidifiClosingStatusSenderFactory.ResolveStatusSender(_reswareOrder);

            // Assert
            Assert.IsNull(result);
        }
    }
}

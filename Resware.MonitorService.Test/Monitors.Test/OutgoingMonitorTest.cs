using System;
using System.Linq;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Data.ActionEvent.Repository;
using Resware.Data.Context;
using Resware.Data.Order.Repository;
using Resware.Entities.ActionEvents;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Factories.CompletedActionEvents;
using ReswareOrderMonitorService.Factories.StatusSenders;
using ReswareOrderMonitorService.Monitors;
using ReswareOrderMonitorService.StatusSenders;

namespace Resware.MonitorService.Test.Monitors.Test
{
    [TestClass]
    public class OutgoingMonitorTest
    {
        private OutgoingMonitor _outgoingMonitor;
        private static ReswareDbContext _reswareDbContext;
        private OrderRepository _orderRepository;
        private ActionEventRepository _actionEventRepository;
        private Mock<IParentClientCompletedActionEventFactory> _parentClientCompletedActionEventFactoryMock;
        private Mock<IClientCompletedActionEventFactory> _clientCompletedActionEventFactory;
        private Mock<IStatusSender> _statusSenderMock;
        private Mock<IStatusSenderFactory> _statusSenderFactoryMock;

        [TestInitialize]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _orderRepository = new OrderRepository(_reswareDbContext);
            _actionEventRepository = new ActionEventRepository(_reswareDbContext);
            _parentClientCompletedActionEventFactoryMock = new Mock<IParentClientCompletedActionEventFactory>();
            _clientCompletedActionEventFactory = new Mock<IClientCompletedActionEventFactory>();
            _statusSenderMock = new Mock<IStatusSender>();
            _statusSenderFactoryMock = new Mock<IStatusSenderFactory>();
            _outgoingMonitor = new OutgoingMonitor(_orderRepository, _actionEventRepository, _parentClientCompletedActionEventFactoryMock.Object);    
        }

        [TestMethod]
        public void MonitorOrders_no_orders_exist_should_return_and_not_update_any_entities()
        {
            // Act
            _outgoingMonitor.MonitorOrders();

            // Assert
            Assert.AreEqual(0, _reswareDbContext.Orders.Count());
        }

        [TestMethod]
        public void MonitorOrders_order_found_and_completed_action_events_exist_that_are_completed_should_send_status_update_and_not_update_any_entities()
        {
            // Arrange
            _reswareDbContext.Orders.Add(new Order {FileNumber = "123456", ClientId = 1, CustomerId = "L17100" });
            _reswareDbContext.ActionEvents.Add(new ActionEvent {ActionEventCode = "123", ActionCompleted = true, ActionCompletedDateTime = DateTime.Now, CreatedDateTime = DateTime.Now, FileNumber = "123456"});
            _reswareDbContext.SaveChanges();

            _statusSenderMock.Setup(ss => ss.SendStatusUpdate(It.IsAny<Order>()));
            _statusSenderFactoryMock.Setup(ssf => ssf.ResolveStatusSender(It.IsAny<Order>())).Returns(_statusSenderMock.Object);
            _clientCompletedActionEventFactory.Setup(ccae => ccae.ResolveCompletedActionEventStatusSenderFactory("123", "L17100", "123456")).Returns(_statusSenderFactoryMock.Object);
            _parentClientCompletedActionEventFactoryMock.Setup(pccae => pccae.ResolveClientCompletedActionEventFactory(1)).Returns(_clientCompletedActionEventFactory.Object);

            // Act
            _outgoingMonitor.MonitorOrders();

            // Assert
            Assert.AreEqual(1, _reswareDbContext.Orders.Count());
            Assert.AreEqual(1, _reswareDbContext.ActionEvents.Count());
        }
    }
}

using System.Linq;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Core.ActionEvent.Factories.ParentActionEvents;
using Resware.Core.ActionEvent.Readers.ActionEvents;
using Resware.Core.Services.Factories.ParentServiceUtilities;
using Resware.Data.ActionEvent.Repository;
using Resware.Data.Context;
using Resware.Data.Order.Repository;
using Resware.Entities.ActionEvents;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Monitors.OrderActionEvents;

namespace Resware.MonitorService.Test.Monitors.Test
{
    [TestClass]
    public class OrderActionEventMonitorTest
    {
        private OrderRepository _orderRepository;
        private ActionEventRepository _actionEventRepository;
        private ParentActionEventFactory _parentActionEventFactory;
        private ActionEventReader _actionEventReader;
        private ParentServiceUtilityFactory _parentServiceUtilityFactory;
        private static OrderActionEventMonitor _orderActionEventMonitor;
        private static ReswareDbContext _reswareDbContext;

        [TestInitialize]
        public void Setup()
        {
            //EffortProviderConfiguration.RegisterProvider();
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _orderRepository = new OrderRepository(_reswareDbContext);
            _actionEventRepository = new ActionEventRepository(_reswareDbContext);
            _parentServiceUtilityFactory = new ParentServiceUtilityFactory();
            _parentActionEventFactory = new ParentActionEventFactory(_parentServiceUtilityFactory);
            _actionEventReader = new ActionEventReader(_actionEventRepository, _parentActionEventFactory);

            _orderActionEventMonitor = new OrderActionEventMonitor(_orderRepository, _actionEventReader);
        }

        [TestMethod]
        public void MonitorOrderActionEvents_should_return_and_not_complete_any_action_events_no_orders_exist()
        {
            // Arrange
            var orders = _orderRepository.GetAllOrders();

            // Act
            _orderActionEventMonitor.MonitorOrderActionEvents();

            // Assert
            Assert.AreEqual(0, orders.Count);
        }

        [TestMethod]
        public void MonitorOrderActionEvents_should_iterate_over_each_order_and_complete_action_events()
        {
            // Arrange
            _reswareDbContext.Orders.Add(new Order { FileNumber = "123456" });
            _reswareDbContext.ActionEvents.Add(new ActionEvent {ActionEventCode = "123", FileNumber = "123456"});
            _reswareDbContext.SaveChanges();

            // Act
            _orderActionEventMonitor.MonitorOrderActionEvents();

            // Assert
            Assert.AreEqual(1, _reswareDbContext.Orders.Count());
            Assert.AreEqual(1, _reswareDbContext.ActionEvents.Count());
        }
    }
}

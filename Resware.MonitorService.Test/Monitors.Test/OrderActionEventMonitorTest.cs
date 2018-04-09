﻿using System.Linq;
using Effort;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Data.ActionEvent.Repository;
using Resware.Data.Context;
using Resware.Data.Order.Repository;
using Resware.Entities.ActionEvents;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.ActionEvents;
using ReswareOrderMonitorService.Monitors;
using ReswareOrderMonitorService.Readers;

namespace Resware.MonitorService.Test.Monitors.Test
{
    [TestClass]
    public class OrderActionEventMonitorTest
    {
        private OrderRepository _orderRepository;
        private ActionEventRepository _actionEventRepository;
        private IParentActionEventFactory _parentActionEventFactory;
        private IActionEventReader _actionEventReader;
        private IParentServiceUtilityFactory _parentServiceUtilityFactory;
        private IOrderActionEventMonitor _orderActionEventMonitor;
        private ReswareDbContext _reswareDbContext;

        [TestInitialize]
        public void Setup()
        {
            EffortProviderConfiguration.RegisterProvider();
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
using System;
using System.Linq;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Data.ActionEvent.Repository;
using Resware.Data.Context;
using Resware.Entities.ActionEvents;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Factories.ActionEvents;
using ReswareOrderMonitorService.Readers;

namespace Resware.MonitorService.Test.Readers.Test
{
    [TestClass]
    public class ActionEventReaderTest
    {
        private IActionEventReader _actionEventReader;
        private ActionEventRepository _actionEventRepository;
        private ReswareDbContext _reswareDbContext;

        private Mock<IParentActionEventFactory> _parentActionEventFactoryMock;


        private Mock<ActionEventFactory> _actionEventFactoryMock;
        private Mock<ReswareOrderMonitorService.ActionEvents.ActionEvent> _actionEventMock;



        [TestInitialize]
        public void Setup()
        {
            //EffortProviderConfiguration.RegisterProvider();
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _actionEventRepository = new ActionEventRepository(_reswareDbContext);

            _actionEventMock = new Mock<ReswareOrderMonitorService.ActionEvents.ActionEvent>();
            _actionEventMock.Setup(ae => ae.PerformAction(It.IsAny<Order>())).Returns(true);

            _actionEventFactoryMock = new Mock<ActionEventFactory>();
            _actionEventFactoryMock.Setup(ae => ae.ResolveActionEvent(It.IsAny<string>())).Returns(_actionEventMock.Object);

            _parentActionEventFactoryMock = new Mock<IParentActionEventFactory>();
            _parentActionEventFactoryMock.Setup(pa => pa.ResolveActionEventFactory(It.IsAny<int>())).Returns(_actionEventFactoryMock.Object);


            _actionEventReader = new ActionEventReader(_actionEventRepository, _parentActionEventFactoryMock.Object);
        }

        [TestMethod]
        public void CompleteActions_should_return_and_not_update_any_action_events()
        {
            // Act
            _actionEventReader.CompleteActions(null);
            
            // Assert
            Assert.AreEqual(0, _reswareDbContext.ActionEvents.Count());
        }

        [TestMethod]
        public void CompleteActions_should_return_order_exists_and_does_not_contain_action_events_should_not_update_any_action_events()
        {
            // Arrange
            var order = new Order();
            
            // Act
            _actionEventReader.CompleteActions(order);

            // Assert
            Assert.AreEqual(0, _reswareDbContext.ActionEvents.Count());
        }

        [TestMethod]
        public void CompleteActions_should_return_order_exists_and_has_one_action_event_event_is_complete_should_not_update()
        {
            // Arrange
            var order = new Order { FileNumber = "123456" };

            _reswareDbContext.ActionEvents.Add(new ActionEvent {ActionCompleted = true, ActionCompletedDateTime = DateTime.Now, ActionEventCode = "1234", CreatedDateTime = DateTime.Now});
            _reswareDbContext.SaveChanges();

            // Act
            _actionEventReader.CompleteActions(order);
            
            // Assert
            Assert.AreEqual(1, _reswareDbContext.ActionEvents.Count());
            Assert.IsTrue(_reswareDbContext.ActionEvents.First().ActionCompleted);
        }

        [TestMethod]
        public void CompleteActions_should_return_order_exists_and_has_one_action_event_event_is_completed_and_updated()
        {
            // Arrange
            var order = new Order {FileNumber = "123456", ClientId = 1};

            _reswareDbContext.ActionEvents.Add(new ActionEvent {ActionEventCode = "123", FileNumber = "123456", ActionCompleted = false});
            _reswareDbContext.SaveChanges();

            // Act
            _actionEventReader.CompleteActions(order);

            // Assert
            Assert.AreEqual(1, _reswareDbContext.ActionEvents.Count());
            Assert.IsTrue(_reswareDbContext.ActionEvents.First().ActionCompleted);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Data.ActionEvent.Repository;
using Resware.Data.Context;

namespace Resware.Data.Test.ActionEvent.Repository.Test
{
    [TestClass]
    public class ActionEventRepositoryTest
    {
        private ActionEventRepository _actionEventRepository;
        private ReswareDbContext _reswareDbContext;
        private List<Entities.ActionEvents.ActionEvent> _actionEvents;


        [TestInitialize]
        public void Setup()
        {
            // Context
            //EffortProviderConfiguration.RegisterProvider();
            var connection = Effort.DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _reswareDbContext.ActionEvents.AddRange(_actionEvents = new List<Entities.ActionEvents.ActionEvent>
            {
                new Entities.ActionEvents.ActionEvent {ActionEventCode = "1"},
                new Entities.ActionEvents.ActionEvent {ActionEventCode = "2"},
                new Entities.ActionEvents.ActionEvent {ActionEventCode = "3"}
            });
            _reswareDbContext.SaveChanges();

            _actionEventRepository = new ActionEventRepository(_reswareDbContext);
        }

        [TestMethod]
        public void SaveNewActionEvent_should_return_negative_one_action_event_is_null()
        {
            // Act
            var result = _actionEventRepository.SaveNewActionEvent(null);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewActionEvent_should_add_valid_action_event_to_list_and_save_will_return_one()
        {
            // Arrange
            var actionEvent = new Entities.ActionEvents.ActionEvent
            {
                FileNumber = "TestFileNumber",
                ActionEventCode = "123"
            };

            // Act
            var result = _actionEventRepository.SaveNewActionEvent(actionEvent);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetAllActionEvents_should_return_an_empty_list()
        {
            // Arrange
            _reswareDbContext.ActionEvents.RemoveRange(_actionEvents);
            _reswareDbContext.SaveChanges();

            // Act
            var result = _actionEventRepository.GetAllActionEvents();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAllActionEvents_should_return_a_list_with_three_action_events_each_with_there_own_unique_action_event_code()
        {
            // Act
            var result = _actionEventRepository.GetAllActionEvents();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Exists(ae => string.Equals(ae.ActionEventCode, "1", StringComparison.CurrentCultureIgnoreCase)));
            Assert.IsTrue(result.Exists(ae => string.Equals(ae.ActionEventCode, "2", StringComparison.CurrentCultureIgnoreCase)));
            Assert.IsTrue(result.Exists(ae => string.Equals(ae.ActionEventCode, "3", StringComparison.CurrentCultureIgnoreCase)));
        }

        [TestMethod]
        public void UpdateActionEvent_should_return_negative_one_action_event_does_not_exist_with_given_id()
        {
            // Arrange
            var updatedActionEvent = new Entities.ActionEvents.ActionEvent {Id = Guid.NewGuid()};
            
            // Act
            var result = _actionEventRepository.UpdateActionEvent(updatedActionEvent);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void UpdateActionEvent_should_update_action_event_with_new_action_event_code_will_return_one()
        {
            // Arrange
            var actionEvent = _actionEventRepository.GetAllActionEvents().First(ae => ae.ActionEventCode.Equals("1"));
            actionEvent.ActionEventCode = "123";

            // Act
            var result = _actionEventRepository.UpdateActionEvent(actionEvent);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}

using ActionEventService.Managers;
using ActionEventService.Readers;
using Adeptive.ResWare.Services;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Data.ActionEvent.Repository;
using Resware.Data.Context;
using ReswareCommon.Messages;

namespace Resware.ActionEvent.WCF.Test.Managers.Test
{
    [TestClass]
    public class ActionEventManagerTest
    {
        private IActionEventManager _actionEventManager;
        private ActionEventRepository _actionEventRepository;
        private ActionEventReader _actionEventReaderMock;

        [TestInitialize]
        public void Setup()
        {
            _actionEventReaderMock = new ActionEventReader();

            EffortProviderConfiguration.RegisterProvider();
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var reswareDbContext = new ReswareDbContext(connection);
            _actionEventRepository = new ActionEventRepository(reswareDbContext);
            _actionEventManager = new ActionEventManager(_actionEventReaderMock, _actionEventRepository);
        }

        [TestMethod]
        public void PlaceActionEvent_passed_null_value_should_return_negative_one_result_with_exception_message()
        {
            // Act
            var result = _actionEventManager.PlaceActionEvent(null);

            // Assert
            Assert.AreEqual(-1, result.Result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("Exception"));
        }

        [TestMethod]
        public void PlaceActionEvent_passed_action_event_data_without_an_action_event_code_return_zero_result_with_action_event_code_is_null_message()
        {
            // Arrange
            var data = new ReceiveActionEventData {ActionEventCode = string.Empty, FileNumber = "1"};

            // Act
            var result = _actionEventManager.PlaceActionEvent(data);

            // Assert
            Assert.AreEqual(0, result.Result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.ActionEventCodeIsNull, result.Message);
        }

        [TestMethod]
        public void PlaceActionEvent_passed_action_data_without_a_file_number_return_zero_result_with_file_number_is_null_message()
        {
            // Arrange
            var data = new ReceiveActionEventData {ActionEventCode = "1", FileNumber = string.Empty};

            // Act
            var result = _actionEventManager.PlaceActionEvent(data);

            // Assert
            Assert.AreEqual(0, result.Result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.FileNumberIsNull, result.Message);
        }

        [TestMethod]
        public void PlaceActionEvent_passed_valid_action_event_data_return_result_with_result_of_one_and_no_message()
        {
            // Arrange
            var data = new ReceiveActionEventData {ActionEventCode = "100", FileNumber = "123456"};

            // Act
            var result = _actionEventManager.PlaceActionEvent(data);

            // Assert
            Assert.AreEqual(1, result.Result);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }
    }
}

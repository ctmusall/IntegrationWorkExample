using ActionEventService.Readers;
using Adeptive.ResWare.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Resware.ActionEvent.WCF.Test.Readers.Test
{
    [TestClass]
    public class ActionEventReaderTest
    {
        private ActionEventReader _actionEventReader;

        [TestInitialize]
        public void Setup()
        {
            _actionEventReader = new ActionEventReader();
        }

        [TestMethod]
        public void ParseInput_should_return_new_action_event_with_action_event_code_and_file_number()
        {
            // Arrange
            var data = new ReceiveActionEventData {ActionEventCode = "123", FileNumber = "123456"};
            
            // Act
            var result = _actionEventReader.ParseInput(data);

            // Assert
            Assert.AreEqual("123", result.ActionEventCode);
            Assert.AreEqual("123456", result.FileNumber);
        }
    }
}

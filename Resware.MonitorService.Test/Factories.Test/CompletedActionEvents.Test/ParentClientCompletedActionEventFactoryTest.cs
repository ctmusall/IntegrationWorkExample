using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReswareOrderMonitorService.Factories.CompletedActionEvents;
using ReswareOrderMonitorService.Factories.CompletedActionEvents.Solidifi;

namespace Resware.MonitorService.Test.Factories.Test.CompletedActionEvents.Test
{
    [TestClass]
    public class ParentClientCompletedActionEventFactoryTest
    {
        private IParentClientCompletedActionEventFactory _parentClientCompletedActionEventFactory;

        [TestInitialize]
        public void Setup()
        {
            _parentClientCompletedActionEventFactory = new ParentClientCompletedActionEventFactory();
        }

        [TestMethod]
        public void ResolveClientCompletedActionEventFactory_client_id_is_not_valid_should_return_null()
        {
            // Act
            var result = _parentClientCompletedActionEventFactory.ResolveClientCompletedActionEventFactory(0);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveClientCompletedActionEventFactory_client_id_matches_solidifi_client_id_should_return_solidifi_completed_action_event_factory()
        {
            // Act
            var result = _parentClientCompletedActionEventFactory.ResolveClientCompletedActionEventFactory(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiCompletedActionEventFactory));
        }
    }
}

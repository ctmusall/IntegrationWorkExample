using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Core.ActionEvent.Factories.ActionEvents;
using Resware.Core.ActionEvent.Factories.ParentActionEvents;

namespace Resware.MonitorService.Test.Factories.Test.ActionEvents.Test
{
    [TestClass]
    public class ParentActionEventFactoryTest
    {
        private IParentActionEventFactory _parentActionEventFactory;

        [TestInitialize]
        public void Setup()
        {
            _parentActionEventFactory = new ParentActionEventFactory();    
        }

        [TestMethod]
        public void ResolveActionEventFactory_should_return_solidifi_action_event_factory()
        {
            // Act
            var result = _parentActionEventFactory.ResolveActionEventFactory(1);

            // Assert
            Assert.AreEqual(typeof(SolidifiActionEventFactory), result.GetType());
        }

        [TestMethod]
        public void ResolveActionEventFactory_client_id_does_not_match_should_return_null()
        {
            // Act
            var result = _parentActionEventFactory.ResolveActionEventFactory(0);

            // Assert
            Assert.IsNull(result);
        }
    }
}

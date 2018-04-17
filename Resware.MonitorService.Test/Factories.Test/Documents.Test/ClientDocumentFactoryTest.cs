using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Core.Factories.DocumentFactory;
using Resware.Core.Factories.DocumentReaders;

namespace Resware.MonitorService.Test.Factories.Test.Documents.Test
{
    [TestClass]
    public class ClientDocumentFactoryTest
    {
        private ClientDocumentFactory _clientDocumentFactory;

        [TestInitialize]
        public void Setup()
        {
            _clientDocumentFactory = new ClientDocumentFactory();
        }

        [TestMethod]
        public void ResolveDocumentReaderFactory_client_id_is_not_valid_should_return_null()
        {
            // Act
            var result = _clientDocumentFactory.ResolveDocumentReaderFactory(0);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveDocumentReaderFactory_client_id_matches_solidifi_should_return_solidifi_document_reader_factory()
        {
            // Act
            var result = _clientDocumentFactory.ResolveDocumentReaderFactory(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SolidifiDocumentReaderFactory));
        }
    }
}

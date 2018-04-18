using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Core.DocumentSenders;
using Resware.Core.Factories.DocumentReaders;

namespace Resware.MonitorService.Test.Factories.Test.Documents.Test
{
    [TestClass]
    public class SolidifiDocumentReaderFactoryTest
    {
        private SolidifiDocumentReaderFactory _solidifiDocumentReaderFactory;

        [TestInitialize]
        public void Setup()
        {
            _solidifiDocumentReaderFactory = new SolidifiDocumentReaderFactory();
        }

        [TestMethod]
        public void ResolveDocumentSender_invalid_document_type_id_should_return_null()
        {
            // Act
            var result = _solidifiDocumentReaderFactory.ResolveDocumentSender(0);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ResolveDocumentSender_document_type_id_matches_disbursement_document_type_id_should_return_type_document_sender_with_disbursement_document_mail_utility()
        {
            // Act
            var result = _solidifiDocumentReaderFactory.ResolveDocumentSender(1022);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DocumentSender));
        }

        [TestMethod]
        public void ResolveDocumentSender_document_type_id_matches_disbursement_document_type_id_should_return_type_document_sender_with_closing_document_mail_utility()
        {
            // Act
            var result = _solidifiDocumentReaderFactory.ResolveDocumentSender(1618);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DocumentSender));
        }
    }
}

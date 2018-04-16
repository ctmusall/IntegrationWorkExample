using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Entities.Notes.Documents;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.DocumentSenders;
using ReswareOrderMonitorService.Utilities;

namespace Resware.MonitorService.Test.DocumentSenders.Test
{
    [TestClass]
    public class DocumentSenderTest
    {
        private DocumentSender _documentSender;
        private Mock<IDocumentMailUtility> _documentMailUtilityMock;
        private Document _document;
        private Order _order;
         
        [TestInitialize]
        public void Setup()
        {
            _documentMailUtilityMock = new Mock<IDocumentMailUtility>();
            _documentSender = new DocumentSender(_documentMailUtilityMock.Object);    
            _document = new Document();
            _order = new Order();
        }

        [TestMethod]
        public void SendDocs_build_message_and_send_to_internal_email_address_should_return_true()
        {
            // Arrange
            _documentMailUtilityMock.Setup(dmu => dmu.SendDocumentMailMessage(It.IsAny<MailMessage>())).Returns(true);
            _documentMailUtilityMock.Setup(dmu => dmu.BuildDocumentMailMessage(_document, _order)).Returns(new MailMessage());

            // Act
            var result = _documentSender.SendDocs(_document, _order);
            
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SendDocs_build_message_returns_null_mail_message_should_return_false()
        {
            // Act
            var result = _documentSender.SendDocs(_document, _order);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SendDocs_send_document_mail_message_does_not_send_successfully_should_return_false()
        {
            // Arrange
            _documentMailUtilityMock.Setup(dmu => dmu.BuildDocumentMailMessage(_document, _order)).Returns(new MailMessage());
            _documentMailUtilityMock.Setup(dmu => dmu.SendDocumentMailMessage(It.IsAny<MailMessage>())).Returns(false);

            // Act
            var result = _documentSender.SendDocs(_document, _order);

            // Assert
            Assert.IsFalse(result);
        }
    }
}

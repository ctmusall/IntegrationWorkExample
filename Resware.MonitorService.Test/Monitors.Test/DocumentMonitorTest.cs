using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Core.DocumentSenders;
using Resware.Core.Factories.DocumentFactory;
using Resware.Core.Factories.DocumentReaders;
using Resware.Core.Utilities.DocumentMail;
using Resware.Data.Context;
using Resware.Data.NoteDoc.Repository;
using Resware.Data.Order.Repository;
using Resware.Entities.Notes;
using Resware.Entities.Notes.Documents;
using Resware.Entities.Orders;
using ReswareOrderMonitorService.Monitors.Documents;

namespace Resware.MonitorService.Test.Monitors.Test
{
    [TestClass]
    public class DocumentMonitorTest
    {
        private DocumentMonitor _documentMonitor;
        private ReswareDbContext _reswareDbContext;
        private NoteDocRepository _noteDocRepository;
        private OrderRepository _orderRepository;
        private Mock<IClientDocumentFactory> _clientDocumentFactoryMock;
        private Mock<DocumentReaderFactory> _documentReaderFactoryMock;
        private Mock<IDocumentMailUtility> _documentMailUtilityMock; 
        private DocumentSender _documentSender;

        [TestInitialize]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _noteDocRepository = new NoteDocRepository(_reswareDbContext);
            _orderRepository = new OrderRepository(_reswareDbContext);
            _clientDocumentFactoryMock = new Mock<IClientDocumentFactory>();
            _documentReaderFactoryMock = new Mock<DocumentReaderFactory>();
            _documentMailUtilityMock = new Mock<IDocumentMailUtility>();
            _documentSender = new DocumentSender(_documentMailUtilityMock.Object);
            _documentMonitor = new DocumentMonitor(_noteDocRepository, _orderRepository, _clientDocumentFactoryMock.Object);    
        }

        [TestMethod]
        public void MonitorDocuments_no_note_docs_are_available_to_process_should_return_and_not_update_any_entities()
        {
            // Act
            _documentMonitor.MonitorDocuments();

            // Assert
            Assert.AreEqual(_reswareDbContext.Notes.Count(), 0);
        }

        [TestMethod]
        public void MonitorDocuments_note_doc_exists_and_corresponding_order_exists_should_resolve_solidifi_sender_send_and_update_note_doc_to_processed()
        {
            // Arrange
            _reswareDbContext.Notes.Add(new Note {CreatedDateTime = DateTime.Now, FileNumber = "123456", Processed = false, ProcessedDateTime = null, Documents = new List<Document> { new Document { Description = "123456", DocumentTypeId = 123, FileName = "Test.txt"} } });
            _reswareDbContext.Orders.Add(new Order {FileNumber = "123456", ClientId = 1});
            _reswareDbContext.SaveChanges();

            _documentMailUtilityMock.Setup(dmu => dmu.BuildDocumentMailMessage(It.IsAny<Document>(), It.IsAny<Order>())).Returns(new MailMessage());
            _documentMailUtilityMock.Setup(dmu => dmu.SendDocumentMailMessage(It.IsAny<MailMessage>())).Returns(true);
            _documentReaderFactoryMock.Setup(drf => drf.ResolveDocumentSender(123)).Returns(_documentSender);
            _clientDocumentFactoryMock.Setup(cdf => cdf.ResolveDocumentReaderFactory(1)).Returns(_documentReaderFactoryMock.Object);

            // Act
            _documentMonitor.MonitorDocuments();

            // Assert
            Assert.AreEqual(1, _reswareDbContext.Notes.Count());
            Assert.AreEqual(1, _reswareDbContext.Orders.Count());
            Assert.IsTrue(_reswareDbContext.Notes.First().Processed);
            Assert.IsNotNull(_reswareDbContext.Notes.First().ProcessedDateTime);
        }
    }
}

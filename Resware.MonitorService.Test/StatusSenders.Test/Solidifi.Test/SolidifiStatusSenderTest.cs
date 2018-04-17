using System.Linq;
using Aspose.Words;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resware.Core.Builders.StatusDocument;
using Resware.Core.Status.StatusSenders;
using Resware.Data.Context;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders;
using ReswareCommon.Constants;
using ReswareOrderMonitorService.StatusSenders.Solidifi;

namespace Resware.MonitorService.Test.StatusSenders.Test.Solidifi.Test
{
    [TestClass]
    public class SolidifiStatusSenderTest
    {
        private IStatusSender _solidifiStatusSender;
        private eClosings.Entities.Orders.Order _eClosingOrder;
        private OrderRepository _orderRepository;
        private ReswareDbContext _reswareDbContext;
        private Mock<IStatusDocumentBuilder> _statusDocumentBuilderMock;
        private Order _order;

        [TestInitialize]
        public void Setup()
        {
            _eClosingOrder = new eClosings.Entities.Orders.Order();
            _order = new Order();
            _statusDocumentBuilderMock = new Mock<IStatusDocumentBuilder>();
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _orderRepository = new OrderRepository(_reswareDbContext);
        }

        [TestMethod]
        public void SendStatusUpdate_should_build_document_and_update_closing_order_status()
        {
            // Arrange
            _solidifiStatusSender = new SolidifiStatusSender(_eClosingOrder, _statusDocumentBuilderMock.Object, new SolidifiUpdateClosingStatus(OrderStatusConstants.Scheduled, _orderRepository));
            _reswareDbContext.Orders.Add(_order);
            _reswareDbContext.SaveChanges();
            _statusDocumentBuilderMock.Setup(sdb => sdb.BuildDocument(It.IsAny<Order>(), It.IsAny<eClosings.Entities.Orders.Order>())).Returns(new Document());

            // Act
            _solidifiStatusSender.SendStatusUpdate(_order);

            // Assert
            Assert.AreEqual(1, _reswareDbContext.Orders.Count());
            Assert.AreEqual(OrderStatusConstants.Scheduled, _reswareDbContext.Orders.First().ClosingStatus);
        }

        [TestMethod]
        public void SendStatusUpdate_should_build_document_and_update_title_opinion_order_status()
        {
            // Arrange
            _solidifiStatusSender = new SolidifiStatusSender(_eClosingOrder, _statusDocumentBuilderMock.Object, new SolidifiUpdateTitleOpinionStatus(OrderStatusConstants.Scheduled, _orderRepository));
            _reswareDbContext.Orders.Add(_order);
            _reswareDbContext.SaveChanges();
            _statusDocumentBuilderMock.Setup(sdb => sdb.BuildDocument(It.IsAny<Order>(), It.IsAny<eClosings.Entities.Orders.Order>())).Returns(new Document());

            // Act
            _solidifiStatusSender.SendStatusUpdate(_order);

            // Assert
            Assert.AreEqual(1, _reswareDbContext.Orders.Count());
            Assert.AreEqual(OrderStatusConstants.Scheduled, _reswareDbContext.Orders.First().TitleOpinionStatus);
        }

        [TestMethod]
        public void SendStatusUpdate_should_build_document_and_update_doc_prep_order_status()
        {
            // Arrange
            _solidifiStatusSender = new SolidifiStatusSender(_eClosingOrder, _statusDocumentBuilderMock.Object, new SolidifiUpdateDocPrepStatus(OrderStatusConstants.Scheduled, _orderRepository));
            _reswareDbContext.Orders.Add(_order);
            _reswareDbContext.SaveChanges();
            _statusDocumentBuilderMock.Setup(sdb => sdb.BuildDocument(It.IsAny<Order>(), It.IsAny<eClosings.Entities.Orders.Order>())).Returns(new Document());

            // Act
            _solidifiStatusSender.SendStatusUpdate(_order);

            // Assert
            Assert.AreEqual(1, _reswareDbContext.Orders.Count());
            Assert.AreEqual(OrderStatusConstants.Scheduled, _reswareDbContext.Orders.First().DocPrepStatus);
        }

        [TestMethod]
        public void SendStatusUpdate_should_not_build_document_and_not_update_doc_prep_status()
        {
            // Arrange
            _order.DocPrepStatus = OrderStatusConstants.Pending;
            _solidifiStatusSender = new SolidifiStatusSender(_eClosingOrder, _statusDocumentBuilderMock.Object, new SolidifiUpdateDocPrepStatus(OrderStatusConstants.Scheduled, _orderRepository));
            _reswareDbContext.Orders.Add(_order);
            _reswareDbContext.SaveChanges();

            // Act
            _solidifiStatusSender.SendStatusUpdate(_order);

            // Assert
            Assert.AreEqual(1, _reswareDbContext.Orders.Count());
            Assert.AreEqual(OrderStatusConstants.Pending, _reswareDbContext.Orders.First().DocPrepStatus);
        }
    }
}

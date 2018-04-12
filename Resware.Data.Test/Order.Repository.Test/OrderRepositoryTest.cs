using System.Collections.Generic;
using System.Linq;
using Effort;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Data.Context;
using Resware.Data.Order.Repository;
using Resware.Entities.Orders.Addresses;
using Resware.Entities.Orders.BuyerSellers;

namespace Resware.Data.Test.Order.Repository.Test
{
    [TestClass]
    public class OrderRepositoryTest
    {
        private OrderRepository _orderRepository;
        private ReswareDbContext _reswareDbContext;

        [TestInitialize]
        public void Setup()
        {
            //EffortProviderConfiguration.RegisterProvider();
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _orderRepository = new OrderRepository(_reswareDbContext);    
        }

        [TestMethod]
        public void SaveNewOrder_should_return_negative_one_passed_in_null_order()
        {
            // Act
            var result = _orderRepository.SaveNewOrder(null, new PropertyAddress(), new List<BuyerSeller>(), new List<BuyerSellerAddress>());

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewOrder_should_return_negative_one_passed_in_null_property_address()
        {
            // Act
            var result = _orderRepository.SaveNewOrder(new Entities.Orders.Order(), null, new List<BuyerSeller>(), new List<BuyerSellerAddress>());

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewOrder_should_return_negative_one_passed_in_null_buyer_seller()
        {
            // Act
            var result = _orderRepository.SaveNewOrder(new Entities.Orders.Order(), new PropertyAddress(), null, new List<BuyerSellerAddress>());

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewOrder_should_return_negative_one_passed_in_null_buyer_seller_address()
        {
            // Act
            var result = _orderRepository.SaveNewOrder(new Entities.Orders.Order(), new PropertyAddress(), new List<BuyerSeller>(), null);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewOrder_should_return_negative_one_passed_in_invalid_file_number_in_order()
        {
            // Act
            var result = _orderRepository.SaveNewOrder(new Entities.Orders.Order(), new PropertyAddress(), new List<BuyerSeller>(), new List<BuyerSellerAddress>());

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewOrder_should_add_new_order_property_address_buyer_sellers_and_buyer_seller_addresses_should_return_two()
        {
            // Act
            var result = _orderRepository.SaveNewOrder(new Entities.Orders.Order {FileNumber = "123456"}, new PropertyAddress(), new List<BuyerSeller>(), new List<BuyerSellerAddress>());

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetAllOrders_should_return_new_empty_list_of_orders()
        {
            // Act
            var result = _orderRepository.GetAllOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAllOrders_should_return_list_of_one_order()
        {
            // Arrange
            _reswareDbContext.Orders.Add(new Entities.Orders.Order {FileNumber = "123456"});
            _reswareDbContext.SaveChanges();

            // Act
            var result = _orderRepository.GetAllOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Exists(o => string.Equals(o.FileNumber, "123456")));
        }

        [TestMethod]
        public void UpdateOrder_should_return_zero_order_attempted_to_update_does_not_exist()
        {
            // Arrange
            var order = new Entities.Orders.Order();

            // Act
            var result = _orderRepository.UpdateOrder(order);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void UpdateOrder_should_update_the_existing_order_with_new_value_should_return_one()
        {
            // Arrange
            var order = new Entities.Orders.Order {FileNumber = "123456", ClientId = 1234};
            _reswareDbContext.Orders.Add(order);
            _reswareDbContext.SaveChanges();

            var updatedOrder = _reswareDbContext.Orders.First();
            updatedOrder.LenderName = "New Name";

            // Act
            var result = _orderRepository.UpdateOrder(updatedOrder);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}

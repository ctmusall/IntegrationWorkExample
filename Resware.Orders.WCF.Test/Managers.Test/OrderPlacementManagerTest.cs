using System;
using Effort;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderPlacement.Factory;
using OrderPlacement.Managers;
using Resware.Data.Context;
using Resware.Data.Order.Repository;
using ReswareCommon.Messages;

namespace Resware.Orders.WCF.Test.Managers.Test
{
    [TestClass]
    public class OrderPlacementManagerTest
    {
        private ReswareReaderFactory _reswareReaderFactory;
        private OrderRepository _orderRepository;
        private IOrderPlacementManager _orderPlacementManager;

        [TestInitialize]
        public void Setup()
        {
            _reswareReaderFactory = new ReswareReaderFactory();
            //EffortProviderConfiguration.RegisterProvider();
            var connection = DbConnectionFactory.CreateTransient();
            var reswareDbContext = new ReswareDbContext(connection);
            _orderRepository = new OrderRepository(reswareDbContext);
            _orderPlacementManager = new OrderPlacementManager(_reswareReaderFactory, _orderRepository);
        }

        [TestMethod]
        public void PlaceOrder_passed_empty_file_number_should_return_a_zero_result_with_file_number_is_null_message()
        {
            // Act
            var result = _orderPlacementManager.PlaceOrder(0, string.Empty, new OrderPlacementServicePropertyAddress(), 0, DateTime.Now, new OrderPlacementServicePartner(), new OrderPlacementServiceBuyerSeller[0], new OrderPlacementServiceBuyerSeller[0], string.Empty, 0);

            // Assert
            Assert.AreEqual(0, result.Result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.FileNumberIsNull, result.Message);
        }

        [TestMethod]
        public void PlaceOrder_passed_null_property_address_should_return_a_zero_result_with_property_address_is_null_message()
        {
            // Act
            var result = _orderPlacementManager.PlaceOrder(0, "123456", null, 0, DateTime.Now, new OrderPlacementServicePartner(), new OrderPlacementServiceBuyerSeller[0], new OrderPlacementServiceBuyerSeller[0], string.Empty, 0);

            // Assert
            Assert.AreEqual(0, result.Result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Message));
            Assert.AreEqual(ValidationMessages.PropertyAddressIsNull, result.Message);
        }

        [TestMethod]
        public void PlaceOrder_passed_valid_data_and_parsed_into_new_order_property_address_without_any_buyer_or_sellers_should_return_two_with_no_message()
        {
            // Act
            var result = _orderPlacementManager.PlaceOrder(1, "123456", new OrderPlacementServicePropertyAddress(), 11, DateTime.Now, new OrderPlacementServicePartner(), null, null, "Note!", 2);

            // Assert
            Assert.AreEqual(2, result.Result);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }

        [TestMethod]
        public void PlaceOrder_passed_valid_data_and_parsed_into_new_order_property_address_buyer_and_sellers_should_return_six_with_no_message()
        {
            // Act
            var result = _orderPlacementManager.PlaceOrder(1, "123456", new OrderPlacementServicePropertyAddress(), 11, DateTime.Now, new OrderPlacementServicePartner(), new[] { new OrderPlacementServiceBuyerSeller()}, new[] {new OrderPlacementServiceBuyerSeller()}, "Note!", 2);
            
            // Assert
            Assert.AreEqual(6, result.Result);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }
    }
}

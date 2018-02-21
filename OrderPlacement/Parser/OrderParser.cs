using System.Collections.Generic;
using System.Linq;
using OrderPlacement.Models;

namespace OrderPlacement.Parser
{
    public class OrderParser : IOrderParser
    {
        public ICollection<OrderResult> ParseOrderResults(ICollection<Order> orders)
        {
            return orders.Select(order => new OrderResult
            {
                Id = order.Id,
                FileNumber = order.FileNumber,
                CustomerId = order.CustomerId,
                BuyersAndSellers = ParseBuyersAndSellers(order.BuyerAndSellers),
                ClosingDateTime = order.ClosingDateTime,
                CreatedDateTime = order.CreatedDateTime,
                CustomerContact = order.CustomerContact,
                DeliveryMethod = order.DeliveryMethod,
                LenderName = order.LenderName,
                Processed = order.Processed,
                Product = order.Product,
                ProcessedDateTime = order.ProcessedDateTime,
                PropertyAddress = ParsePropertyAddress(order.PropertyAddress)
            }).ToList();
        }

        private static ICollection<PropertyAddressResult> ParsePropertyAddress(IEnumerable<PropertyAddress> propertyAddresses)
        {
            return propertyAddresses.Select(propertyAddress => new PropertyAddressResult
            {
                Id = propertyAddress.Id,
                StreetNumber = propertyAddress.StreetNumber,
                StreetDirection = propertyAddress.StreetDirection,
                StreetName = propertyAddress.StreetName,
                StreetSuffix = propertyAddress.StreetSuffix,
                Unit = propertyAddress.Unit,
                Zip = propertyAddress.Zip,
                City = propertyAddress.City,
                State = propertyAddress.State,
                AddressStreetInfo = propertyAddress.AddressStreetInfo,
                Description = propertyAddress.Description,
                County = propertyAddress.County
            }).ToList();
        }

        private static ICollection<BuyerSellerResult> ParseBuyersAndSellers(IEnumerable<BuyerSeller> buyersAndSellers)
        {
            return buyersAndSellers.Select(buyerAndSeller => new BuyerSellerResult
            {
                Id = buyerAndSeller.Id,
                OrderId = buyerAndSeller.OrderId,
                Prefix = buyerAndSeller.Prefix,
                FirstName = buyerAndSeller.FirstName,
                MiddleName = buyerAndSeller.MiddleName,
                LastName = buyerAndSeller.LastName,
                Suffix = buyerAndSeller.Suffix,
                EntityName = buyerAndSeller.EntityName,
                MaritalStatus = buyerAndSeller.MaritalStatus,
                Phone = buyerAndSeller.Phone,
                Email = buyerAndSeller.Email,
                Spouse = buyerAndSeller.Spouse,
                Type = buyerAndSeller.Type,
                Address = ParseBuyerAndSellerAddress(buyerAndSeller.Address)
            }).ToList();
        }

        private static ICollection<BuyerSellerAddressResult> ParseBuyerAndSellerAddress(IEnumerable<BuyerSellerAddress> buyerSellerAddresses)
        {
            return buyerSellerAddresses.Select(buyerSellerAddress => new BuyerSellerAddressResult
            {
                BuyerSellerId = buyerSellerAddress.BuyerSellerId,
                Id = buyerSellerAddress.Id,
                StreetNumber = buyerSellerAddress.StreetNumber,
                StreetDirection = buyerSellerAddress.StreetDirection,
                StreetName = buyerSellerAddress.StreetName,
                StreetSuffix = buyerSellerAddress.StreetSuffix,
                Unit = buyerSellerAddress.Unit,
                Zip = buyerSellerAddress.Zip,
                City = buyerSellerAddress.City,
                State = buyerSellerAddress.State,
                AddressStreetInfo = buyerSellerAddress.AddressStreetInfo,
                Description = buyerSellerAddress.Description
            }).ToList();
        }
    }
}
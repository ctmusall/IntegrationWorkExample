using System.Collections.Generic;
using System.Linq;
using OrderPlacement.Models;
using OrderPlacement.Parser;

namespace OrderPlacement.Parsers
{
    public class OrderResultParser : IOrderResultParser
    {
        public ICollection<OrderResult> ParseAllOrderResults(ICollection<Order> orders)
        {
            return orders.Select(order => new OrderResult
            {
                Id = order.Id,
                FileNumber = order.FileNumber,
                CustomerId = order.CustomerId,
                BuyersAndSellers = ParseBuyersAndSellersResult(order.BuyerAndSellers),
                ClosingDateTime = order.ClosingDateTime,
                CreatedDateTime = order.CreatedDateTime,
                Notes = order.Notes,
                ClosingStatus = order.ClosingStatus,
                TitleOpinionStatus = order.TitleOpinionStatus,
                DocPrepStatus = order.DocPrepStatus,
                ClientId = order.ClientId,
                CustomerProduct = order.CustomerProduct,
                LenderName = order.LenderName,
                Product = order.Product,
                PropertyAddress = ParsePropertyAddressResult(order.PropertyAddress)
            }).ToList();
        }

        public OrderResult ParseOrderResult(Order order)
        {
            return new OrderResult
            {
                Id = order.Id,
                FileNumber = order.FileNumber,
                CustomerId = order.CustomerId,
                BuyersAndSellers = ParseBuyersAndSellersResult(order.BuyerAndSellers),
                ClosingDateTime = order.ClosingDateTime,
                CreatedDateTime = order.CreatedDateTime,
                LenderName = order.LenderName,
                Product = order.Product,
                CustomerProduct = order.CustomerProduct,
                ClientId = order.ClientId,
                Notes = order.Notes,
                ClosingStatus = order.ClosingStatus,
                TitleOpinionStatus = order.TitleOpinionStatus,
                DocPrepStatus = order.DocPrepStatus,
                PropertyAddress = ParsePropertyAddressResult(order.PropertyAddress)
            };
        }

        private static ICollection<PropertyAddressResult> ParsePropertyAddressResult(IEnumerable<PropertyAddress> propertyAddresses)
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
                County = propertyAddress.County,
                OrderId = propertyAddress.OrderId
            }).ToList();
        }

        private static ICollection<BuyerSellerResult> ParseBuyersAndSellersResult(IEnumerable<BuyerSeller> buyersAndSellers)
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
                Address = ParseBuyerAndSellerAddressResult(buyerAndSeller.Address)
            }).ToList();
        }

        private static ICollection<BuyerSellerAddressResult> ParseBuyerAndSellerAddressResult(IEnumerable<BuyerSellerAddress> buyerSellerAddresses)
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
                Description = buyerSellerAddress.Description,
                County = buyerSellerAddress.County
            }).ToList();
        }
    }
}
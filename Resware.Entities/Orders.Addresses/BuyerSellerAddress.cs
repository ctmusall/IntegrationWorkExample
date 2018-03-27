using System;
using System.ComponentModel.DataAnnotations.Schema;
using Resware.Entities.Orders.BuyerSellers;

namespace Resware.Entities.Orders.Addresses
{
    public class BuyerSellerAddress : Address
    {
        [ForeignKey("BuyerSeller")]
        public Guid BuyerSellerId { get; set; }
        public virtual BuyerSeller BuyerSeller { get; set; }
    }
}
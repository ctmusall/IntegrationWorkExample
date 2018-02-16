using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderPlacement.Models
{
    public class BuyerSellerAddress : Address
    {
        [ForeignKey("BuyerSeller")]
        public Guid BuyerSellerId { get; set; }

        public virtual BuyerSeller BuyerSeller { get; set; }
    }
}
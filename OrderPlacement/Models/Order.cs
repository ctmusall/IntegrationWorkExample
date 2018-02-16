using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderPlacement.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FileNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string LenderName { get; set; }
        public DateTime? ClosingDateTime { get; set; }
        public string DeliveryMethod { get; set; }
        public string Product { get; set; }
        public string CustomerContact { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool Processed { get; set; }
        public DateTime? ProcessedDateTime { get; set; }

        public virtual ICollection<PropertyAddress> PropertyAddress { get; set; }
        public virtual ICollection<BuyerSeller> BuyerAndSellers { get; set; }
    }
}
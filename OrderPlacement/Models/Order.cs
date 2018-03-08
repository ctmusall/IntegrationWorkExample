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
        public string CustomerId { get; set; }
        public string LenderName { get; set; }
        public DateTime? ClosingDateTime { get; set; }
        public string Product { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Notes { get; set; }
        public int ClientId { get; set; }
        public string CustomerProduct { get; set; }
        public string ClosingStatus { get; set; }
        public string TitleOpinionStatus { get; set; }
        public string DocPrepStatus { get; set; }

        public virtual ICollection<PropertyAddress> PropertyAddress { get; set; }
        public virtual ICollection<BuyerSeller> BuyerAndSellers { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderPlacement.Models
{
    internal class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        internal Guid Id { get; set; }
        internal int ClientId { get; set; }
        internal string FileNumber { get; set; }
        internal string CustomerName { get; set; }
        internal string CustomerId { get; set; }
        internal string LenderName { get; set; }
        internal DateTime? ClosingDateTime { get; set; }
        internal string DeliveryMethod { get; set; }
        internal virtual PropertyAddress PropertyAddress { get; set; }

        // TODO - Product
        // TODO - CustomerContact
    }
}
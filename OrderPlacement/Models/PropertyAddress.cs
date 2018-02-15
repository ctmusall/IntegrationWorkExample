using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderPlacement.Models
{
    internal class PropertyAddress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        internal Guid Id { get; set; }
        [ForeignKey("Order")]
        internal Guid OrderId { get; set; }

        internal string StreetNumber { get; set; }
        internal string StreetDirection { get; set; }
        internal string StreetName { get; set; }
        internal string StreetSuffix { get; set; }
        internal string Unit { get; set; }
        internal string Zip { get; set; }
        internal string City { get; set; }
        internal string State { get; set; }
        internal string AddressStreetInfo { get; set; }
        internal string Description { get; set; }
        internal string County { get; set; }
        internal Order Order { get; set; }
    }
}
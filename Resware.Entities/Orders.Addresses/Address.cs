using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resware.Entities.Orders.Addresses
{
    public abstract class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string StreetNumber { get; set; }
        public string StreetDirection { get; set; }
        public string StreetName { get; set; }
        public string StreetSuffix { get; set; }
        public string Unit { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AddressStreetInfo { get; set; }
        public string Description { get; set; }
        public string County { get; set; }
    }
}
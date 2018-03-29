using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderPlacement.Common;
using Resware.Entities.Orders.Addresses;

namespace Resware.Entities.Orders.BuyerSellers
{
    public class BuyerSeller
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string EntityName { get; set; }
        public string MaritalStatus { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Spouse { get; set; }
        public BuyerSellerEnum Type { get; set; }

        public virtual Order Order { get; set; }
        public virtual ICollection<BuyerSellerAddress> Address { get; set; }
    }
}
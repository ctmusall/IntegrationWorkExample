using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resware.Entities.Orders.Addresses
{
    public class PropertyAddress : Address
    {
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
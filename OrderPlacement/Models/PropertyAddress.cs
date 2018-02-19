using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderPlacement.Models
{
    public class PropertyAddress : Address
    {
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }
        public string County { get; set; }
    }
}
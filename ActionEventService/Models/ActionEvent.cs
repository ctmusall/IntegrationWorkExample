using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActionEventService.Models
{
    public class ActionEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FileNumber { get; set; }
        public string ActionEventCode { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
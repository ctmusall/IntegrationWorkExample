using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resware.Entities.ActionEvents
{
    public class ActionEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FileNumber { get; set; }
        public string ActionEventCode { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool ActionCompleted { get; set; }
        public DateTime? ActionCompletedDateTime { get; set; }
    }
}
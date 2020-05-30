using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LostArkEng.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("EventType")]
        [Required]
        public int TypeId { get; set; }
        public int MinGearScore { get; set; }
        public bool isActive { get; set; }

        [NotMapped]
        public virtual EventType EventType { get; set; }
    }
}

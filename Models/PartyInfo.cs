using LostArkEng.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LostArkEng.Models.ApplicationUser;

namespace LAE.Models
{
    public class PartyInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ActivityType")]
        [Required]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        [Required]
        public ApplicationUser CreatedBy { get; set; }
        public DateTime StartingTime { get; set; } = DateTime.UtcNow;
        [Required]
        public int PartyMembers { get; set; }
        public int PartySize { get; set; }

        public string TestColumn { get; set; }
        public bool isActive { get; set; }
        [ForeignKey("Server")]
        public ServerName ServerName { get; set; }
        [NotMapped]
        public virtual ServerName Server { get; set; }

        public virtual ICollection<Party> Members { get; set; }

    }
}

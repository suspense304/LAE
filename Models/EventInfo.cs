using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static LostArkEng.Models.ApplicationUser;

namespace LostArkEng.Models
{
    public class EventInfo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ActivityType")]
        [Required]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }


        [Required]
        public ApplicationUser CreatedBy { get; set; }
        public ApplicationUser MemberTwo { get; set; } = null!;
        public ApplicationUser MemberThree { get; set; } = null!;
        public ApplicationUser MemberFour { get; set; } = null!;
        public DateTime StartingTime { get; set; } = DateTime.UtcNow;
        public bool isActive { get; set; }
        [ForeignKey("Server")]
        public ServerName ServerName { get; set; }
        

        [NotMapped]
        public virtual ServerName Server { get; set; }
    }
}

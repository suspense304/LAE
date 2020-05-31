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
        public int Id { get; set; }

        [ForeignKey("Activities")]
        [Required]
        public int ActivityId { get; set; }
        [Required]
        public ApplicationUser CreatedBy { get; set; }
        public ApplicationUser MemberTwo { get; set; } = null!;
        public ApplicationUser MemberThree { get; set; } = null!;
        public ApplicationUser MemberFour { get; set; } = null!;
        public DateTime StartingTime { get; set; } = DateTime.Now.AddMinutes(30);
        public bool isActive { get; set; }
        [ForeignKey("Server")]
        public ServerName ServerName { get; set; }

        [NotMapped]
        public virtual Activity ActivityInfo { get; set; }

        [NotMapped]
        public virtual ICollection<Activity> Activities { get; set; }

        [NotMapped]
        public virtual ServerName Server { get; set; }
    }
}

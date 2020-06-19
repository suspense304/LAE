using LostArkEng.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAE.Models
{
    public class Party
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("PartyInfo")]
        [Required]
        public int PartyId { get; set; }

        public PartyInfo PartyInfo { get; set; }

        [Required]
        public ApplicationUser PartyName { get; set; }
    }
}

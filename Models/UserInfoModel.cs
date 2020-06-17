using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LAE.Models
{
    public class UserInfoModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Server")]
        public int ServerName { get; set; }

        [Required]
        [Display(Name = "Discord Name")]
        public string DiscordName { get; set; }

        [Required]
        [Display(Name = "Character Name")]
        public string CharacterName { get; set; }

        [Required]
        [Display(Name = "Character Class")]
        public int CharClass { get; set; }

        [Required]
        [Display(Name = "Gear Score")]
        [Range(0, 1300)]
        public double GearScore { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}

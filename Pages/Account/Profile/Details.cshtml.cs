using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;

namespace LAE.Pages.Account.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DetailsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ApplicationUser AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser = await _userManager.FindByIdAsync(id);

            if (AppUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

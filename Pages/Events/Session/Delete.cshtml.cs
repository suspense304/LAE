using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;
using LAE.Models;

namespace LAE.Pages.Events.Events
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteModel(LAE.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }

        [BindProperty]
        public PartyInfo PartyInfo { get; set; }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            LoggedInUser = await GetCurrentUser();
            if (id == null)
            {
                return NotFound();
            }

            PartyInfo = await _context.PartyInfo.FirstOrDefaultAsync(m => m.Id == id);

            if (PartyInfo == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PartyInfo = await _context.PartyInfo.FindAsync(id);

            if (PartyInfo != null)
            {
                _context.PartyInfo.Remove(PartyInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

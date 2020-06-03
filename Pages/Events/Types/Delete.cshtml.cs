using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;

namespace LAE.Pages.Events.Types
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }
        public DeleteModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        [BindProperty]
        public EventType EventType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            LoggedInUser = await GetCurrentUser();
            if (id == null)
            {
                return NotFound();
            }

            EventType = await _context.EventType.FirstOrDefaultAsync(m => m.TypeId == id);

            if (EventType == null)
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

            EventType = await _context.EventType.FindAsync(id);

            if (EventType != null)
            {
                _context.EventType.Remove(EventType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./List");
        }
    }
}

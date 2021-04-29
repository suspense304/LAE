using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;

namespace LAE.Pages.Events.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }
        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Activities = _context.EventType.ToList();
            _userManager = userManager;
        }


        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        

        public async Task<IActionResult> OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
            return Page();
        }

        [BindProperty]
        public Activity Activity { get; set; }
        [BindProperty]
        public List<EventType> Activities { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Activity newAct = new Activity();
            newAct.TypeId = Activity.EventType.TypeId;
            newAct.MinGearScore = Activity.MinGearScore;
            newAct.isActive = true;
            newAct.Name = Activity.Name;
            newAct.PartySize = Activity.PartySize;

            _context.Actvity.Add(newAct);

            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

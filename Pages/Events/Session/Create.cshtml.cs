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

namespace LAE.Pages.Events.Events
{
    public class CreateModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateModel(LAE.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            Activities = _context.Actvity.ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public int GearScore { get; set; }

        [BindProperty]
        public EventInfo EventInfo { get; set; }

        [BindProperty]
        public Activity Activity { get; set; }
        [BindProperty]
        public List<Activity> Activities { get; set; }


        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await GetCurrentUser();

            EventInfo newEvent = new EventInfo();
            newEvent.ActivityId = Activity.Id;
            newEvent.CreatedBy = user;
            newEvent.isActive = EventInfo.isActive;
            newEvent.StartingTime = EventInfo.StartingTime;
            newEvent.ServerName = user.Server;

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.EventInfo.Add(newEvent);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LAE.Data;
using LostArkEng.Models;

namespace LAE.Pages.Events.Events
{
    public class CreateModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;
        

        public CreateModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.Id = "0";
            applicationUser.ItemLevel = 709;
            applicationUser.DiscordName = "Suspense";

            EventInfo newEvent = new EventInfo();
            newEvent.ActivityId = Activity.Id;
            newEvent.CreatedBy = applicationUser;
            newEvent.isActive = EventInfo.isActive;
            newEvent.StartingTime = EventInfo.StartingTime;



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

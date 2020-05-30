using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LAE.Data;
using LostArkEng.Models;

namespace LAE.Pages.Events.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
            Activities = _context.EventType.ToList();
        }
        

        public IActionResult OnGet()
        {
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
            newAct.isActive = Activity.isActive;
            newAct.Name = Activity.Name;

            _context.Actvity.Add(newAct);

            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

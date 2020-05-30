using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAE.Data;
using LostArkEng.Models;

namespace LAE.Pages.Events.Sessions
{
    public class EditModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public EditModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
            Activities = _context.EventType.ToList();
        }

        [BindProperty]
        public Activity Activity { get; set; }
        [BindProperty]
        public List<EventType> Activities { get; set; }

        [BindProperty]
        public EventType EventType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Activity = await _context.Actvity.FirstOrDefaultAsync(m => m.Id == id);
            EventType = await _context.EventType.Where(w => w.TypeId == Activity.TypeId).FirstOrDefaultAsync();

            if (Activity == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Activity.TypeId = Activity.TypeId;
            _context.Attach(Activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(Activity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ActivityExists(int id)
        {
            return _context.Actvity.Any(e => e.Id == id);
        }
    }
}

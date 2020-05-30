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

namespace LAE.Pages.Events.Types
{
    public class EditModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public EditModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EventType EventType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EventType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTypeExists(EventType.TypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./List");
        }

        private bool EventTypeExists(int id)
        {
            return _context.EventType.Any(e => e.TypeId == id);
        }
    }
}

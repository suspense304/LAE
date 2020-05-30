using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LAE.Data;
using LostArkEng.Models;

namespace LAE.Pages.Events.Sessions
{
    public class DetailsModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public DetailsModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Activity Activity { get; set; }

        public EventType Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Activity = await _context.Actvity.FirstOrDefaultAsync(m => m.Id == id);
            Event = await _context.EventType.Where(m => m.TypeId == Activity.TypeId).FirstOrDefaultAsync();

            if (Activity == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

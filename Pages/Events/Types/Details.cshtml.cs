using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LAE.Data;
using LostArkEng.Models;

namespace LAE.Pages.Events.Types
{
    public class DetailsModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public DetailsModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}

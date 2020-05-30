using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LAE.Data;
using LostArkEng.Models;

namespace LAE.Pages.Events.Events
{
    public class DetailsModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public DetailsModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public EventInfo EventInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EventInfo = await _context.EventInfo.FirstOrDefaultAsync(m => m.Id == id);

            if (EventInfo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

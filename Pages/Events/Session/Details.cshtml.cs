using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LAE.Data;
using LAE.Models;
using LostArkEng.Models;

namespace LAE.Pages.Events.Session
{
    public class DetailsModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public DetailsModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public PartyInfo PartyInfo { get; set; }

        public IList<ApplicationUser> Members { get; set; }

        public ApplicationUser Creator { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PartyInfo = await _context.PartyInfo
                .Include(c => c.CreatedBy)
                .Include(p => p.Activity)
                .Include(x => x.Members).FirstOrDefaultAsync(m => m.Id == id);

            Creator = PartyInfo.CreatedBy;
            Members = await _context.Party.AsQueryable().Where(w => w.PartyId == id).Select(s => s.PartyName).ToListAsync();

            if (PartyInfo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

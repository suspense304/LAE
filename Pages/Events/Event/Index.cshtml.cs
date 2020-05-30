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
    public class IndexModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public IndexModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Activity> Activity { get;set; }

        public async Task OnGetAsync()
        {
            Activity = await _context.Actvity.ToListAsync();
        }
    }
}

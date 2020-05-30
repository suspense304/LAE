using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;

namespace LAE.Pages.Events.Events
{
    public class IndexModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public IndexModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EventInfo> EventInfo { get;set; }

        public async Task OnGetAsync()
        {
            EventInfo = await _context.EventInfo.ToListAsync();
        }
    }
}

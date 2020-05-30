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
    public class ListModel : PageModel
    {
        private readonly LAE.Data.ApplicationDbContext _context;

        public ListModel(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EventType> EventType { get;set; }

        public async Task OnGetAsync()
        {
            EventType = await _context.EventType.ToListAsync();
        }
    }
}

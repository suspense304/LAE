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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }

        public IndexModel(LAE.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
        public IList<EventInfo> EventInfo { get; set; }

        [BindProperty]
        public EventInfo SelectedEvent { get; set; }

        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
            EventInfo = await _context.EventInfo.Include(x => x.MemberTwo).Include(x => x.MemberThree).Include(x => x.MemberFour).Include(x => x.CreatedBy).ToListAsync();
        }

    }
}

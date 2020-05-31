using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAE.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private EventInfo SelectedEvent { get; set; }
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
        public EventController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> JoinTeamAsync(int? Id, int? slot)
        {
            ApplicationUser user = await GetCurrentUser();
            SelectedEvent = await _context.EventInfo.FirstOrDefaultAsync(m => m.Id == Id);

            switch (slot)
            {
                case 2:
                    SelectedEvent.MemberTwo = user;
                    break;
                case 3:
                    SelectedEvent.MemberThree = user;
                    break;
                case 4:
                    SelectedEvent.MemberFour = user;
                    break;
            }

            _context.Attach(SelectedEvent).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
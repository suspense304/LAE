using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using LAE.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace LAE.Pages.Events.Events
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DateTime CurrentTime;

        private EventInfo SelectedEvent { get; set; }

        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
        public IList<EventInfo> EventInfo { get; set; }

        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
            CurrentTime = DateTime.Now;
            
            EventInfo = await _context.EventInfo.Include(x => x.MemberTwo)
                                                .Include(x => x.MemberThree)
                                                .Include(x => x.MemberFour)
                                                .Include(x => x.CreatedBy)
                                                .Include(x => x.Activity)
                                                .Where(w => w.isActive && w.Activity.MinGearScore <= LoggedInUser.ItemLevel)
                                                .OrderBy(w => w.StartingTime)
                                                .ToListAsync();

            var result = _context.Database.ExecuteSqlRawAsync("EXECUTE dbo.updateEvents @timeNow={0}", CurrentTime);
        }

        public async Task<IActionResult> OnPostJoinTeam(int? id, int? slot, string? option)
        {
            ApplicationUser user = await GetCurrentUser();
            DiscordSender discord = new DiscordSender(717523711694995487, "jtf8VSXM6ht8H9JlRG9gkBWcgHijFRe9TXISgOU0bO4qtQr8oaVYaJaRmPiNweMFehS0", CultureInfo.CurrentCulture);
            SelectedEvent = await _context.EventInfo.Include(x => x.MemberTwo)
                                                    .Include(x => x.MemberThree)
                                                    .Include(x => x.MemberFour)
                                                    .Include(x => x.CreatedBy)
                                                    .Include(x => x.Activity)
                                                    .AsQueryable().FirstOrDefaultAsync(m => m.Id == id);

            if(option == "Join")
            {
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

                if(SelectedEvent.MemberTwo != null &
                   SelectedEvent.MemberThree != null &
                   SelectedEvent.MemberFour != null)
                {
                    string MemberTwo = (SelectedEvent.MemberTwo == null) ? "TestMember" : SelectedEvent.MemberTwo.DiscordName;
                    string MemberThree = (SelectedEvent.MemberThree == null) ? "TestMember" : SelectedEvent.MemberThree.DiscordName;
                    string MemberFour = (SelectedEvent.MemberFour == null) ? "TestMember" : SelectedEvent.MemberFour.DiscordName;
                    string EventName = _context.Actvity.AsQueryable().Where(m => m.Id == SelectedEvent.ActivityId).FirstOrDefault().Name;

                    discord.Emit(SelectedEvent.CreatedBy.DiscordName, MemberTwo, MemberThree, MemberFour, EventName);
                }
                

                await _context.SaveChangesAsync();
            }

            if(option == "Leave")
            {
                switch (slot)
                {
                    case 2:
                        SelectedEvent.MemberTwo = null;
                        break;
                    case 3:
                        SelectedEvent.MemberThree = null;
                        break;
                    case 4:
                        SelectedEvent.MemberFour = null;
                        break;
                }

                _context.Attach(SelectedEvent).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            

            return RedirectToPage("./Index");
        }

    }
}

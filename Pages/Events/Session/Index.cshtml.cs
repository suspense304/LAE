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
        public DateTime ServerTime;

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


        public DateTime ConvertTime(DateTime dbTime)
        {
            DateTime local = DateTime.UtcNow;
            DateTime server = DateTime.UtcNow.AddHours(3);
            decimal diff = (int)(server.Subtract(local).TotalMinutes);
            dbTime = dbTime.AddMinutes(-(double)diff);
            return new DateTime(dbTime.Year, dbTime.Month, dbTime.Day, dbTime.Hour, dbTime.Minute, dbTime.Second);
        }
        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
            CurrentTime = DateTime.Now;
            ServerTime = DateTime.UtcNow.AddHours(3);

            if (LoggedInUser != null)
            {
                EventInfo = await _context.EventInfo.Include(x => x.MemberTwo)
                                                .Include(x => x.MemberThree)
                                                .Include(x => x.MemberFour)
                                                .Include(x => x.CreatedBy)
                                                .Include(x => x.Activity)
                                                .Where(w => w.isActive && w.Activity.MinGearScore <= LoggedInUser.ItemLevel)
                                                .OrderBy(w => w.StartingTime)
                                                .ToListAsync();
            }


            var result = _context.Database.ExecuteSqlRawAsync("EXECUTE dbo.updateEvents @timeNow={0}", ServerTime);
        }

        public async Task<IActionResult> OnPostJoinTeam(int? id, int? slot, string? option)
        {
            ApplicationUser user = await GetCurrentUser();
            
            SelectedEvent = await _context.EventInfo.Include(x => x.MemberTwo)
                                                    .Include(x => x.MemberThree)
                                                    .Include(x => x.MemberFour)
                                                    .Include(x => x.CreatedBy)
                                                    .Include(x => x.Activity)
                                                    .AsQueryable().FirstOrDefaultAsync(m => m.Id == id);

            if (option == "Join")
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

                if (SelectedEvent.MemberTwo != null &
                   SelectedEvent.MemberThree != null &
                   SelectedEvent.Activity.TypeId == 3)
                {
                    DiscordSender discord = new DiscordSender(717523711694995487, "jtf8VSXM6ht8H9JlRG9gkBWcgHijFRe9TXISgOU0bO4qtQr8oaVYaJaRmPiNweMFehS0", CultureInfo.CurrentCulture);
                    string MemberTwo = SelectedEvent.MemberTwo.DiscordName;
                    string MemberThree = SelectedEvent.MemberThree.DiscordName;
                    string EventName = _context.Actvity.AsQueryable().Where(m => m.Id == SelectedEvent.ActivityId).FirstOrDefault().Name;

                    SelectedEvent.isActive = false;
                    discord.Emit(SelectedEvent.CreatedBy.DiscordName, MemberTwo, MemberThree, EventName);
                }

                else if (SelectedEvent.MemberTwo != null &
                   SelectedEvent.MemberThree != null &
                   SelectedEvent.MemberFour != null)
                {
                    DiscordSender discord = new DiscordSender(717523711694995487, "jtf8VSXM6ht8H9JlRG9gkBWcgHijFRe9TXISgOU0bO4qtQr8oaVYaJaRmPiNweMFehS0", CultureInfo.CurrentCulture);
                    string MemberTwo = SelectedEvent.MemberTwo.DiscordName;
                    string MemberThree = SelectedEvent.MemberThree.DiscordName;
                    string MemberFour = SelectedEvent.MemberFour.DiscordName;
                    string EventName = _context.Actvity.AsQueryable().Where(m => m.Id == SelectedEvent.ActivityId).FirstOrDefault().Name;

                    SelectedEvent.isActive = false;
                    discord.Emit(SelectedEvent.CreatedBy.DiscordName, MemberTwo, MemberThree, MemberFour, EventName);
                }

                else if(SelectedEvent.MemberTwo != null & SelectedEvent.MemberThree != null)
                {
                    DiscordSender discordOpen = new DiscordSender(722102560722386975, "1Ty2jGrVK46OuSlFwOrp82tCjFIO8ngAhG6TiDQHOw63s7innCp8K654KQIKQLN77fBO", CultureInfo.CurrentCulture);
                    string MemberTwo = SelectedEvent.MemberTwo.DiscordName;
                    string MemberThree = SelectedEvent.MemberThree.DiscordName;
                    string EventName = _context.Actvity.AsQueryable().Where(m => m.Id == SelectedEvent.ActivityId).FirstOrDefault().Name;
                    discordOpen.EmitOpenGroup(SelectedEvent.CreatedBy.DiscordName, MemberTwo, MemberThree, EventName);
                }
                else
                {
                    DiscordSender discordOpen = new DiscordSender(722102560722386975, "1Ty2jGrVK46OuSlFwOrp82tCjFIO8ngAhG6TiDQHOw63s7innCp8K654KQIKQLN77fBO", CultureInfo.CurrentCulture);
                    string MemberTwo = SelectedEvent.MemberTwo.DiscordName;
                    string EventName = _context.Actvity.AsQueryable().Where(m => m.Id == SelectedEvent.ActivityId).FirstOrDefault().Name;
                    discordOpen.EmitOpenGroup(SelectedEvent.CreatedBy.DiscordName, MemberTwo, EventName);
                }
                await _context.SaveChangesAsync();
            }
            
            if (option == "Leave")
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

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
using Microsoft.AspNetCore.Authorization;
using LAE.Models;

namespace LAE.Pages.Events.Events
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DateTime CurrentTime;
        public DateTime ServerTime;

        private PartyInfo SelectedEvent { get; set; }

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
        public IList<PartyInfo> PartyInfo { get; set; }

        public async Task<int> GetPartyCount(int partyId)
        {
            int count = await _context.Party.AsQueryable().CountAsync(w => w.PartyId == partyId) + 1;
            if (count == 0)
            {
                return 1;
            }
            else
            {
                return count;
            }
        }

        public DateTime ConvertTime(DateTime dbTime)
        {
            DateTime local = DateTime.UtcNow;
            DateTime server = DateTime.UtcNow.AddHours(3);
            decimal diff = (int)(server.Subtract(local).TotalMinutes);
            dbTime = dbTime.AddMinutes(-(double)diff);
            return new DateTime(dbTime.Year, dbTime.Month, dbTime.Day, dbTime.Hour, dbTime.Minute, dbTime.Second);
        }

        public async Task<bool> GroupCheck(int partyId)
        {
            List<Party> party = await _context.Party.AsQueryable().Where(w => w.PartyId == partyId).ToListAsync();
            return (party == null) ? false : true;
        }

        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
            CurrentTime = DateTime.Now;
            ServerTime = DateTime.UtcNow.AddHours(3);

            if (LoggedInUser != null)
            {
                PartyInfo = await _context.PartyInfo.Include(x => x.CreatedBy)
                                                .Include(x => x.Activity)
                                                .Include(x => x.Members)
                                                .Where(w => w.isActive && w.Activity.MinGearScore <= LoggedInUser.ItemLevel)
                                                .OrderBy(w => w.StartingTime)
                                                .ToListAsync();
            }


            var result = _context.Database.ExecuteSqlRawAsync("EXECUTE dbo.updateEvents @timeNow={0}", ServerTime);
        }

        public async Task<IActionResult> OnPostJoinTeam(int id, int? remaining, string? option)
        {
            ApplicationUser user = await GetCurrentUser();

            SelectedEvent = await _context.PartyInfo.Include(x => x.CreatedBy)
                                                    .Include(x => x.Activity)
                                                    .Include(x => x.Members)
                                                    .AsQueryable().FirstOrDefaultAsync(m => m.Id == id);
            string EventName = _context.Actvity.AsQueryable().Where(m => m.Id == SelectedEvent.ActivityId).FirstOrDefault().Name;
            if (option == "Join")
            {
                Party party = new Party();
                party.PartyId = id;
                party.PartyName = user;
                SelectedEvent.PartyMembers++;

                _context.Party.Add(party);
                _context.Attach(SelectedEvent).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                if (remaining == 1)
                {
                    DiscordSender discord = new DiscordSender(717523711694995487, "jtf8VSXM6ht8H9JlRG9gkBWcgHijFRe9TXISgOU0bO4qtQr8oaVYaJaRmPiNweMFehS0", CultureInfo.CurrentCulture);
                    List<string> MemberList = SelectedEvent.Members.Select(w => w.PartyName.DiscordName).ToList();
                    string members = "";

                    foreach (var member in MemberList)
                    {
                        members += "@" + member + ", ";
                    }

                    EventName = _context.Actvity.AsQueryable().Where(m => m.Id == SelectedEvent.ActivityId).FirstOrDefault().Name;

                    SelectedEvent.isActive = false;
                    discord.Emit(SelectedEvent.CreatedBy.DiscordName, members, EventName);
                }
                else
                {
                    DiscordSender discordOpen = new DiscordSender(722102560722386975, "1Ty2jGrVK46OuSlFwOrp82tCjFIO8ngAhG6TiDQHOw63s7innCp8K654KQIKQLN77fBO", CultureInfo.CurrentCulture);
                    discordOpen.EmitOpenGroup(SelectedEvent.CreatedBy.DiscordName, user.DiscordName, EventName);
                }
            }

            if (option == "Leave")
            {
                Party party = await _context.Party.AsQueryable().FirstOrDefaultAsync(m => m.PartyId == id && m.PartyName == user);
                SelectedEvent.PartyMembers--;
                _context.Party.Remove(party);
                _context.Attach(SelectedEvent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            

            return RedirectToPage("./Index");
        }

    }
}

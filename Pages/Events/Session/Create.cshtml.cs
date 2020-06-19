using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;
using LAE.Services;
using System;
using System.Globalization;
using System.Linq;
using LAE.Models;

namespace LAE.Pages.Events.Events
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventService _eventService;

        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }
        [BindProperty]
        public int GearScore { get; set; }

        //[BindProperty]
        //public EventInfo EventInfo { get; set; }

        [BindProperty]
        public PartyInfo PartyInfo { get; set; }

        [BindProperty(SupportsGet = true)]
        public int EventTypeId { get; set; }
        public int ActivityId { get; set; }

        public int PartySize { get; set; }
        public SelectList ActivityTypes { get; set; }

        public DateTime CurrentTime;
        public DateTime ServerTime;

        [BindProperty]
        public string ErrorMessage { get; set; }

        public CreateModel(Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEventService eventService)
        {
            _context = context;
            _userManager = userManager;
            _eventService = eventService;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        public JsonResult OnGetSubEvents()
        {
            return new JsonResult(_eventService.GetSubEvents(EventTypeId));
        }

        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
            CurrentTime = DateTime.Now;
            ServerTime = DateTime.UtcNow.AddHours(3);
            ActivityTypes = new SelectList(_eventService.GetEvents(),
                nameof(EventType.TypeId), nameof(EventType.Type));
        }


        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await GetCurrentUser();

            decimal diff = (int)DateTime.UtcNow.AddHours(3).Subtract(DateTime.Now).TotalMinutes;
            DateTime newTime = PartyInfo.StartingTime.AddMinutes((double)diff);

            PartyInfo newParty = new PartyInfo();
            newParty.ActivityId = PartyInfo.ActivityId;
            newParty.CreatedBy = user;
            newParty.isActive = true;
            newParty.StartingTime = newTime;
            newParty.PartyMembers = 1;
            newParty.PartySize = PartyInfo.PartySize;
            newParty.Server = user.Server;

            int activeCount = _context.PartyInfo.AsQueryable().Where(w => w.CreatedBy == user && w.isActive == true).ToList().Count;

            if (activeCount == 0)
            {
                _context.PartyInfo.Add(newParty);

                DiscordSender discordOpen = new DiscordSender(722102560722386975, "1Ty2jGrVK46OuSlFwOrp82tCjFIO8ngAhG6TiDQHOw63s7innCp8K654KQIKQLN77fBO", CultureInfo.CurrentCulture);
                string EventName = _context.Actvity.AsQueryable().Where(m => m.Id == newParty.ActivityId).FirstOrDefault().Name;
                discordOpen.EmitOpenGroup(newParty.CreatedBy.DiscordName, EventName);

                await _context.SaveChangesAsync();
                ErrorMessage = "";
                return RedirectToPage("./Index");
            }
            else
            {
                ErrorMessage = "You can't create more than one event at a time!";
                return Page();
            }



        }
    }
}

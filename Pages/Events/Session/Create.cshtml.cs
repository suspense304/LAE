using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;
using LAE.Services;

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

        [BindProperty]
        public EventInfo EventInfo { get; set; }

        [BindProperty(SupportsGet = true)]
        public int EventTypeId { get; set; }
        public int ActivityId { get; set; }
        public SelectList ActivityTypes { get; set; }

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
            ActivityTypes =  new SelectList(_eventService.GetEvents(),
                nameof(EventType.TypeId), nameof(EventType.Type));
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await GetCurrentUser();

            EventInfo newEvent = new EventInfo();
            newEvent.ActivityId = EventInfo.ActivityId;
            newEvent.CreatedBy = user;
            newEvent.isActive = true;
            newEvent.StartingTime = EventInfo.StartingTime;
            newEvent.ServerName = user.Server;

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.EventInfo.Add(newEvent);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

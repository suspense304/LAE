using System.Threading.Tasks;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static LostArkEng.Models.ApplicationUser;

namespace LAE.Pages.Account.Profile
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }

        [BindProperty]
        public string CharacterName { get; set; }

        [BindProperty]
        public string DiscordName { get; set; }

        [BindProperty]
        public int CharClass { get; set; }

        [BindProperty]
        public double GearScore { get; set; }

        public string ReturnUrl { get; set; }

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
            CharacterName = LoggedInUser.CharacterName;
            DiscordName = LoggedInUser.DiscordName;
            CharClass = (int)LoggedInUser.CharClass;
            GearScore = LoggedInUser.ItemLevel;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await GetCurrentUser();
            user.CharacterName = CharacterName;
            user.DiscordName = DiscordName;
            user.CharClass = (ApplicationUser.CharacterClass)CharClass;
            user.ItemLevel = GearScore;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return Redirect(Url.Content("~/"));
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
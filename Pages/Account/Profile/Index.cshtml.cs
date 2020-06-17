using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LAE.Data;
using LAE.Models;
using LostArkEng.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static LostArkEng.Models.ApplicationUser;

namespace LAE.Pages.Account.Profile
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IList<EventInfo> EventInfo { get; set; }

        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }

        [BindProperty]
        public UserInfoModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();

            if (LoggedInUser != null)
            {
                EventInfo = await _context.EventInfo.Include(x => x.MemberTwo)
                                                .Include(x => x.MemberThree)
                                                .Include(x => x.MemberFour)
                                                .Include(x => x.CreatedBy)
                                                .Include(x => x.Activity)
                                                .Where(w => w.CreatedBy == LoggedInUser ||
                                                            w.MemberTwo == LoggedInUser ||
                                                            w.MemberThree == LoggedInUser ||
                                                            w.MemberFour == LoggedInUser
                                                ).OrderBy(w => w.StartingTime)
                                                .ToListAsync();
            }

            Input = new UserInfoModel();
            Input.CharacterName = LoggedInUser.CharacterName;
            Input.DiscordName = LoggedInUser.DiscordName;
            Input.CharClass = (int)LoggedInUser.CharClass;
            Input.GearScore = LoggedInUser.ItemLevel;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await GetCurrentUser();
            user.CharacterName = Input.CharacterName;
            user.DiscordName = Input.DiscordName;
            user.CharClass = (ApplicationUser.CharacterClass)Input.CharClass;
            user.ItemLevel = Input.GearScore;

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
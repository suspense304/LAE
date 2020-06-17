using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LAE.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public ApplicationUser LoggedInUser { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    await _emailSender.SendEmailAsync("doughantke@gmail.com", "Confirm your email",
        //        $"Test email. Seeing if this works.");

        //    return RedirectToPage("./Index");
        //}
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
        }

        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LAE.Data;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity;

namespace LAE.Pages.Events.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

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

        public IList<Activity> Activity { get;set; }

        public async Task OnGetAsync()
        {
            LoggedInUser = await GetCurrentUser();
            Activity = await _context.Actvity.ToListAsync();
        }
    }
}

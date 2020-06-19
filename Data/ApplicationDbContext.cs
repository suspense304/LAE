using System;
using System.Collections.Generic;
using System.Text;
using LAE.Models;
using LostArkEng.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LAE.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Activity> Actvity { get; set; }
        //public virtual DbSet<EventInfo> EventInfo { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }

        public virtual DbSet<PartyInfo> PartyInfo { get; set; }

        public virtual DbSet<Party> Party { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}

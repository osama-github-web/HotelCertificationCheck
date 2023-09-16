using HotelCertificationCheck.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HotelCertificationCheck.Context
{
    public class HotelContext : IdentityDbContext<HotelUser>
    {
        public HotelContext(DbContextOptions<HotelContext> dbContextOptions)
            :base(dbContextOptions) { }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

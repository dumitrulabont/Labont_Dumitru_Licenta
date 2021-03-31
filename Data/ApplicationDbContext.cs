using Labont_Dumitru_Licenta.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Labont_Dumitru_Licenta.Models;

namespace Labont_Dumitru_Licenta.Data
{
    public class ApplicationDbContext : IdentityDbContext<Labont_Dumitru_LicentaUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           
        }
        public DbSet<Labont_Dumitru_Licenta.Models.Car> Car { get; set; }
        public DbSet<Labont_Dumitru_Licenta.Models.UserLocation> UserLocations { get; set; }
        public DbSet<Labont_Dumitru_Licenta.Models.CarDetail> CarDetails { get; set; }
        public DbSet<Labont_Dumitru_Licenta.Models.Request> Requests { get; set; }
        public DbSet<Labont_Dumitru_Licenta.Models.Contract> Contracts{ get; set; }
        public DbSet<Labont_Dumitru_LicentaUser> Labont_Dumitru_LicentaUsers { get; set; }
    }
}

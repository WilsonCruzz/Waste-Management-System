using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using waste_management_system.Models;

namespace waste_management_system.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<TypeOfWaste> TypeOfWastes { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<PickUpRequest> PickUpRequests { get; set; }
        public DbSet<Observation> Observations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Capacity> Capacities { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TypeOfWaste>()
        //        .HasOne(t => t.Vehicles)
        //        .WithMany(v => v.TypeOfWastes)
        //        .HasForeignKey(t => t.VehicleId);


        //    base.OnModelCreating(modelBuilder);
        //}
    }
}

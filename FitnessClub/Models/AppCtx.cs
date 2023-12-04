using FitnessClub.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Models
{
    public class AppCtx : IdentityDbContext<User>
    {
        public AppCtx(DbContextOptions<AppCtx> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Service> Services { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
    
    
}

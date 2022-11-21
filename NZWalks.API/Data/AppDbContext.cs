using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>().
                HasOne(x => x.Role).WithMany(y=> y.User_Roles).HasForeignKey(x=>x.RoleId);

            modelBuilder.Entity<User_Role>().
                HasOne(x => x.User).WithMany(y => y.User_Roles).HasForeignKey(x => x.UserId);
        }

        public DbSet<Region> Regions { get; set; } = default!;
        public DbSet<Walk> Walks { get; set; } = default!;
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<User_Role> User_Roles { get; set; } = default!;
    }
}

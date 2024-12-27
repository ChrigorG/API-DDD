using Entities.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Settings
{
    public class AppDbContext : IdentityDbContext<UserEntity>
    {
        private readonly DbContextOptions<AppDbContext> _db;

        public AppDbContext(DbContextOptions<AppDbContext> db) : base(db)
        {
            _db = db;
        }

        public DbSet<NewsEntity> News { get; set; }
        public DbSet<UserEntity> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConnection());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        public string GetStringConnection()
        {
            return "Server=(localdb)\\mssqllocaldb;Database=API_DDD;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
    }
}

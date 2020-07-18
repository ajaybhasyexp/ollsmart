using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace OllsMart
{
    public class OllsMartContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private IConfiguration _config;
        public OllsMartContext(DbContextOptions<OllsMartContext> options,
        IConfiguration configuration) : base(options)
        {
            _config = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var user = modelBuilder.Entity<User>();
            user.HasKey(p => p.UserId);
        }

    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace OllsMart
{
    public class OllsMartContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

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

            var product = modelBuilder.Entity<Product>();
            product.HasKey(p => p.ProductId);

            var category = modelBuilder.Entity<Category>();
            product.HasKey(p => p.CategoryId);
        }

    }
}
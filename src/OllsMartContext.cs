using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace OllsMart
{
    public class OllsMartContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }   
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ExpenseHead> ExpenseHeads {get;set;}
        public DbSet<Expense> Expenses {get;set;}
        public DbSet<OrderHeader> OrderHeaders {get;set;}
        public DbSet<OrderDetail> OrderDetails {get;set;}
        public DbSet<InvoiceHeader> InvoiceHeaders {get;set;}
        public DbSet<InvoiceDetail> InvoiceDetails {get;set;}

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
            category.HasKey(p => p.CategoryId);
            
            var brand = modelBuilder.Entity<Brand>();
            brand.HasKey(p => p.BrandId);
            
            var unit = modelBuilder.Entity<Unit>();
            unit.HasKey(p => p.UnitId);
           
            var productAttribute = modelBuilder.Entity<ProductAttribute>();
            productAttribute.HasKey(p => p.ProductAttributeId);
            
            var productProperty = modelBuilder.Entity<ProductProperty>();
            productProperty.HasKey(p => p.ProductPropertyId);
            
            var userRole = modelBuilder.Entity<UserRole>();
            userRole.HasKey(p => p.UserRoleId);

            var expenseHead = modelBuilder.Entity<ExpenseHead>();
            expenseHead.HasKey(p => p.ExpenseHeadId);
            
            var expense = modelBuilder.Entity<Expense>();
            expense.HasKey(p => p.ExpenseId);

            var orderHeader = modelBuilder.Entity<OrderHeader>();
            orderHeader.HasKey(p => p.OrderHeaderId);
            // orderHeader.Property(b => b.OrderNo).HasComputedColumnSql("'SO'+CONVERT([nvarchar](50),[OrderHeaderId])");

            var orderDetail = modelBuilder.Entity<OrderDetail>();
            orderDetail.HasKey(p => p.OrderDetailId);
            
            // orderDetail.HasOne(p=>p.OrderHeader).WithMany(p => p.OrderDetail).HasForeignKey(p => p.OrderId);
           
            var invoiceHeader = modelBuilder.Entity<InvoiceHeader>();
            invoiceHeader.HasKey(p => p.InvoiceHeaderId);

            var invoiceDetail = modelBuilder.Entity<InvoiceDetail>();
            invoiceDetail.HasKey(p => p.InvoiceDetailId);
            

        }

    }
}
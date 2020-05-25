using System.Data.Entity;
using Portfolio.Entities.Models;

namespace Portfolio.Entities
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext() : base("DefaultConnection")
        {

        }

        public PortfolioContext(System.Data.Common.DbConnection connection) : base(connection, true)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ImageUseType> ImageUseTypes { get; set; }
        public virtual DbSet<Checkout> Checkouts { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<UploadImage> UploadImages { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

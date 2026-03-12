using AnnapurnaEnterprises.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnapurnaEnterprises.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<PreviousWork> PreviousWorks => Set<PreviousWork>();
        public DbSet<PreviousWorkImage> PreviousWorkImages => Set<PreviousWorkImage>();
        public DbSet<Inquiry> Inquiries => Set<Inquiry>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdminUser>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PreviousWork>()
                .HasMany(w => w.Images)
                .WithOne(i => i.PreviousWork)
                .HasForeignKey(i => i.PreviousWorkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
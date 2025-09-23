using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Models;

namespace MiniShop;
public class ApplicationContext : DbContext
{

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ImageConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.AuthorCreated)
            .WithMany()
            .HasForeignKey(p => p.AuthorCreatedId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(p => p.AuthorUpdated)
            .WithMany()
            .HasForeignKey(p => p.AuthorUpdatedId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(p => p.Image)
            .WithMany()
            .HasForeignKey(p => p.ImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasOne(c => c.AuthorCreated)
            .WithMany()
            .HasForeignKey(c => c.AuthorCreatedId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(c => c.AuthorUpdated)
            .WithMany()
            .HasForeignKey(c => c.AuthorUpdatedId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasOne(i => i.AuthorCreated)
            .WithMany()
            .HasForeignKey(i => i.AuthorCreatedId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(i => i.AuthorUpdated)
            .WithMany()
            .HasForeignKey(i => i.AuthorUpdatedId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.AuthorCreated)
            .WithMany()
            .HasForeignKey(o => o.AuthorCreatedId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(o => o.AuthorUpdated)
            .WithMany()
            .HasForeignKey(o => o.AuthorUpdatedId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasOne(o => o.AuthorCreated)
            .WithMany()
            .HasForeignKey(o => o.AuthorCreatedId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(o => o.AuthorUpdated)
            .WithMany()
            .HasForeignKey(o => o.AuthorUpdatedId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
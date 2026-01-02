using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public partial class EBayDbContext : DbContext
{
    public EBayDbContext()
    {
    }

    public EBayDbContext(DbContextOptions<EBayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ConnectionCountLog> ConnectionCountLogs { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Listing> Listings { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleGroup> RoleGroups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // This will be overridden by dependency injection in production
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bids__3214EC07EB808BDF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BidAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Bidder).WithMany(p => p.Bids)
                .HasForeignKey(d => d.BidderId)
                .HasConstraintName("FK_Bids_Bidders");

            entity.HasOne(d => d.Listing).WithMany(p => p.Bids)
                .HasForeignKey(d => d.ListingId)
                .HasConstraintName("FK_Bids_Listings");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC075BB0B4C7");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<ConnectionCountLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Connecti__3214EC07E55B0495");

            entity.ToTable("ConnectionCountLog");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IpAddress).HasMaxLength(100);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Groups__3214EC0788F29F03");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.GroupName).HasMaxLength(200);
        });

        modelBuilder.Entity<Listing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Listings__3214EC071456452C");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(500);

            entity.HasOne(d => d.Category).WithMany(p => p.Listings)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Listings_Categories");

            entity.HasOne(d => d.Product).WithMany(p => p.Listings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Listings_Products");

            entity.HasOne(d => d.Seller).WithMany(p => p.Listings)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK_Listings_Sellers");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07252D918B");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK_Orders_Buyers");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC0721A91A40");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC070110B395");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC07FDF9BEED");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl).HasMaxLength(1000);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductImages_Products");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ratings__3214EC07BEC84CED");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Ratings_Products");

            entity.HasOne(d => d.RatedUser).WithMany(p => p.RatingRatedUsers)
                .HasForeignKey(d => d.RatedUserId)
                .HasConstraintName("FK_Ratings_RatedUser");

            entity.HasOne(d => d.Rater).WithMany(p => p.RatingRaters)
                .HasForeignKey(d => d.RaterId)
                .HasConstraintName("FK_Ratings_Rater");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Refresh___3213E83F95D8C999");

            entity.ToTable("Refresh_tokens");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientId)
                .HasMaxLength(500)
                .HasColumnName("client_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(500)
                .HasColumnName("device_id");
            entity.Property(e => e.DeviceName)
                .HasMaxLength(500)
                .HasColumnName("device_name");
            entity.Property(e => e.DeviceOs)
                .HasMaxLength(200)
                .HasColumnName("device_os");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(200)
                .HasColumnName("device_type");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .HasColumnName("ip_address");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefreshTokens_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07F9620129");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(200);
        });

        modelBuilder.Entity<RoleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleGrou__3214EC079F80166B");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Group).WithMany(p => p.RoleGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_RoleGroups_Groups");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleGroups)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleGroups_Roles");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D56B28EA");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(300);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(200);
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserGrou__3214EC07794F801D");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Group).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_UserGroups_Groups");

            entity.HasOne(d => d.User).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserGroups_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany()
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRole_Roles");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRole_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

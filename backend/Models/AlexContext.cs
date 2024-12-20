using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class AlexContext : DbContext
{
    public AlexContext()
    {
    }

    public AlexContext(DbContextOptions<AlexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<OrderAdress> OrderAdresses { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderPayment> OrderPayments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAdress> UserAdresses { get; set; }

    public virtual DbSet<UserPayment> UserPayments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("cart_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_cart_item_product");

            entity.HasOne(d => d.User).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_cart_item_user");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("discount");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DiscountPercent).HasColumnName("discount_percent");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("image");

            entity.Property(e => e.Id).HasColumnName("id ");
            entity.Property(e => e.ImageName)
                .IsUnicode(false)
                .HasColumnName("image_name");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_image_product");
        });

        modelBuilder.Entity<OrderAdress>(entity =>
        {
            entity.ToTable("order_adress");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdressLine)
                .IsUnicode(false)
                .HasColumnName("adress_line");
            entity.Property(e => e.City)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.PostalCode)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.Telephone)
                .IsUnicode(false)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("order_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdressId).HasColumnName("adress_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Adress).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.AdressId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_order_details_order_adress");

            entity.HasOne(d => d.Payment).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_order_details_order_payment");

            entity.HasOne(d => d.User).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_details_user");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("order_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_items_order_details");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_items_product");
        });

        modelBuilder.Entity<OrderPayment>(entity =>
        {
            entity.ToTable("order_payment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account_no");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_type");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_product_product_category");

            entity.HasOne(d => d.Discount).WithMany(p => p.Products)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_product_discount");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("product_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descriprion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descriprion");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.PasswordResetToken)
                .IsUnicode(false)
                .HasColumnName("password_reset_token");
            entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
            entity.Property(e => e.ResetTokenExpires)
                .HasColumnType("datetime")
                .HasColumnName("reset_token_expires");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('empty')")
                .HasColumnName("username");
            entity.Property(e => e.VerificationToken)
                .IsUnicode(false)
                .HasColumnName("verification_token");
            entity.Property(e => e.VerifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("verified_at");
        });

        modelBuilder.Entity<UserAdress>(entity =>
        {
            entity.ToTable("user_adress");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdressLine)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("adress_line");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserAdresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_adress_user");
        });

        modelBuilder.Entity<UserPayment>(entity =>
        {
            entity.ToTable("user_payment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account_no");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPayments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_user_payment_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

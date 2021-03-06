﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StoreApplication.DataAccess.Entities
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookEntity> Books { get; set; }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<InventoryEntity> Inventories { get; set; }
        public virtual DbSet<LocationEntity> Locations { get; set; }
        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<OrderlineEntity> Orderlines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>(entity =>
            {
                entity.HasKey(e => e.Isbn)
                    .HasName("PK__book__99F9D0A56A3DF283");

                entity.ToTable("book");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(15)
                    .HasColumnName("isbn");

                entity.Property(e => e.AuthorFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("author_first_name");

                entity.Property(e => e.AuthorLastName)
                    .HasMaxLength(50)
                    .HasColumnName("author_last_name");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__customer__locati__61F08603");
            });

            modelBuilder.Entity<InventoryEntity>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.BookIsbn })
                    .HasName("PK__inventor__939A7875C75E8FB4");

                entity.ToTable("inventory");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.BookIsbn)
                    .HasMaxLength(15)
                    .HasColumnName("book_isbn");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.BookIsbnNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.BookIsbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__inventory__book___689D8392");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__inventory__locat__67A95F59");
            });

            modelBuilder.Entity<LocationEntity>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__customer__6B79F03D");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__location__6C6E1476");
            });

            modelBuilder.Entity<OrderlineEntity>(entity =>
            {
                entity.ToTable("orderline");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookIsbn)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("book_isbn");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.BookIsbnNavigation)
                    .WithMany(p => p.Orderlines)
                    .HasForeignKey(d => d.BookIsbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orderline__book___7132C993");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderlines)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orderline__order__703EA55A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

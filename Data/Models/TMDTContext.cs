using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class TMDTContext : DbContext
    {
        public TMDTContext()
        {
        }

        public TMDTContext(DbContextOptions<TMDTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Bakery> Bakery { get; set; }
        public virtual DbSet<BakeryType> BakeryType { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        
        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ERP-HAIDT\\SQLEXPRESS;Database=TMDT;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idcustomer).HasColumnName("IDCustomer");

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.Username).HasMaxLength(150);

                entity.HasOne(d => d.IdcustomerNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.Idcustomer)
                    .HasConstraintName("FK__Account__IDCusto__398D8EEE");
            });

            modelBuilder.Entity<Bakery>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idtype).HasColumnName("IDType");

                entity.HasOne(d => d.IdtypeNavigation)
                    .WithMany(p => p.Bakery)
                    .HasForeignKey(d => d.Idtype)
                    .HasConstraintName("FK__Bakery__IDType__3E52440B");
            });

            modelBuilder.Entity<BakeryType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(11);
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdShoppingCart).HasColumnName("IDShoppingCart");

                entity.Property(e => e.Idbakery).HasColumnName("IDBakery");

                entity.HasOne(d => d.IdbakeryNavigation)
                    .WithMany(p => p.ShoppingCartItem)
                    .HasForeignKey(d => d.Idbakery)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShoppingCartItem__IDBak");

                entity.HasOne(d => d.IdShoppingCartNavigation)
                    .WithMany(p => p.ShoppingCartItem)
                    .HasForeignKey(d => d.IdShoppingCart)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShoppingCartItem__IDShoppingCart");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.Idorder, e.Idbakery })
                    .HasName("orderdetail_pk");

                entity.Property(e => e.Idorder).HasColumnName("IDOrder");

                entity.Property(e => e.Idbakery).HasColumnName("IDBakery");

                entity.HasOne(d => d.IdbakeryNavigation)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.Idbakery)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__IDBak__44FF419A");

                entity.HasOne(d => d.IdorderNavigation)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.Idorder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__IDOrd__440B1D61");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Idcustomer).HasColumnName("IDCustomer");

                entity.HasOne(d => d.IdcustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Idcustomer)
                    .HasConstraintName("FK__Orders__IDCustom__412EB0B6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Models
{
    public partial class ElectricityBillsContext : DbContext
    {
        public ElectricityBillsContext()
        {
        }

        public ElectricityBillsContext(DbContextOptions<ElectricityBillsContext> options)
            : base(options)
        {
        }

        public  DbSet<CounterReads> CounterReads { get; set; }
        public  DbSet<Customer> Customer { get; set; }
        public  DbSet<CustomerBills> CustomerBills { get; set; }
        public  DbSet<Line> Line { get; set; }
        public  DbSet<Payment> Payment { get; set; }
        public  DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder
                    .UseSqlServer("Server=.;Database=ElectricityBills;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CounterReads>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);

                entity.Property(e => e.DateOfRead).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CounterReads)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CounterReads_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.LineId);

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.LastBalance).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.LineId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Customer_Line");
            });

            modelBuilder.Entity<CustomerBills>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.LineId);

                entity.Property(e => e.BillAmount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.DateOfLastRead).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.LastBalance).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.MaintenanceFees).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PeriodOfBill).HasMaxLength(50);

                entity.Property(e => e.ServicesFees).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerBills)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CustomerBills_Customer");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.CustomerBills)
                    .HasForeignKey(d => d.LineId)
                    .HasConstraintName("FK_CustomerBills_Line");
            });

            modelBuilder.Entity<Line>(entity =>
            {
                entity.Property(e => e.LineName).HasMaxLength(50);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.DateOfPay).HasColumnType("date");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnName("Payment")
                    .HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Payment_Customer");
            });
        }
    }
}

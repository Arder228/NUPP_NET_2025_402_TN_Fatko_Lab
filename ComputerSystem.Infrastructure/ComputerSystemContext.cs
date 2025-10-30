using Microsoft.EntityFrameworkCore;
using ComputerSystem.Infrastructure.Models;

namespace ComputerSystem.Infrastructure
{
    public class ComputerSystemContext : DbContext
    {
        public ComputerSystemContext(DbContextOptions<ComputerSystemContext> opts) : base(opts) { }

        public DbSet<ComputerModel> Computers => Set<ComputerModel>();
        public DbSet<ComputerDetailModel> ComputerDetails => Set<ComputerDetailModel>();
        public DbSet<VendorModel> Vendors => Set<VendorModel>();
        public DbSet<ComponentModel> Components => Set<ComponentModel>();
        public DbSet<ProcessorModel> Processors => Set<ProcessorModel>();
        public DbSet<GraphicsCardModel> GraphicsCards => Set<GraphicsCardModel>();
        public DbSet<MemoryModel> Memories => Set<MemoryModel>();
        public DbSet<CoolingModel> Coolings => Set<CoolingModel>();
        public DbSet<MotherboardModel> Motherboards => Set<MotherboardModel>();
        public DbSet<PowerSupplyModel> PowerSupplies => Set<PowerSupplyModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComponentModel>().ToTable("Components");
            modelBuilder.Entity<ProcessorModel>().ToTable("Processors");
            modelBuilder.Entity<GraphicsCardModel>().ToTable("GraphicsCards");
            modelBuilder.Entity<MemoryModel>().ToTable("Memories");
            modelBuilder.Entity<CoolingModel>().ToTable("Coolings");
            modelBuilder.Entity<MotherboardModel>().ToTable("Motherboards");
            modelBuilder.Entity<PowerSupplyModel>().ToTable("PowerSupplies");

            modelBuilder.Entity<ComputerModel>(b =>
            {
                b.ToTable("Computers");
                b.HasKey(c => c.Id);
                b.Property(c => c.Name).IsRequired().HasMaxLength(200);
                b.HasMany(c => c.Components)
                 .WithOne(cmp => cmp.Computer)
                 .HasForeignKey(cmp => cmp.ComputerId)
                 .OnDelete(DeleteBehavior.SetNull);
                b.HasOne(c => c.Detail)
                 .WithOne(d => d.Computer)
                 .HasForeignKey<ComputerDetailModel>(d => d.ComputerId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ComputerDetailModel>(b =>
            {
                b.ToTable("ComputerDetails");
                b.HasKey(d => d.Id);
                b.Property(d => d.SerialNumber).HasMaxLength(200);
            });

            modelBuilder.Entity<VendorModel>(b =>
            {
                b.ToTable("Vendors");
                b.HasKey(v => v.Id);
                b.Property(v => v.Name).IsRequired().HasMaxLength(200);
                b.HasMany(v => v.Components)
                 .WithOne(c => c.Vendor)
                 .HasForeignKey(c => c.VendorId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ComponentModel>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Model).IsRequired().HasMaxLength(200);
                b.Property(c => c.Manufacturer).HasMaxLength(200);
            });

            modelBuilder.Entity<ProcessorModel>().HasBaseType<ComponentModel>();
            modelBuilder.Entity<GraphicsCardModel>().HasBaseType<ComponentModel>();
            modelBuilder.Entity<MemoryModel>().HasBaseType<ComponentModel>();
            modelBuilder.Entity<CoolingModel>().HasBaseType<ComponentModel>();
            modelBuilder.Entity<MotherboardModel>().HasBaseType<ComponentModel>();
            modelBuilder.Entity<PowerSupplyModel>().HasBaseType<ComponentModel>();
        }
    }
}

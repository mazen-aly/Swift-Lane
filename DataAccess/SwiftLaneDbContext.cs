using DataAccess.Enums;
using DataAccess.SecurityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess
{
    public class SwiftLaneDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public SwiftLaneDbContext(DbContextOptions<SwiftLaneDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<SystemSettings> SystemSettings { get; set; }

        public DbSet<ShippingType> ShippingTypes { get; set; }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<ShipmentCarrier> ShipmentCarriers { get; set; }

        public DbSet<ShipmentStatus> ShipmentStatuses { get; set; }

        public DbSet<SubscriptionPackage> SubscriptionPackages { get; set; }

        public DbSet<UserReceiver> UserReceivers { get; set; }

        public DbSet<UserSender> UserSenders { get; set; }

        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId);

                entity.Property(e => e.CurrentState)
                    .HasDefaultValue(DatabaseRecordState.Existant);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(e => e.CurrentState)
                    .HasDefaultValue(DatabaseRecordState.Existant);

            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(e => e.CurrentState)
                    .HasDefaultValue(DatabaseRecordState.Existant);

            });

            modelBuilder.Entity<SystemSettings>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<ShippingType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(e => e.CurrentState)
                    .HasDefaultValue(DatabaseRecordState.Existant);

            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.UserReceiver)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.UserReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.UserSender)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.UserSenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ShippingType)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.ShippingTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.CurrentState)
                    .HasDefaultValue(DatabaseRecordState.Existant);
            });

            modelBuilder.Entity<ShipmentCarrier>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(e => e.CurrentState)
                    .HasDefaultValue(DatabaseRecordState.Existant);

            });

            modelBuilder.Entity<ShipmentStatus>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.ShipmentStatuses)
                    .HasForeignKey(d => d.CarrierId);

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.ShipmentStatuses)
                    .HasForeignKey(d => d.ShipmentId);

                entity.Property(e => e.CurrentState)
                    .HasDefaultValue(DatabaseRecordState.Existant);
            });

            modelBuilder.Entity<SubscriptionPackage>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(e => e.CurrentState).HasDefaultValue(DatabaseRecordState.Existant);

            });

            modelBuilder.Entity<UserReceiver>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(e => e.Address)
                    .HasMaxLength(200);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.UserReceivers)
                    .HasForeignKey(d => d.CityId);

                entity.Property(e => e.CurrentState).HasDefaultValue(DatabaseRecordState.Existant);
            });

            modelBuilder.Entity<UserSender>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(e => e.Address)
                    .HasMaxLength(200);

                entity.Property(e => e.CurrentState)
                    .HasDefaultValue(DatabaseRecordState.Existant);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.UserSenders)
                    .HasForeignKey(d => d.CityId);
            });

            modelBuilder.Entity<UserSubscription>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.UserSubscriptions)
                    .HasForeignKey(d => d.PackageId);
            });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<string>().HaveMaxLength(100);
            configurationBuilder.Properties<string?>().HaveMaxLength(100);
            configurationBuilder.Properties<decimal>().HaveColumnType("decimal(8, 4)");
            configurationBuilder.Properties<decimal?>().HaveColumnType("decimal(8, 4)");
        }

    }
}
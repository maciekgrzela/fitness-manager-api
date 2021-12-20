using System;
using System.Threading;
using System.Threading.Tasks;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Entities.EntitiesConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.DataAccess.Context
{
    public class DataContext: IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<CustomerFitnessClassEnrolmentEntity> CustomerFitnessClassEnrolments { get; set; }
        public DbSet<CustomerSubscriptionsEntity> CustomerSubscriptions { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<EquipmentReservationEntity> EquipmentReservations { get; set; }
        public DbSet<FitnessClassEnrolmentsEntity> FitnessClassEnrolments { get; set; }
        public DbSet<FitnessClassEntity> FitnessClasses { get; set; }
        public DbSet<FitnessClubEntity> FitnessClubs { get; set; }
        public DbSet<FitnessClubNetworkEntity> FitnessClubNetworks { get; set; }
        public DbSet<HallEntity> Halls { get; set; }
        public DbSet<InstructorEntity> Instructors { get; set; }
        public DbSet<SportsEquipmentEntity> SportsEquipments { get; set; }
        public DbSet<SubscriptionEntity> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressEntityConfiguration());
            builder.ApplyConfiguration(new ContactEntityConfiguration());
            builder.ApplyConfiguration(new CustomerEntityConfiguration());
            builder.ApplyConfiguration(new CustomerFitnessClassEnrolmentEntityConfiguration());
            builder.ApplyConfiguration(new CustomerSubscriptionsEntityConfiguration());
            builder.ApplyConfiguration(new DepartmentEntityConfiguration());
            builder.ApplyConfiguration(new EquipmentReservationEntityConfiguration());
            builder.ApplyConfiguration(new FitnessClassEnrolmentsEntityConfiguration());
            builder.ApplyConfiguration(new FitnessClassEntityConfiguration());
            builder.ApplyConfiguration(new FitnessClubEntityConfiguration());
            builder.ApplyConfiguration(new FitnessClubNetworkEntityConfiguration());
            builder.ApplyConfiguration(new HallEntityConfiguration());
            builder.ApplyConfiguration(new InstructorEntityConfiguration());
            builder.ApplyConfiguration(new SportsEquipmentEntityConfiguration());
            builder.ApplyConfiguration(new SubscriptionEntityConfiguration());
            
            base.OnModelCreating(builder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            var dateTimeNow = DateTime.Now;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = dateTimeNow;
                    entry.Property(nameof(entry.Entity.CreatedAt)).IsModified = false;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = dateTimeNow;
                    entry.Entity.LastModifiedAt = dateTimeNow;
                }
            }
        }
    }
}
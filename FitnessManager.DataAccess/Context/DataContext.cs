using System;
using System.Threading;
using System.Threading.Tasks;
using FitnessManager.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.DataAccess.Context
{
    public class DataContext: IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
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
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;


namespace Domain.EfCore
{
    public abstract class AppDbContextBase : DbContext, IDomainEventContext
    {
        protected AppDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public IEnumerable<DomainEventBase> GetDomainEvents()
        {
            var domainEntities = ChangeTracker
                .Entries<EntityBase>()
                .Where(x =>
                    x.Entity.DomainEvents != null &&
                    x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.DomainEvents.Clear());

            return domainEvents;
        }

        public override int SaveChanges()
        {
            SetModifiedInfo();
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                SetModifiedInfo();
                UpdateSoftDeleteStatuses();
            }
            catch (Exception e)
            {
                Console.WriteLine("SaveChangesAsync error: " + e.ToString());
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity.GetType().GetProperty("IsDeleted") is not null)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["IsDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                            break;
                    }
                }
            }
        }

        private void SetModifiedInfo()
        {
            var currentUser = this.GetService<ICurrentUser>();
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditedEntity && x.State != EntityState.Unchanged);
            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is IAuditedEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreationTime = DateTime.UtcNow;
                        entity.CreatorId = currentUser.Id;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.CreationTime).IsModified = false;
                        Entry(entity).Property(x => x.CreatorId).IsModified = false;
                    }

                    entity.LastModificationTime = DateTime.UtcNow;
                    entity.LastModifierId = currentUser.Id;
                }
            }
        }

    }
}

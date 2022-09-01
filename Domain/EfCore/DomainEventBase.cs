﻿
using MediatR;

namespace Domain.EfCore
{

    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
        IDictionary<string, object> MetaData { get; }
    }

    public interface IDomainEventContext
    {
        IEnumerable<DomainEventBase> GetDomainEvents();
    }

    public abstract class DomainEventBase : IDomainEvent
    {
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public IDictionary<string, object> MetaData { get; set; } = new Dictionary<string, object>();
    }
}

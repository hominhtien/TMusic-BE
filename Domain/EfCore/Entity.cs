
namespace Domain.EfCore
{
    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; protected set; }
    }

    public abstract class Entity : IEntity
    {
    }
}

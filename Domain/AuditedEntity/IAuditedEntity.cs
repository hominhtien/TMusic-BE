
namespace Domain.AuditedEntity
{
    public interface IAuditedEntity
    {
        DateTime CreationTime { get; set; }
        Guid? CreatorId { get; set; }
        DateTime? DeletionTime { get; set; }
        Guid? DeleterId { get; set; }
        Guid? LastModifierId { get; set; }
        DateTime? LastModificationTime { get; set; }
    }
}

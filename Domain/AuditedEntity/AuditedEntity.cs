using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AuditedEntity
{
    [Serializable]
    public abstract class AuditedEntity<T> : IAuditedEntity
    {
        public virtual DateTime CreationTime { get; set; }
        public virtual Guid? CreatorId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual Guid? DeleterId { get; set; }
        public virtual Guid? LastModifierId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual T Id { get; set; }
    }
}

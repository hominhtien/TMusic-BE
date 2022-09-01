using Domain.AuditedEntity;

namespace Domain.Model
{
    public class Topic : AuditedEntity<Guid>
    {
        public int ImageLink { get; set; }
        public string TopicName { get; set; }
    }
}

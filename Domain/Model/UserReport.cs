using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class UserReport : AuditedEntity<Guid>
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ReportUserId { get; set; }
        public virtual User ReportUser { get; set; }
        public string Desciption { get; set; }
        public string Status { get; set; }
        public DateTime ProcessingDate { get; set; }
    }
    public class UserReportEntityConfiguration : IEntityTypeConfiguration<UserReport>
    {
        public void Configure(EntityTypeBuilder<UserReport> builder)
        {
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.ReportUser).WithMany().HasForeignKey(x => x.ReportUserId);
        }
    }
}

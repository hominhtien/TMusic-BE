
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class PackageVip : AuditedEntity<Guid>
    {
        public string PackageVipName { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal? PackageVipPrice { get; set; }
        public int TimeVip { get; set; }
        public string Status { get; set; }
        public string ImageLink { get; set; }
    }
    public class PackageVipEntityConfiguration : IEntityTypeConfiguration<PackageVip>
    {
        public void Configure(EntityTypeBuilder<PackageVip> builder)
        {
            builder.Property(x => x.PackageVipName).IsRequired();
            builder.Property(x => x.OriginalPrice).IsRequired();
            builder.Property(x => x.PackageVipPrice).IsRequired();
            builder.Property(x => x.TimeVip).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}

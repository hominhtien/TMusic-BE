
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class SongView : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid SongId { get; set; }
        public virtual Song Song { get; set; }
    }
    public class SongViewEntityConfiguration : IEntityTypeConfiguration<SongView>
    {
        public void Configure(EntityTypeBuilder<SongView> builder)
        {
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Song).WithMany().HasForeignKey(x => x.SongId);
        }
    }
}

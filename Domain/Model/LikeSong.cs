
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class LikeSong : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid SongId { get; set; }
        public virtual Song Song { get; set; }  
    }
    internal class LikeSongEntityConfiguration : IEntityTypeConfiguration<LikeSong>
    {
        public void Configure(EntityTypeBuilder<LikeSong> builder)
        {
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.SongId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Song).WithMany().HasForeignKey(x => x.SongId);
        }
    }
}

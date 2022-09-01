using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class PlaylistDetail : AuditedEntity<Guid>
    {
        public Guid PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }
        public Guid SongId { get; set; }
        public virtual Song Song { get; set; }
    }
    public class PlaylistDetailEntityConfiguration : IEntityTypeConfiguration<PlaylistDetail>
    {
        public void Configure(EntityTypeBuilder<PlaylistDetail> builder)
        {
            builder.HasOne(x => x.Playlist).WithMany().HasForeignKey(x => x.PlaylistId);
            builder.HasOne(x => x.Song).WithMany().HasForeignKey(x => x.SongId);
        }
    }
}

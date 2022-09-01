
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class LikePlaylist : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
    internal class LikePlaylistEntityConfiguration : IEntityTypeConfiguration<LikePlaylist>
    {
        public void Configure(EntityTypeBuilder<LikePlaylist> builder)
        {

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.PlaylistId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Playlist).WithMany().HasForeignKey(x => x.PlaylistId);
        }
    }
}

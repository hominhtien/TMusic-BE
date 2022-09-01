
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class SongPlaylistCategoryDetail : AuditedEntity<Guid>
    {
        public Guid SongId { get; set; }
        public virtual Song Song { get; set; }

        public Guid PlaylistCategoryId { get; set; }
        public virtual PlaylistCategory PlaylistCategory { get; set; }
    }
    public class SongPlaylistCategoryDetailEntityConfiguration : IEntityTypeConfiguration<SongPlaylistCategoryDetail>
    {
        public void Configure(EntityTypeBuilder<SongPlaylistCategoryDetail> builder)
        {

            builder.HasOne(x => x.Song)
                .WithMany(e => e.SongPlaylistCategoryDetails).HasForeignKey(x => x.SongId);

            builder.HasOne(x => x.PlaylistCategory)
                .WithMany(e => e.SongPlaylistCategoryDetails).HasForeignKey(x => x.PlaylistCategoryId);
        }
    }
}

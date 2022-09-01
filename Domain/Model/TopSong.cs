
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class TopSong : AuditedEntity<Guid>
    {
        public string TopName { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }  
        public string Desciption { get; set; }
        public Guid PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }

        public string ImageLink { get; set; }

        public bool Enable { get; set; }
    }
    public class TopSongEntityConfiguration : IEntityTypeConfiguration<TopSong>
    {
        public void Configure(EntityTypeBuilder<TopSong> builder)
        {
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.Playlist).WithMany().HasForeignKey(x => x.PlaylistId);
        }
    }
}

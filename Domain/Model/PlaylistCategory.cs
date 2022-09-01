using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class PlaylistCategory : AuditedEntity<Guid>
    {
        public string PlaylistCategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public virtual ICollection<SongPlaylistCategoryDetail> SongPlaylistCategoryDetails { get; set; }

    }
    public class PlaylistCategoryEntityConfiguration : IEntityTypeConfiguration<PlaylistCategory>
    {
        public void Configure(EntityTypeBuilder<PlaylistCategory> builder)
        {
            builder.Property(x => x.PlaylistCategoryName).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.HasIndex(x => x.PlaylistCategoryName);
            builder.HasIndex(x => x.CategoryId);
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
        }
    }
}


using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class CategorySongDetail : AuditedEntity<Guid>
    {

        public Guid SongId { get; set; }
        public virtual Song Song { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
    public class CategorySongDetailEntityConfiguration : IEntityTypeConfiguration<CategorySongDetail>
    {
        public void Configure(EntityTypeBuilder<CategorySongDetail> builder)
        {

            builder.HasOne(x => x.Song)
                .WithMany(e => e.CategoryDetails).HasForeignKey(x => x.SongId);

            builder.HasOne(x => x.Category)
                .WithMany(e => e.CategoryDetails).HasForeignKey(x => x.CategoryId);
        }
    }
}

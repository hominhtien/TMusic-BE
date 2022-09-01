
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class Category : AuditedEntity<Guid>
    {
        public string CategoryName { get; set; }
        public string ImageLink { get; set; }
        public virtual ICollection<CategorySongDetail> CategoryDetails { get; set; }


    }
    internal class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasIndex(x => x.CategoryName);
            builder.HasIndex(x => x.Id);
        }
    }
}

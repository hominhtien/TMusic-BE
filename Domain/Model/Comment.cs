

using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class Comment : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; }

        public string Content { get; set; }
        public Guid ParentId { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> ReverseComment { get; set; }
    }
    internal class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.SongId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Song).WithMany().HasForeignKey(x => x.SongId);

            builder.HasOne(d => d.Parent)
                       .WithMany(d => d.ReverseComment)
                       .HasForeignKey(d => d.ParentId);
        }
    }
}

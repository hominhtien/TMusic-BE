using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Playlist : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string PlaylistName { get; set; }
        public string Type { get; set; } // chế độ
        public string ImageLink { get; set; }
        public bool Enable { get; set; } // vô hiệu hóa
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Desciption { get; set; }

    }
    public class PlaylistEntityConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.Property(x => x.PlaylistName).IsRequired();
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.PlaylistName);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
        }
    }
}

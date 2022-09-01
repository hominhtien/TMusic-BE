
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class Song : AuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string SongName { get; set; }
        public string Desciption { get; set; }
        public int Downloads { get; set; }
        public int SongType { get; set; } //Public, private, share friend
        public int Like { get; set; }
        public string SingerName { get; set; }
        public string Lyrics { get; set; }
        public int View { get; set; }
        public Guid TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public string Advertisement { get; set; } // Quảng cáo
        public long SongTime { get; set; } // thời lượng bài hát tính bằng second
        public string SongLink { get; set; }
        public string ImageLink { get; set; }
        public bool Enable { get; set; }
        public string Disable { get; set; } // vô hiệu hóa
        public virtual ICollection<SongPlaylistCategoryDetail> SongPlaylistCategoryDetails { get; set; }
        public virtual ICollection<CategorySongDetail> CategoryDetails { get; set; }

    }
    public class SongEntityConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Topic).WithMany().HasForeignKey(x => x.TopicId);
        }
    }
}

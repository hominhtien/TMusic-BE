using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class SongReport : AuditedEntity<Guid>
    {
        public string Content { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid UserReportId { get; set; }
        public virtual User UserReport { get; set; }

        public Guid SongId { get; set; }
        public virtual Song Song { get; set; }


        public Guid ReportSongId { get; set; }
        public virtual  Song ReportSong { get; set; }

        public DateTime ProcessingDate { get; set; }

        public string Status { get; set; }
        public string Type { get; set; } // Đã xử lí, chưa xử lí
    }
    public class SongReportEntityConfiguration : IEntityTypeConfiguration<SongReport>
    {
        public void Configure(EntityTypeBuilder<SongReport> builder)
        {
            builder.HasOne(x => x.UserReport).WithMany().HasForeignKey(x => x.UserReportId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Song).WithMany().HasForeignKey(x => x.SongId);
            builder.HasOne(x => x.ReportSong).WithMany().HasForeignKey(x => x.ReportSongId);
        }
    }
}

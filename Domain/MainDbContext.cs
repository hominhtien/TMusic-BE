using Domain.EfCore;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace TMusic.Service.Domain
{
    public class MainDbContext : AppDbContextBase
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySongDetail> CategorySongDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<DownloadSong> DownloadSongs { get; set; }
        public DbSet<LikePlaylist> LikePlaylists { get; set; }
        public DbSet<LikeSong> LikeSongs { get; set; }
        public DbSet<PackageVip> PackageVips { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistCategory> PlaylistCategories { get; set; }
        public DbSet<PlaylistDetail> PlaylistDetails { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongPlaylistCategoryDetail> SongPlaylistCategoryDetails { get; set; }
        public DbSet<SongReport> SongReports { get; set; }
        public DbSet<SongView> SongViews { get; set; }
        public DbSet<TopSong> TopSongs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<UserReport> UserReports { get; set; }
    }
}

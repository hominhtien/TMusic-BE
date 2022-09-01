
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class User : AuditedEntity<Guid>
    {
        public bool Verified { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public string Description { get; set; }
        public string DateOfBirth { get; set; }

        public string Avatar { get; set; }

        public string Sex { get; set; }
        public string Online { get; set; }

        public string Vip { get; set; }

        public DateTime ExpiryVipDate { get; set; }
        public string? Uid { get; set; }
        public bool Enable { get; set; }
        public bool Active { get; set; }
    }
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.Email);
            builder.HasIndex(x => x.FullName);
            builder.HasQueryFilter(x => x.Enable == true);
            builder.HasQueryFilter(x => x.Active == true);
        }
    }
}

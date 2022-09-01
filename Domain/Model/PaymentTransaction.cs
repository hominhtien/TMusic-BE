
using Domain.AuditedEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Model
{
    public class PaymentTransaction : AuditedEntity<Guid>
    {
        public string[] PackageVipIds { get; set; }
        public string ServiceProvider { get; set; } //MOMO, VN PAY
        public decimal? TotalPayment { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string Status { get; set; }
        public string ResponseCode { get; set; }

    }
    public class PaymentTransactionEntityConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.Property(x => x.TotalPayment).IsRequired();
            builder.Property(x => x.ServiceProvider).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}

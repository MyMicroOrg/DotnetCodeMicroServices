using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loaner.Data.Mapping
{
    public partial class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("FIN_Customer_MST");
            builder.HasKey(c => c.Id).HasName("FCM_ID");
            builder.Property(p => p.Id).HasColumnName("FCM_ID");
            builder.Property(p => p.Code).HasColumnName("FCM_Code").HasMaxLength(50);
            builder.Property(p => p.Name).HasColumnName("FCM_Name").HasMaxLength(100);
            builder.Property(p => p.Company).HasColumnName("FCM_CO").HasMaxLength(100);
            builder.Property(p => p.Address).HasColumnName("FCM_Address").HasMaxLength(100);
            builder.Property(p => p.Phone).HasColumnName("FCM_Phone").HasMaxLength(100);
            builder.Property(p => p.PAN).HasColumnName("FCM_PAN").HasMaxLength(50);
            builder.Property(p => p.AADHAR).HasColumnName("FCM_AADHAR").HasMaxLength(50);
            builder.Property(p => p.VoterId).HasColumnName("FCM_VoterID").HasMaxLength(50);
            builder.Property(p => p.Remarks).HasColumnName("FCM_Remarks").HasMaxLength(200);
            builder.Property(p => p.Status).HasColumnName("FCM_Status").HasMaxLength(1);
            builder.Property(p => p.CreatedOnDateUtc).HasColumnName("Created_On_UTC");
            builder.Property(p => p.CreatedBy).HasColumnName("Created_By").HasMaxLength(50);
            builder.Property(p => p.LastUpdatedOnUtc).HasColumnName("LastUpdated_ON_UTC");
            builder.Property(p => p.LastUpdateBy).HasColumnName("LastUpdated_BY").HasMaxLength(50);
            builder.Property(p => p.AuthOnUtc).HasColumnName("Auth_On_UTC");
            builder.Property(p => p.AuthBy).HasColumnName("Auth_By").HasMaxLength(50);
        }
    }
}
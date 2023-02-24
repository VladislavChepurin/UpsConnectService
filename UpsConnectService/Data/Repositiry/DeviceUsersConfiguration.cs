using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpsConnectService.Models.Devices;

namespace UpsConnectService.Data.Repositiry
{
    public class DeviceUsersConfiguration : IEntityTypeConfiguration<DeviceUsers>
    {
        public void Configure(EntityTypeBuilder<DeviceUsers> builder)
        {
            builder.ToTable("DeviceUsers").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}

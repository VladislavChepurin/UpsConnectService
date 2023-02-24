using Microsoft.EntityFrameworkCore;
using UpsConnectService.Models.Devices;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UpsConnectService.Data.Repositiry
{
    public class DeviceHistoryConfiguration : IEntityTypeConfiguration<DataDeviceRequestExt>
    {
        public void Configure(EntityTypeBuilder<DataDeviceRequestExt> builder)
        {
            builder.ToTable("DeviceHistory").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}

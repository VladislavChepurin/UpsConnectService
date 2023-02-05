using Microsoft.EntityFrameworkCore;
using UpsConnectService.Models.Devices;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UpsConnectService.Data.Repositiry
{
    public class DeviceBaseExConfiguration : IEntityTypeConfiguration<DeviceBaseExt>
    {
        public void Configure(EntityTypeBuilder<DeviceBaseExt> builder)
        {
            builder.ToTable("DeviceBase").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }ghkjln.km
    }
}

using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Data.Repositiry;

public class DeviceRepository: Repository<Device>
{
    public DeviceRepository(ApplicationDbContext db) : base(db)
    {

    }

    public void AddDevice(User target, string serial)
    {

    }

    public List<string> getDeviceByUser(User target)
    {

    }

    public void DeleteDevice(User target, string serial)
    {

    }

}

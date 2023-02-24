using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Data.Repositiry;

public class DeviceUsersRepository: Repository<DeviceUsers>
{
    public DeviceUsersRepository(ApplicationDbContext db) : base(db)
    {

    }

    public void AddDevice(User target, string name, string serial)
    {
        var device = Set.AsEnumerable().FirstOrDefault(x => x.UserId == target.Id && x.SerialNumber == serial);

        if (device == null)
        {
            var item = new DeviceUsers()
            {
                UserId = target.Id,
                User = target,
                NameDevice = name,
                SerialNumber = serial              
            };
            Create(item);
        }
    }

    public List<DeviceUsers> getDeviceByUser(User target)
    {
        var device = Set.AsEnumerable().Where(x => x?.User?.Id == target.Id);
        return device.ToList();
    }

    public void DeleteDevice(User target, string serial)
    {
        var device = Set.AsEnumerable().FirstOrDefault(x => x.User?.Id == target.Id && x.SerialNumber == serial);
        if (device != null)
        {
            Delete(device);
        }
    }
}

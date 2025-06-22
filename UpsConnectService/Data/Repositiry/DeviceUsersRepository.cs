using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Data.Repositiry;

public class DeviceUsersRepository: Repository<DeviceUsers>
{
    public DeviceUsersRepository(ApplicationDbContext db) : base(db)
    {

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

    public void UpdateOrCreateDevice(User target, DeviceUsers model)
    {
        // Ищем устройство по ID (если оно есть в модели)
        var device = model.Id != 0
            ? Set.FirstOrDefault(x => x.Id == model.Id)
            : Set.FirstOrDefault(x => x.UserId == target.Id && x.SerialNumber == model.SerialNumber);

        if (device == null)
        {
            // Создаем новое устройство
            var item = new DeviceUsers()
            {
                UserId = target.Id,
                User = target,
                NameDevice = model.NameDevice,
                SerialNumber = model.SerialNumber
            };
            Create(item);
        }
        else
        {
            // Обновляем существующее устройство
            device.NameDevice = model.NameDevice;
            device.SerialNumber = model.SerialNumber;
            Update(device);
        }
    }
}

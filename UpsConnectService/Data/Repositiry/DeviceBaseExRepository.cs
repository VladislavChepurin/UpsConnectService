using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Data.Repositiry;

public class DeviceBaseExRepository : Repository<DeviceBaseExt>
{
    public DeviceBaseExRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void AddDeviceToHistory(DeviceBaseExt device)
    {
        throw new NotImplementedException();
    }

    public List<DeviceBaseExt> getDeviceAllTimePeriod(DateTime date1, DateTime date2)
    {
        throw new NotImplementedException();
    }

    public List<DeviceBaseExt> getDeviceUserTimePeriod(DateTime date1, DateTime date2, User user)
    {
        throw new NotImplementedException();
    }

    public List<DeviceBaseExt> getDeviceAllHistory()
    {
        throw new NotImplementedException();
    }

    public List<DeviceBaseExt> getDeviceUserFullHistory(User user)
    {
        throw new NotImplementedException();
    }

    public List<DeviceBaseExt> getDeviceUserLastHistory(User user)
    {
        throw new NotImplementedException();
    }

    public void DeleteOldRequest (IEnumerable<DeviceBaseExt> devices)
    {
        throw new NotImplementedException();
    }

}

using AutoMapper;
using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Data.Repositiry;


public class DeviceHistoryRepository : Repository<DataDeviceRequestExt>
{

    private readonly IMapper _mapper;

    public DeviceHistoryRepository(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }

    public void AddDeviceToHistory(DataDeviceRequest request)
    {
        if (request != null)
        {
            var item = _mapper.Map<DataDeviceRequestExt>(request);
            item.DateTime = DateTime.Now;  
            Create(item);
        }               
    }

    public List<DataDeviceRequestExt> GetDeviceAllTimePeriod(DateTime date1, DateTime date2)
    {
        throw new NotImplementedException();
    }

    public List<DataDeviceRequestExt> getDeviceUserTimePeriod(DateTime date1, DateTime date2, User user)
    {
        throw new NotImplementedException();
    }

    public List<DataDeviceRequestExt> getDeviceAllHistory()
    {
        throw new NotImplementedException();
    }

    public List<DataDeviceRequestExt> getDeviceUserFullHistory(User user)
    {
        throw new NotImplementedException();
    }

    public List<DataDeviceRequestExt> getDeviceUserLastHistory(User user)
    {
        throw new NotImplementedException();
    }

    public void DeleteOldRequest (IEnumerable<DataDeviceRequestExt> devices)
    {
        throw new NotImplementedException();
    }

}

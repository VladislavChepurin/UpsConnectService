using UpsConnectService.Data.Repositiry;
using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Data.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        //DeviceRepository Device { get; }

        void SaveChanges();
        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class;

        List<Device> GetAllDevices(User user);
    }
}

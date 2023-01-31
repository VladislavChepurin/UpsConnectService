using Microsoft.EntityFrameworkCore.Infrastructure;
using UpsConnectService.Data.Repositiry;
using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _appContext;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(ApplicationDbContext app)
    {
        this._appContext = app;
    }

    public void Dispose()
    {

    }

    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<Type, object>();
        }

        if (hasCustomRepository)
        {
            var customRepo = _appContext.GetService<IRepository<TEntity>>();
            if (customRepo != null)
            {
                return customRepo;
            }
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(_appContext);
        }

        return (IRepository<TEntity>)_repositories[type];

    }
    public void SaveChanges()
    {
        _appContext.SaveChanges();
    }

    private List<Device> GetAllDevices(User user)
    {
        var repository = GetRepository<Device>() as DeviceRepository;
        return repository.getDeviceByUser(user);
    }
}

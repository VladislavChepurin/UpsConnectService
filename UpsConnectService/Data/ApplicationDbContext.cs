using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UpsConnectService.Data.Repositiry;
using UpsConnectService.Models.Users;

namespace UpsConnectService.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new DeviceUsersConfiguration());
        builder.ApplyConfiguration(new DeviceHistoryConfiguration());
    }
}

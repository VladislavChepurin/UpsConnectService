using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Hubs;
using UpsConnectService;
using UpsConnectService.Data;
using UpsConnectService.Data.Repositiry;
using UpsConnectService.Data.UoW;
using UpsConnectService.Extentions;
using UpsConnectService.Models.Devices;
using UpsConnectService.Models.Users;
using UpsConnectService.Repository;
using UpsConnectService.Validation;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

// Add services to the container.
services.AddControllersWithViews();

services.AddValidatorsFromAssemblyContaining<DataDeviceRequestValidator>(); // register validators
services.AddScoped<IValidator<DataDeviceRequest>, DataDeviceRequestValidator>();

services.AddTransient<IUnitOfWork, UnitOfWork>();
services.AddTransient<IUnitOfWork, UnitOfWork>();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection))
    .AddUnitOfWork()
    .AddCustomRepository<DeviceUsers, DeviceUsersRepository>()
    .AddCustomRepository<DataDeviceRequestExt, DeviceHistoryRepository>()
    .AddIdentity<User, IdentityRole>(opts =>
    {
        opts.Password.RequiredLength = 5;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);

services.AddScoped<IRoleRepository, RoleRepository>();

//Build services
var app = builder.Build();
//--------------------------------------------------
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
app.UseWebSockets(webSocketOptions);

//--------------------------------------------------
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapHub<DataHub>("/chatHub");

app.Run();







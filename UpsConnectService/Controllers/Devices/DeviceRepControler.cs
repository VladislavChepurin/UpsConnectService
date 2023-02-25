using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using UpsConnectService.Data.Repositiry;
using UpsConnectService.Data.UoW;
using UpsConnectService.Models.Devices;

namespace UpsConnectService.Controllers.Devices;

public class DeviceRepControler: Controller
{
    private readonly IHubContext<DataHub> _hubContext;
    private readonly IValidator<DataDeviceRequest> _validator;
    private readonly ILogger<DeviceRepControler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeviceRepControler(IHubContext<DataHub> hubContext, IValidator<DataDeviceRequest> validator, ILogger<DeviceRepControler> logger, IUnitOfWork unitOfWork)
	{
        _hubContext= hubContext;
        _validator= validator;
        _logger= logger;
        _unitOfWork= unitOfWork;
	}


    [HttpPost]
    [Route("DeviceServices")]
    public async Task<IActionResult> DataDevice([FromBody] DataDeviceRequest request)
    {
        ValidationResult result = await _validator.ValidateAsync(request);

        if (result.IsValid) 
        {
            //Record to DataBase
            var repository = _unitOfWork.GetRepository<DataDeviceRequestExt>() as DeviceHistoryRepository;
            repository?.AddDeviceToHistory(request);
            _unitOfWork.SaveChanges();

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", request);
            return StatusCode(200, $"Is OK {request.SerialNumber}: {DateTime.Now:G}");
             
        }
        _logger.LogInformation($"Неверные данные от устройства {request.SerialNumber}: {DateTime.Now:G}" + result.ToString());
        return StatusCode(400);
    }
 }

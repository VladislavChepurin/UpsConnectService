using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using UpsConnectService.Models.Devices;

namespace UpsConnectService.Controllers.Devices;

public class DeviceRepControler: Controller
{
    IHubContext<DataHub> _hubContext;
    private IValidator<DataDeviceRequest> _validator;
    private readonly ILogger<DeviceRepControler> _logger;

    public DeviceRepControler(IHubContext<DataHub> hubContext, IValidator<DataDeviceRequest> validator, ILogger<DeviceRepControler> logger)
	{
        _hubContext= hubContext;
        _validator= validator;
        _logger= logger;
	}

    [HttpPost]
    [Route("DeviceServices")]
    public async Task<IActionResult> DataDevice([FromBody] DataDeviceRequest request)
    {
        ValidationResult result = await _validator.ValidateAsync(request);

        if (result.IsValid) {

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", request.SerialNumber, request.NameDevice, request.StatusCode, request.InputVoltage, request.OutputVoltage);
            return StatusCode(200, $"Данные получены");

        }
        _logger.LogInformation($"Неверные данные от устройства {DateTime.Now:hh:mm:ss dd.MM.yyyy} \n" + result.ToString());
        return StatusCode(400);
    }
 }

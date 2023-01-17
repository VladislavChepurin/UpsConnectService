using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using UpsConnectService.Models.Device;

namespace UpsConnectService.Controllers.Device;

public class DeviceRepControler: Controller
{
    IHubContext<DataHub> _hubContext;

    public DeviceRepControler(IHubContext<DataHub> hubContext)
	{
        _hubContext= hubContext;
	}

    [HttpPost]
    [Route("DeviceServices")]
    public async Task<IActionResult> Device([FromBody] Devices request)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", request.Name, request.Message);

        return StatusCode(200, $"Данные получены");
    }

 }

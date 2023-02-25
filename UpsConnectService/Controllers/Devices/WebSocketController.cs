using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalRChat.Hubs;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using UpsConnectService.Data.Repositiry;
using UpsConnectService.Data.UoW;
using UpsConnectService.Models.Devices;

namespace UpsConnectService.Controllers.Devices;

public class WebSocketController : ControllerBase
{
    private readonly IHubContext<DataHub> _hubContext;
    private readonly IValidator<DataDeviceRequest> _validator;
    private readonly ILogger<DeviceRepControler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public WebSocketController(IHubContext<DataHub> hubContext, IValidator<DataDeviceRequest> validator, ILogger<DeviceRepControler> logger, IUnitOfWork unitOfWork)
    {
        _hubContext = hubContext;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [Route("/send")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync(new WebSocketAcceptContext { DangerousEnableCompression = true });
            await Echo(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }

    protected async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult? result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        if (result != null)
        {
            while (!result.CloseStatus.HasValue)
            {
                var request = JsonConvert.DeserializeObject<DataDeviceRequest>(Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count)));

                if (request != null)
                {
                    ValidationResult validation = await _validator.ValidateAsync(request);
                    if (validation.IsValid)
                    {
                        //Record to DataBase
                        var repository = _unitOfWork.GetRepository<DataDeviceRequestExt>() as DeviceHistoryRepository;
                        repository?.AddDeviceToHistory(request);
                        _unitOfWork.SaveChanges();

                        await _hubContext.Clients.All.SendAsync("ReceiveMessage", request);
                    }
                    else
                    {
                        _logger.LogInformation($"Неверные данные от устройства {request.SerialNumber} пришли {DateTime.Now:hh:mm:ss dd.MM.yyyy} \n" + validation.ToString());
                    }
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(
                            Encoding.UTF8.GetBytes($"Is OK {request.SerialNumber}: {DateTime.Now:G}")),
                        result.MessageType,
                        result.EndOfMessage,
                        CancellationToken.None);
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }                
            }
        }
        if (result?.CloseStatus != null)
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);        
    }
}

using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR;
using UpsConnectService.Controllers.Devices;
using UpsConnectService.Data.Repositiry;
using UpsConnectService.Data.UoW;
using UpsConnectService.Models.Devices;

namespace SignalRChat.Hubs
{
    public class DataHub : Hub
    {
        private readonly IValidator<DataDeviceRequest> _validator;
        private readonly ILogger<DeviceRepControler> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public DataHub(IValidator<DataDeviceRequest> validator, ILogger<DeviceRepControler> logger, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task SendMessage(DataDeviceRequest request)
        {

            ValidationResult validation = await _validator.ValidateAsync(request);
            if (validation.IsValid)
            {
                //Record to DataBase
                var repository = _unitOfWork.GetRepository<DataDeviceRequestExt>() as DeviceHistoryRepository;
                repository?.AddDeviceToHistory(request);
                _unitOfWork.SaveChanges();

                await Clients.All.SendAsync("ReceiveMessage", request);
                SendResponse(Context.ConnectionId, $"Is OK {request.SerialNumber}: {DateTime.Now:G}");
            }    
            else
            {
                _logger.LogInformation($"Неверные данные от устройства {request.SerialNumber} пришли {DateTime.Now:hh:mm:ss dd.MM.yyyy} \n" + validation.ToString());
            }           
        }

        public void SendResponse(string сonnectionId, string message)
        {
            Clients.Client(сonnectionId).SendAsync("ResponseMessage", message);
        }
    }
}

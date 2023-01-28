using FluentValidation;
using UpsConnectService.Models.Devices;

namespace UpsConnectService.Validation
{
    public class DataDeviceRequestValidator : AbstractValidator<DataDeviceRequest>
    {
        public DataDeviceRequestValidator()
        {
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.NameDevice).NotEmpty();
            RuleFor(x => x.StatusCode).NotEmpty();
            RuleFor(x => x.InputVoltage).NotEmpty().InclusiveBetween(150, 260);
            RuleFor(x => x.OutputVoltage).NotEmpty().InclusiveBetween(180, 230);
            RuleFor(x => x.InputСurrent).NotEmpty();
            RuleFor(x => x.OutputCurrent).NotEmpty();
            RuleFor(x => x.BatteryCharge).NotEmpty();
            RuleFor(x => x.Bypass).NotEmpty();
            //RuleFor(x => x.GeneralError).NotEmpty();
            //RuleFor(x => x.BatteryError).NotEmpty();
        }
    }
}

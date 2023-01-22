using AutoMapper;
using UpsConnectService.Models.Users;
using UpsConnectService.ViewModels.Users;

namespace UpsConnectService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()               
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
        }
    }
}

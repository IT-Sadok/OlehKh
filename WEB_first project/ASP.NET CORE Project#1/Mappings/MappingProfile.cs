using AutoMapper;
using ASP.NET_CORE_Project_1.DTO;
using ASP.NET_CORE_Project_1.Models;

namespace ASP.NET_CORE_Project_1.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseSignUpModel, Account>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "passenger"));

            CreateMap<DriverSignUpModel, Account>()
                .ForMember(dest => dest.DrivingExperienceYears, opt => opt.MapFrom(src => src.Experience))
                .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.CarModel))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "driver"));
        }
    }
}

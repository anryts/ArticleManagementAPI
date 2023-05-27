using AutoMapper;
using Common.DTOs;
using Gateway.Data.Entities;

namespace APIGateway.Profiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<User, UserCreationDto>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.PasswordHash));
    }
}
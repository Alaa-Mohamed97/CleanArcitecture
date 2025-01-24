using AutoMapper;
using CleanArcitecture.Core.Features.ApplicationUser.Command.Models;
using CleanArcitecture.Core.Features.ApplicationUser.Queries.DTOs;
using CleanArcitecture.Domain.Entities;

namespace CleanArcitecture.Core.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, AddUserCommand>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
            CreateMap<User, EditUserCommand>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
            CreateMap<User, UserListDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
            CreateMap<User, UserDetailsDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
        }
    }
}

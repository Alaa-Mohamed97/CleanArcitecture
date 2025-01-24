using AutoMapper;
using CleanArcitecture.Core.Features.Role.Queries.DTOs;
using Microsoft.AspNetCore.Identity;

namespace CleanArcitecture.Core.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole<int>, RoleListDto>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ReverseMap();
            CreateMap<IdentityRole<int>, RoleDetailsDto>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ReverseMap();
        }
    }
}

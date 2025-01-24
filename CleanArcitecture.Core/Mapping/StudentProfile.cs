using AutoMapper;
using CleanArcitecture.Core.Features.Students.Command.Models;
using CleanArcitecture.Core.Features.Students.Queries.DTOs;
using CleanArcitecture.Core.Helpers;
using CleanArcitecture.Domain.Entities;

namespace CleanArcitecture.Core.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentListDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Localized.IsEn() ? src.NameEn : src.NameAr))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DName))
                .ReverseMap();
            CreateMap<Student, StudentDetailsDto>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Localized.IsEn() ? src.NameEn : src.NameAr))
              .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DName))
              .ReverseMap();
            CreateMap<Student, AddStudentRequestCommand>()
              .ReverseMap();
            CreateMap<Student, EditStudentCommand>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
              .ReverseMap();
        }
    }
}

using AutoMapper;
using BelHard_TestWebApi.Controllers;
using BelHard_TestWebApi.DTO;
using BelHard_TestWebApi.Models;

namespace BelHard_TestWebApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}

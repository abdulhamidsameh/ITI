using AutoMapper;
using ITI.DAL.Models;
using ITI.PL.ViewModels;

namespace ITI.PL.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Student,StudentViewModel>().ReverseMap();
            CreateMap<Department,DepartmentViewModel>().ReverseMap();
        }
    }
}

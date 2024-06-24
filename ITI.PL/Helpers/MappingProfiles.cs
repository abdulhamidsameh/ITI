using AutoMapper;
using ITI.DAL.Models;
using ITI.PL.ViewModels.Department;
using ITI.PL.ViewModels.Student;

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

﻿using AutoMapper;
using ITI.DAL.Models;
using ITI.PL.ViewModels.Department;
using ITI.PL.ViewModels.Student;
using ITI.PL.ViewModels.Topic;

namespace ITI.PL.Helpers
{
    public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Student,StudentViewModel>().ReverseMap();
			CreateMap<Department,DepartmentViewModel>().ReverseMap();
            CreateMap<Topic,TopicViewModel>().ReverseMap();
        }
    }
}

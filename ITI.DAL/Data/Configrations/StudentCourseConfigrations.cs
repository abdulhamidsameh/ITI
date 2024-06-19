using ITI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Data.Configrations
{
	internal class StudentCourseConfigrations : IEntityTypeConfiguration<StudentCourse>
	{
		public void Configure(EntityTypeBuilder<StudentCourse> builder)
		{
			builder.HasKey("StudentId", "CourseId");
			builder.HasOne(SC => SC.Course)
				.WithMany(C => C.Students)
				.HasForeignKey(SC => SC.CourseId);
			builder.HasOne(SC => SC.Student)
				.WithMany(S => S.Courses)
				.HasForeignKey(SC => SC.StudentId);
		}
	}
}

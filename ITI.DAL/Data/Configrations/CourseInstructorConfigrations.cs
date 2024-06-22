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
	internal class CourseInstructorConfigrations : IEntityTypeConfiguration<CourseInstructor>
	{
		public void Configure(EntityTypeBuilder<CourseInstructor> builder)
		{
			builder.HasKey("InstructorId", "CourseId");
			builder.HasOne(CI => CI.Course)
				.WithMany(C => C.Instructors)
				.HasForeignKey(CI => CI.CourseId);
			builder.HasOne(CI => CI.Instructor)
				.WithMany(I => I.Courses)
				.HasForeignKey(CI => CI.InstructorId);
		}
	}
}

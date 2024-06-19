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
	internal class CourseConfigrations : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.Property(C => C.Id).UseIdentityColumn(10, 10);
			builder.Property(C => C.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(C => C.Description).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.HasOne(C => C.Topic)
				.WithMany(T => T.Courses)
				.HasForeignKey(C => C.TopicId);
		}
	}
}

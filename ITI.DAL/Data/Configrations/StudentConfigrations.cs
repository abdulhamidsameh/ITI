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
	internal class StudentConfigrations : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.Property(S => S.FirstName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
			builder.Property(S => S.LastName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
			builder.Property(S => S.Address).IsRequired().HasMaxLength(50).HasColumnType("varchar");
			builder.HasOne(S => S.Department)
				.WithMany(D => D.Students)
				.HasForeignKey(S => S.DepartmentId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

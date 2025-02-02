﻿using ITI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Data.Configrations
{
	internal class DepartmentConfigrations : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(D => D.Id).UseIdentityColumn(10, 10);
			builder.Property(D => D.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(D => D.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.HasMany(D => D.Instructors)
				.WithOne(I => I.Department)
				.HasForeignKey(I => I.DepartmentId);

			builder.HasOne(D => D.Instructor)
				.WithOne()
				.HasForeignKey<Department>(D => D.InstructorId)
				.OnDelete(DeleteBehavior.NoAction);
				
		}
	}
}

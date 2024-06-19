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
	internal class InstructorConfigrations : IEntityTypeConfiguration<Instructor>
	{
		public void Configure(EntityTypeBuilder<Instructor> builder)
		{
			builder.Property(I => I.Id).UseIdentityColumn(10, 10);
			builder.Property(I => I.Name).IsRequired().HasMaxLength(50).HasColumnType("varchar");
			builder.Property(I => I.Address).IsRequired().HasMaxLength(50).HasColumnType("varchar");
			builder.Property(I => I.Bouns).HasColumnType("decimal(12,2)");
			builder.Property(I => I.Salary).HasColumnType("decimal(12,2)");
			builder.Property(I => I.HourRate).HasColumnType("decimal(12,2)");
		}
	}
}

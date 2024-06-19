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
	internal class TopicConfigrations : IEntityTypeConfiguration<Topic>
	{
		public void Configure(EntityTypeBuilder<Topic> builder)
		{
			builder.Property(T => T.Id).UseIdentityColumn(10, 10);
			builder.Property(T => T.Name).IsRequired().HasMaxLength(50).HasColumnType("varchar");
		}
	}
}

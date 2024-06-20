using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Models
{
	public class StudentCourse : BaseEntity
	{
		public int StudentId { get; set; }
		public int CourseId { get; set; }
		public int Grade { get; set; }

		// NP
		virtual public Student Student { get; set; } = null!;
		virtual public Course Course { get; set; } = null!;
	}
}

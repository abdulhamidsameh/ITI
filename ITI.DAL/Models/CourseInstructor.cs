using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Models
{
	public class CourseInstructor : BaseEntity
	{
		public int InstructorId { get; set; }
		public int CourseId { get; set; }
		public int Evaluation { get; set; }

		// NP
		virtual public Instructor Instructor { get; set; } = null!;
		virtual public Course Course { get; set; } = null!;
	}
}

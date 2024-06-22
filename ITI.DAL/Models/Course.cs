using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Models
{
	public class Course : BaseEntity
	{
		public string Name { get; set; } = null!;
		public int Duration { get; set; }
		public string Description { get; set; } = null!;

		// FK
		public int TopicId { get; set; }
		// NP
		virtual public Topic Topic { get; set; } = null!;

		//NP
		virtual public ICollection<CourseInstructor> Instructors { get; set; } = new HashSet<CourseInstructor>();

		// NP
		virtual public ICollection<StudentCourse> Students { get; set; } = new HashSet<StudentCourse>();
	}
}

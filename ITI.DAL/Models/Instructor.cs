using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Models
{
	public class Instructor
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public decimal Bouns { get; set; }
		public decimal Salary { get; set; }
		public string Address { get; set; } = null!;
		public decimal HourRate { get; set; }

		// FK
		public int DepartmentId { get; set; }
		// NP
		virtual public Department Department { get; set; } = null!;

		// NP
		virtual public ICollection<CourseInstructor> Courses { get; set; } = new HashSet<CourseInstructor>();
	}
}

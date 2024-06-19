using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Models
{
	public class Student
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public Stream Address { get; set; } = null!;
		public int Age { get; set; }

		// FK
		public int DepartmentId { get; set; }
		// NP
		virtual public Department Department { get; set; } = null!;

		// NP
		virtual public ICollection<StudentCourse> Courses { get; set; } = new HashSet<StudentCourse>();
	}
}

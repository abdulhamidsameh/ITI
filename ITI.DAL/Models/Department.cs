using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Models
{
	public class Department : BaseEntity
	{
		public string Code { get; set; } = null!;
		public string Name { get; set; } = null!;
		//public DateTime HiringDate { get; set; }
		public DateTime DateOfCreation { get; set; }


		// NP
		virtual public ICollection<Student> Students { get; set; } = new HashSet<Student>();

		// NP
		virtual	public ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();

		// FK
		public int? InstructorId { get; set; }
		virtual public Instructor? Instructor { get; set; } = null!;

    }
}

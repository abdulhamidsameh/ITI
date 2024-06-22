using ITI.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels
{
	public class DepartmentViewModel
	{
        public int Id { get; set; }
        [Required]
		public string Code { get; set; } = null!;
		
		[Required]
		[MaxLength(50,ErrorMessage ="Max Length Of Name Must be 50 Char ")]
		[MinLength(2,ErrorMessage = "Min Length Of Name Must be 2 Char ")]
		public string Name { get; set; } = null!;

		[Required]
		public DateTime DateOfCreation { get; set; }


		// NP
		virtual public ICollection<Student> Students { get; set; } = new HashSet<Student>();

		// NP
		virtual public ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();

		// FK
		public int? InstructorId { get; set; }
		virtual public Instructor? Instructor { get; set; } = null!;

	}
}

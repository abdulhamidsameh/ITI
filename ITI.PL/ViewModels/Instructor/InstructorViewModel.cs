using ITI.DAL.Models;
using ITI.PL.ViewModels.Department;
using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels.Instructor
{
	public class InstructorViewModel
	{
        public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		public string Name { get; set; } = null!;

		[Required]
		public decimal Bouns { get; set; }

		[Required]
		public decimal Salary { get; set; }

		[Required]
		[MaxLength(50)]
		public string Address { get; set; } = null!;

		[Required]
		public decimal HourRate { get; set; }

		// FK
		public int? DepartmentId { get; set; }
		// NP
		virtual public DepartmentViewModel? Department { get; set; } = null!;

	}
}

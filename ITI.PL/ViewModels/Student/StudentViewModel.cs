using ITI.DAL.Models;
using ITI.PL.ViewModels.Department;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels.Student
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name Is Required")]
        [Display(Name = "First Name")]
        [MaxLength(25, ErrorMessage = "Max Length Of First Name is 25 Char")]
        [MinLength(5, ErrorMessage = "Min Length Of First Name is 5 Char")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name Is Required")]
        [Display(Name = "Last Name")]
        [MaxLength(25, ErrorMessage = "Max Length Of Last Name is 25 Char")]
        [MinLength(5, ErrorMessage = "Min Length Of Last Name is 5 Char")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(50, ErrorMessage = "Max Length Of Address is 50 Char")]
        [MinLength(5, ErrorMessage = "Min Length Of Address is 5 Char")]
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; } = null!;

        [Required]
        [Range(22, 30)]
        public int Age { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [RegularExpression(@"^01[0125][0-9]{8}$",
        ErrorMessage = "Please enter correct email address")]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        public IFormFile? Image { get; set; } = null!;

        
		public string? ImageName { get; set; }


		// FK
		public int? DepartmentId { get; set; }
        // NP
        virtual public DepartmentViewModel? Department { get; set; } = null!;
        // NP
        virtual public ICollection<StudentCourse> Courses { get; set; } = new HashSet<StudentCourse>();


    }
}

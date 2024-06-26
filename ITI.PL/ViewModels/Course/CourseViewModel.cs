using ITI.DAL.Models;
using ITI.PL.ViewModels.Topic;
using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels.Course
{
	public class CourseViewModel
	{
        public int Id { get; set; }
        [Required]
		[MaxLength(50)]
		public string Name { get; set; } = null!;

		[Required]
		[Range(1,3)]
		public int Duration { get; set; }

		[Required]
		[MaxLength(255)]
		public string Description { get; set; } = null!;

		// FK
		public int? TopicId { get; set; }
		// NP
		public TopicViewModel? Topic { get; set; } = null!;

		


	}
}

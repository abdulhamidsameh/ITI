using System.ComponentModel.DataAnnotations;

namespace ITI.PL.ViewModels.Topic
{
	public class TopicViewModel
	{
        public int Id { get; set; }
        [Required]
		[MaxLength(50)]
		public string Name { get; set; } = null!;
    }
}

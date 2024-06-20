﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DAL.Models
{
	public class Student : BaseEntity
	{
		[Required]
		public string FirstName { get; set; } = null!;
		[Required]

		public string LastName { get; set; } = null!;
		[Required]

		public string Address { get; set; } = null!;
		[Required]

		public int Age { get; set; }

		// FK
		public int? DepartmentId { get; set; }
		// NP
		virtual public Department? Department { get; set; } = null!;

		// NP
		virtual public ICollection<StudentCourse> Courses { get; set; } = new HashSet<StudentCourse>();
	}
}

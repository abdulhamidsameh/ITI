using ITI.BLL.Interfaces;
using ITI.DAL.Data;
using ITI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.BLL.Repositories
{
	public class StudentRepository : GenericRepository<Student>, IStudentRepository, IGenericRepository<Student>
	{

		public StudentRepository(ApplicationDbContext dbContext)
			: base(dbContext)
		{
		}

		public IQueryable<Student> GetStudentsByAddress(string address)
		{
			return _dbContext.Students.Where(S => string.Equals(S.Address, address, StringComparison.OrdinalIgnoreCase));
		}

	}
}

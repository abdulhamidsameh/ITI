using ITI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.BLL.Interfaces
{
	public interface IStudentRepository : IGenericRepository<Student>
	{
		IQueryable<Student> GetStudentsByAddress(string address);
	}
}

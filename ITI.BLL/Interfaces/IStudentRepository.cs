using ITI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.BLL.Interfaces
{
	public interface IStudentRepository
	{
		IEnumerable<Student> GetAll();
		Student Get(int id);
		int Add(Student entity);
		int Update(Student entity);
		int Delete(Student entity);
	}
}

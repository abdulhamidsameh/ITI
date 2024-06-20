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
	public class StudentRepository : IStudentRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public StudentRepository(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
		}
        public int Add(Student entity)
		{
			_dbContext.Students.Add(entity);
			return _dbContext.SaveChanges();
		}

		public int Delete(Student entity)
		{
			_dbContext.Students.Remove(entity);
			return _dbContext.SaveChanges();
		}

		public Student Get(int id)
		{
			return _dbContext.Students.Where(S => S.Id == id).Include(S => S.Department).FirstOrDefault()!;
		}

		public IEnumerable<Student> GetAll()
		{
			return _dbContext.Students.AsNoTracking().ToList();
		}

		public int Update(Student entity)
		{
			_dbContext.Students.Update(entity);
			return _dbContext.SaveChanges();
		}
	}
}

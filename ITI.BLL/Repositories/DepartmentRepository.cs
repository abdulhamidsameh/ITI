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
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public DepartmentRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public int Add(Department entity)
		{
			_dbContext.Departments.Add(entity);
			return _dbContext.SaveChanges();
		}

		public int Delete(Department entity)
		{
			_dbContext.Departments.Remove(entity);
			return _dbContext.SaveChanges();
		}

		public Department Get(int id)
		{
			//return _dbContext.Find<Department>(id)!;
			var department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
			if (department is null)
				department = _dbContext.Departments.Where(D => D.Id == id).Include(D => D.Instructor).FirstOrDefault();
			return department!;
		}

		public IEnumerable<Department> GetAll()
		{
			return _dbContext.Departments.Include(D => D.Instructor).AsNoTracking().ToList();
		}

		public int Update(Department entity)
		{
			_dbContext.Departments.Update(entity);
			return _dbContext.SaveChanges();
		}
	}
}

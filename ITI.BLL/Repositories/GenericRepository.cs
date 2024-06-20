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
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private protected readonly ApplicationDbContext _dbContext;

		public GenericRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public int Add(T entity)
		{
			_dbContext.Add(entity);
			return _dbContext.SaveChanges();
		}

		public int Delete(T entity)
		{
			_dbContext.Remove(entity);
			return _dbContext.SaveChanges();
		}

		public T Get(int id)
		{
			return _dbContext.Set<T>().Where(D => D.Id == id).FirstOrDefault()!;

		}

		public IEnumerable<T> GetAll()
		{
			return _dbContext.Set<T>().AsNoTracking().ToList();
		}

		public int Update(T entity)
		{
			_dbContext.Update(entity);
			return _dbContext.SaveChanges();
		}
	}
}

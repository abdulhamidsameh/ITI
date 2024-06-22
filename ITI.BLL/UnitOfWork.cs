using ITI.BLL.Interfaces;
using ITI.BLL.Repositories;
using ITI.DAL.Data;
using ITI.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.BLL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;
		private Hashtable _repositories;
		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new Hashtable();
		}


		public int Complete()
			=> _dbContext.SaveChanges();

		public void Dispose()
			=> _dbContext.Dispose();

		public IGenericRepository<T> Repository<T>() where T : BaseEntity
		{
			var key = typeof(T).Name;

			if (!_repositories.ContainsKey(key))
			{
				if (key == nameof(Student))
				{
					var repo = new StudentRepository(_dbContext);
					_repositories.Add(key, repo);
				}
				else
				{
					var repo = new GenericRepository<T>(_dbContext);
					_repositories.Add(key, repo);
				}
			}

			return _repositories[key] as IGenericRepository<T>;
		}
	}
}

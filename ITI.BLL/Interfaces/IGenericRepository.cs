using ITI.BLL.Specifications;
using ITI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.BLL.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		IEnumerable<T> GetAll();
		IEnumerable<T> GetWithSpecAll(ISpecifications<T> spec);
		T Get(int id);
		T GetWithSpec(ISpecifications<T> spec);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}

﻿using ITI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.BLL.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		public IGenericRepository<T> Repository<T>() where T : BaseEntity;
		public int Complete();
	}
}

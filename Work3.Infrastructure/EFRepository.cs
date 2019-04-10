using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Work3.Core.Entities;
using Work3.Core.Interfaces;

namespace Work3.Infrastructure
{
	public class EFRepository<T> 
		: IAsyncRepository<T> where T 
		: BaseEntity
	{
		protected readonly StudentContext DbContext;

		public EFRepository(StudentContext dbContext)
		{
			DbContext = dbContext;
		}
		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await DbContext.Set<T>().FindAsync(id);
		}

		public Task<IReadOnlyList<T>> ListAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T> AddAsync(T entity)
		{
			throw new NotImplementedException();
		}
	}
}

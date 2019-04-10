using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Work3.Core.Entities;

namespace Work3.Core.Interfaces
{
	public interface IAsyncRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> ListAllAsync();
		Task<T> AddAsync(T entity);
	}
}

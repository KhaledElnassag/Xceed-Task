using DemoDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBll.Interfaces
{
	public interface IGenericRepository<T>:IAsyncDisposable where T : class
	{
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllWithSpecAsync(Func<IQueryable<T>, IQueryable<T>> spec);

        Task Add(T Item);
		void Update(T Item);
		void Delete(T Item);
        Task<int> Complete();
    }
}

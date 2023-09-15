using DemoBll.Interfaces;
using DemoDAl.Contexts;
using DemoDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBll.Repositories
{
	public class GenericRopsitory<T> : IGenericRepository<T> where T : class
	{
		private readonly MVCContext _dbContext;
		public GenericRopsitory(MVCContext dbContext) 
		{
			_dbContext = dbContext;
		}
		public async Task Add(T Item)
		=> await _dbContext.Set<T>().AddAsync(Item);
		

		public void Delete(T Item)
		=>	_dbContext.Set<T>().Remove(Item);

        public void Update(T item)
		=>_dbContext.Set<T>().Update(item);

        

		

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(Func<IQueryable<T>, IQueryable<T>> spec)
        {
            return await spec(_dbContext.Set<T>()).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async ValueTask DisposeAsync()
        =>await _dbContext.DisposeAsync();

        public async Task<int> Complete()
        => await _dbContext.SaveChangesAsync();
    }
}

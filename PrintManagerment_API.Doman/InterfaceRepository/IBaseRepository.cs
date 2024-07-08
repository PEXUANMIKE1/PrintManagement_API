using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Doman.InterfaceRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(int id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities);
    }
}

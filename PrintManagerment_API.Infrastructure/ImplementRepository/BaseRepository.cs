using Microsoft.EntityFrameworkCore;
using PrintManagerment_API.Doman.InterfaceRepositories;
using PrintManagerment_API.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Infrastructure.ImplementRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        #region Protected Member
        protected IDbContext _iDbContext = null;
        protected DbSet<TEntity> _dbset;
        protected DbContext _dbContext;
        protected DbSet<TEntity> DbSet 
        {
            get
            {
                if (_dbset == null)
                {
                    _dbset = _dbContext.Set<TEntity>() as DbSet<TEntity>;
                }
                return _dbset; 
            }
        }
        #endregion
        #region Contructor
        public BaseRepository(IDbContext iDbContext)
        {
            _iDbContext = iDbContext;
            _dbContext = (DbContext)iDbContext;
        }
        #endregion
        #region Public async
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _iDbContext.CommitChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            await _iDbContext.CommitChangesAsync();
            return entities;
        }
        public async Task DeleteAsync(int id)
        {
            var dataEntity = await DbSet.FindAsync(id);
            if(dataEntity != null)
            {
                DbSet.Remove(dataEntity);
                await _iDbContext.CommitChangesAsync();
            }
        }
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression != null ? DbSet.Where(expression) : DbSet;
            var dataQuery = query;
            if(dataQuery != null)
            {
                DbSet.RemoveRange(dataQuery);
                await _iDbContext.CommitChangesAsync();
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression != null ? DbSet.Where(expression) : DbSet;
            return query;
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _iDbContext.CommitChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _iDbContext.CommitChangesAsync();
            return entities;
        }
        #endregion
    }
}

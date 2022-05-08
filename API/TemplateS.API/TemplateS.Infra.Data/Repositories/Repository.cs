using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Interfaces;
using TemplateS.Domain.Common;
using TemplateS.Infra.Data.Context;

namespace TemplateS.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region 'Properties'

        protected readonly ContextCore _context;

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        #endregion

        public Repository(ContextCore context) => _context = context;

        #region 'Methods: Create/Update/Remove/Save'

        public TEntity Create(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                Save();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TEntity> Create(List<TEntity> models)
        {
            try
            {
                DbSet.AddRange(models);
                Save();
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(TEntity model)
        {
            try
            {
                var entry = NewMethod(model);
                DbSet.Attach(model);
                entry.State = EntityState.Modified;

                return Save() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private EntityEntry<TEntity> NewMethod(TEntity model) => _context.Entry(model);

        public bool Update(List<TEntity> models)
        {
            try
            {
                foreach (var register in models)
                {
                    var entry = _context.Entry(register);
                    DbSet.Attach(register);
                    entry.State = EntityState.Modified;
                }

                return Save() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(TEntity model)
        {
            try
            {
                var entry = _context.Entry(model);
                DbSet.Attach(model);
                entry.State = EntityState.Deleted;

                return Save() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(params object[] Keys)
        {
            try
            {
                var model = DbSet.Find(Keys);
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var model = DbSet.Where(where).FirstOrDefault();

                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 'Methods: Search'

        public TEntity Find(params object[] Keys)
        {
            try
            {
                return DbSet.Find(Keys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.AsNoTracking().FirstOrDefault(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;

                if (includes != null)
                    query = (IQueryable<TEntity>)includes(query);

                return query.SingleOrDefault(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.Where(where);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;

                if (includes != null)
                    query = (IQueryable<TEntity>)includes(query);

                return query.Where(predicate).AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 'Assyncronous Methods'

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                var query = DbSet.AsNoTracking();

                if (includes != null)
                    query = (IQueryable<TEntity>)includes(query);

                return await query.SingleOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> CreateAsync(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                await SaveAsync();
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            try
            {
                (model as BaseEntity).UpdatedDate = DateTime.Now;
                var entry = _context.Entry(model);
                DbSet.Attach(model);
                entry.State = EntityState.Modified;

                return await SaveAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            try
            {
                var entry = _context.Entry(model);
                DbSet.Attach(model);
                entry.State = EntityState.Deleted;
                return await SaveAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(params object[] Keys)
        {
            try
            {
                var model = DbSet.Find(Keys);
                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var model = DbSet.FirstOrDefault(where);

                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 'Search Methods Async'

        public async Task<TEntity> GetAsync(params object[] Keys)
        {
            try
            {
                return await DbSet.FindAsync(Keys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, object>? includes = null)
        {
            try
            {
                var query = DbSet.AsNoTracking();

                if (includes != null)
                    query = (IQueryable<TEntity>)includes(query);

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        public void Dispose()
        {
            try
            {
                if (_context != null)
                    _context.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

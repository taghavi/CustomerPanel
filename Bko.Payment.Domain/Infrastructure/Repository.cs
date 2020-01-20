using Bko.Payment.Domain;
using Bko.Payment.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bko.Payment.Domain.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private DbSet<TEntity> dbSet;
        string errorMessage = string.Empty;

        public Repository(DbContext Context)
        {
            this.context = Context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetById(object id)
        {
            return this.dbSet.Find(id);
        }
        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
              return  this.dbSet.Add(entity);
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }
        }

        //public virtual void Delete(object id)
        //{
        //    TEntity entityToDelete = dbSet.Find(id);
        //    Delete(entityToDelete);
        //}

        //public virtual void Delete(TEntity entityToDelete)
        //{
        //    if (context.Entry(entityToDelete).State == EntityState.Detached)
        //    {
        //        dbSet.Attach(entityToDelete);
        //    }
        //    dbSet.Remove(entityToDelete);
        //}

        public virtual void Edit(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                //var msg = string.Empty;
                //foreach (var validationErrors in dbEx.EntityValidationErrors)
                //{
                //    foreach (var validationError in validationErrors.ValidationErrors)
                //    {
                //        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                //        validationError.PropertyName, validationError.ErrorMessage);
                //    }
                //}
                //var fail = new Exception(msg, dbEx);
                //throw fail;
            }
        }

        public virtual TEntity FindBy(Expression<Func<TEntity, bool>> predicate)
        {
           return  dbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetIncludes(params Expression<Func<TEntity, Object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet.Include(includes[0]);
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }
            return query.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bko.Payment.Domain.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
        TEntity GetById(object Id);
        TEntity FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        //void Delete(object Id);
        //void Delete(TEntity entity);
        void Edit(TEntity entity);        
    }
}

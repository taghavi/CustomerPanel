using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Repository
{


    public class TxnRepository<TObject> : IRepository<TObject>
       where TObject : class
    {
        private bool shareContext = false;

        protected IUnitOfWork UoW;

        public TxnRepository(IUnitOfWork uow)
        {
            UoW = uow;
        }

        protected IDbSet<TObject> DbSet
        {
            get
            {
                return UoW.Set<TObject>();
            }
        }

        public void Dispose()
        {
            if (shareContext && (UoW != null))
                UoW.Dispose();
        }

        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable();
        }


        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TObject>();
        }

        //public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> filter, Expression<Func<TObject, long>> sort, int pageNumber, out int total, int size = 0, SortOrder sortOrder = SortOrder.Ascending,  string[] navigationProperties=null)
        //{
        //    int skipCount = (pageNumber - 1) * size;
        //    var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();

        //    total = _resetSet.Count();
        //    if (sortOrder == SortOrder.Descending)
        //    {
        //        _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.OrderByDescending(sort).Skip(skipCount).Take(size);

        //    }
        //    else
        //    {
        //        _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.OrderBy(sort).Skip(skipCount).Take(size);
        //    }

        //    if (navigationProperties != null)
        //        foreach (string navigationProperty in navigationProperties)
        //            _resetSet = _resetSet.Include(navigationProperty);
        //    return _resetSet.AsQueryable();
        //}
        public virtual IQueryable<TObject> GetWithPagingAndSortFunc(
     Expression<Func<TObject, bool>> filter,
     Func<IQueryable<TObject>,
         IOrderedQueryable<TObject>> orderBy,
     List<Expression<Func<TObject, object>>>
         includeProperties,
     int page,
     int pageSize, out int total, bool getAll = false, params string[] navigationProperties)
        {
            IQueryable<TObject> query = DbSet;

            total = filter != null ? DbSet.Where(filter).Count() : DbSet.Count();
            if (getAll)
                pageSize = total;
            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
            {
                //برای پارامتریک کردن کویری سمت اس کیو ال به اسکیپ و تیک باید اکسپرژن داد تا پلن اگزکیوت کش شود
                int resultsToSkip = (page - 1) * pageSize;
                query = query
                    .Skip(() => resultsToSkip)
                    .Take(() => pageSize);
            }

            return query;
        }
        public virtual int Count(Expression<Func<TObject, bool>> filter)
        {

            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();

            return _resetSet.Count();
        }

        public bool Any(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject FindWithIncludeParam(Expression<Func<TObject, bool>> predicate, params string[] navigationProperties)
        {
            var query = DbSet.AsQueryable();
            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);
            return query.FirstOrDefault(predicate);
        }
        public virtual IQueryable<TObject> GetWithIncludeParam(Expression<Func<TObject, bool>> predicate, params string[] navigationProperties)
        {
            var query = DbSet.AsQueryable();
            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            return predicate == null ? query : query.Where(predicate);
        }

        public void AddToContextWithoutSaving(TObject TObject)
        {
            DbSet.AddOrUpdate(TObject);


        }

        public virtual IList<ValidationResult> Save(TObject TObject, Func<IDictionary<object, object>, IList<ValidationResult>> func = null)
        {

            DbSet.AddOrUpdate(TObject);

            if (!shareContext)
                return UoW.SaveChanges(func);
            return null;
        }

        public virtual IList<ValidationResult> SaveList(IEnumerable<TObject> TObject, Func<IDictionary<object, object>, IList<ValidationResult>> func = null)
        {
            try
            {
                foreach (var item in TObject)
                {
                    DbSet.Add(item);
                }

                if (!shareContext)
                    return UoW.SaveChanges(func);
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public virtual IList<ValidationResult> UpdateList(IEnumerable<TObject> TObject, Func<IDictionary<object, object>, IList<ValidationResult>> func = null)
        {
            foreach (var item in TObject)
            {
                var entry = UoW.Set<TObject>();
                DbSet.Attach(item);
                UoW.Db.Entry(item).State = EntityState.Modified;
            }
            //UoW.MarkAsChanged(entry);
            if (!shareContext)
                return UoW.SaveChanges(func);
            return null;
        }


        public virtual IList<ValidationResult> Update(TObject TObject, Func<IDictionary<object, object>, IList<ValidationResult>> func = null)
        {
            var entry = UoW.Set<TObject>();
            DbSet.Attach(TObject);
            UoW.Db.Entry(TObject).State = EntityState.Modified;
            //UoW.MarkAsChanged(entry);
            if (!shareContext)
                return UoW.SaveChanges(func);
            return null;
        }
        public virtual IList<ValidationResult> Insert(TObject TObject, Func<IDictionary<object, object>, IList<ValidationResult>> func = null)
        {
            var entry = UoW.Set<TObject>();
            DbSet.Attach(TObject);
            UoW.Db.Entry(TObject).State = EntityState.Added;
            //UoW.MarkAsChanged(entry);
            if (!shareContext)
                return UoW.SaveChanges(func);
            return null;
        }
        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);
            if (!shareContext)
                return UoW.SaveChanges();
            return 0;
        }




        public bool SpSqlQuery(string query, object[] parameters)
        {
            return UoW.SpSqlQuery(query, parameters);
        }


    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Bko.DataAccess;
using Framework.Repository;

namespace Business
{
  
    public class TxnBlo<T>
      where T : class
    {
        private readonly TxnRepository<T> _repository;
        private readonly MSrvTxnEntities _uow;

        public TxnBlo(TxnRepository<T> repository, MSrvTxnEntities uow)
        {
            _repository = repository;
            _uow = uow;

        }


        public IQueryable<T> GetAll()
        {
            return _repository.All();
        }

        

        public bool SpSqlQuery(string query, object[] parameters)
        {
            return _repository.SpSqlQuery(query, parameters);
        }


      
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            _repository.Delete(predicate);
        }

       

        public IList<ValidationResult> Add(T domainObject, Func<IDictionary<object, object>, IList<ValidationResult>> action = null)
        {
            return _repository.Save(domainObject, action);
        }

        public T SingleOrDefaultWithIncludeParam(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {

            return _repository.FindWithIncludeParam(predicate, navigationProperties);

        }

        public IQueryable<T> GetWithIncludeParam(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {


            return _repository.GetWithIncludeParam(predicate, navigationProperties);

        }
        public virtual T AddGetObject(T entity, out IList<ValidationResult> validationResult, Func<IDictionary<object, object>, IList<ValidationResult>> action = null)
        {
            validationResult = _repository.Save(entity, action);
            return entity;
        }

        public virtual void AddToContextWithoutSaving(T entity)
        {
            _repository.AddToContextWithoutSaving(entity);
        }


        public void UpdateDomainObject(T dto, out IList<ValidationResult> validationResult, Func<IDictionary<object, object>, IList<ValidationResult>> action = null)
        {
            validationResult = _repository.Update(dto, action);
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _repository.Count(predicate);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        //public IQueryable<T> GetWithPaging(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> sort, int pageIndex, int pageSize, out int totalItem, SortOrder sortOrder = SortOrder.Ascending)
        //{
        //    return _repository.Filter(predicate, sort, pageIndex, out totalItem, pageSize, sortOrder);
        //}

        public IQueryable<T> GetWithPaging(
   Expression<Func<T, bool>> filter,
   Func<IQueryable<T>,
       IOrderedQueryable<T>> orderBy,
   List<Expression<Func<T, object>>>
       includeProperties,
   int page,
   int pageSize, out int totalItem,bool getAll=false)
        {
            return _repository.GetWithPagingAndSortFunc(filter, orderBy, includeProperties, page, pageSize, out totalItem,getAll);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            var result = _repository.Any(predicate);
            return result;
        }
    }
}

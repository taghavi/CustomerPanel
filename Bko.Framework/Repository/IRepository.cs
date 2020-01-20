using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Linq.Expressions;

namespace Framework.Repository
{
    //
    // Summary:
    //     Specifies how rows of data are sorted.
    public enum SortOrder
    {
        //
        // Summary:
        //     The default. No sort order is specified.
        Unspecified = -1,
        //
        // Summary:
        //     Rows are sorted in ascending order.
        Ascending = 0,
        //
        // Summary:
        //     Rows are sorted in descending order.
        Descending = 1
    }
    /// <summary>
    /// repository interface based on repository and unit of work pattern that works dirrectly with context and any other classes for working with context should use this interface
    /// </summary>
    /// <typeparam name="T">generic type for all kind of entities</typeparam>
    public interface IRepository<T> : IDisposable
    // where T : class
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns>all record of T class</returns>
        IQueryable<T> All();

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        /// <returns>all records that satisfy the related predicate</returns>
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

       
        /// <summary>
        /// Filters the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="size">The size.</param>
        /// <param name="total">The total.</param>
        /// <param name="index">The index.</param>
        /// <returns>query of selecting all data after filtering based on the predicate</returns>
        //IQueryable<T> Filter(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> sort, int size, out int total, int index = 0, SortOrder sortOrder = SortOrder.Ascending, string[] navigationProperties = null);

      
        /// <summary>
        /// Anies the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>if any record of T object with the specified predicate exist</returns>
        bool Any(Expression<Func<T, bool>> predicate);


      

              bool SpSqlQuery(string query, object[] parameters);

     
        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        /// <returns>find T object based on specified keys</returns>
        T Find(params object[] keys);

        /// <summary>
        /// Finds the with include parameter.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="navigationProperties">The navigation properties.</param>
        /// <returns>find T object based on specified keys  and navigation properties</returns>
        T FindWithIncludeParam(Expression<Func<T, bool>> predicate, params string[] navigationProperties);

        IQueryable<T> GetWithIncludeParam(Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>find T object based on predicate</returns>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        IList<ValidationResult> Save(T t, Func<IDictionary<object, object>, IList<ValidationResult>> func = null);


        /// <summary>
        /// Saves the changes with validation.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <param name="saveChangesWithValidation">The save changes with validation.</param>
        /// <returns></returns>
        //bool SaveChangesWithValidation(Func<IDictionary<object, object>, IList<ValidationResult>> func,
        //    out IList<ValidationResult> saveChangesWithValidation);

        /// <summary>
        /// Adds to context without saving.
        /// </summary>
        /// <param name="TObject">The t object.</param>
        void AddToContextWithoutSaving(T TObject);

        /// <summary>
        /// Create a List of objects into database.
        /// </summary>
        /// <param name="TObject">Specified a new list of object to create.</param>
        /// <param name="func">business validation function</param>
        /// <returns>
        /// list of validation result
        /// </returns>
        IList<ValidationResult> SaveList(IEnumerable<T> TObject, Func<IDictionary<object, object>, IList<ValidationResult>> func = null);

        /// <summary>
        /// Updates the list.
        /// </summary>
        /// <param name="objectList">The object list.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        IList<ValidationResult> UpdateList(IEnumerable<T> objectList, Func<IDictionary<object, object>, IList<ValidationResult>> func = null);
        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>
        /// <returns></returns>
        //int Delete(T t);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Deletes from context without saving.
        /// </summary>
        /// <param name="TObject">The t object.</param>
        //void DeleteFromContextWithoutSaving(T TObject);
        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        IList<ValidationResult> Update(T t, Func<IDictionary<object, object>, IList<ValidationResult>> func = null);
        IList<ValidationResult> Insert(T t, Func<IDictionary<object, object>, IList<ValidationResult>> func = null);
        int Count(Expression<Func<T, bool>> filter);

        IQueryable<T> GetWithPagingAndSortFunc(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy,
            List<Expression<Func<T, object>>>
                includeProperties,
            int page,
            int pageSize, out int total,bool GetAll,params string[] navigationProperties);
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Repository
{
    /// <summary>
    /// interface of specification class use specification pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Specification<T> : ISpecification<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Specification{T}"/> class.
        /// </summary>
        public Specification()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Specification{T}"/> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        private Specification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        /// <summary>
        /// Gets or sets the predicate.
        /// </summary>
        /// <value>
        /// The predicate.
        /// </value>
        public Expression<Func<T, bool>> Predicate { get; protected set; }
        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public Func<IQueryable<T>, IOrderedQueryable<T>> Sort { get; protected set; }
        /// <summary>
        /// Gets or sets the post process.
        /// </summary>
        /// <value>
        /// The post process.
        /// </value>
        public Func<IQueryable<T>, IQueryable<T>> PostProcess { get; protected set; }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public Specification<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> property)
        {
            var newSpecification = new Specification<T>(Predicate) { PostProcess = PostProcess };
            if (Sort != null)
            {
                newSpecification.Sort = items => Sort(items).ThenBy(property);
            }
            else
            {
                newSpecification.Sort = items => items.OrderBy(property);
            }
            return newSpecification;
        }

        /// <summary>
        /// Takes the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public Specification<T> Take(int amount)
        {
            var newSpecification = new Specification<T>(Predicate) { Sort = Sort };
            if (PostProcess != null)
            {
                newSpecification.PostProcess = items => PostProcess(items).Take(amount);
            }
            else
            {
                newSpecification.PostProcess = items => items.Take(amount);
            }
            return newSpecification;
        }

        /// <summary>
        /// Skips the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public Specification<T> Skip(int amount)
        {
            var newSpecification = new Specification<T>(Predicate) { Sort = Sort };
            if (PostProcess != null)
            {
                newSpecification.PostProcess = items => PostProcess(items).Skip(amount);
            }
            else
            {
                newSpecification.PostProcess = items => items.Skip(amount);
            }
            return newSpecification;
        }


        /// <summary>
        /// Prepares the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private IQueryable<T> Prepare(IQueryable<T> query)
        {
            var filtered = query.Where(Predicate);
            var sorted = Sort(filtered);
            var postProcessed = PostProcess(sorted);
            return postProcessed;
        }

        /// <summary>
        /// Satisfyings the item from.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public T SatisfyingItemFrom(IQueryable<T> query)
        {
            return Prepare(query).SingleOrDefault();
        }

        /// <summary>
        /// Satisfyings the items from.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IQueryable<T> SatisfyingItemsFrom(IQueryable<T> query)
        {
            return Prepare(query);
        }


    }
}
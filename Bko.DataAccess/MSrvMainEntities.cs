using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Framework.Logger;
using Framework.Repository;

namespace Bko.DataAccess
{
    public partial class MsrvMainEntities 
    {

        protected static BkoLog logger;


        public void CreateLoggerInstance()
        {
            if (logger == null)
                logger = new BkoLog();
        }
        public IList<TEntity> SqlQuery<TEntity>(string query, object[] parameters) where TEntity : class
        {
            try
            {
                if (parameters == null)
                    return this.Database.SqlQuery<TEntity>(query).ToList();
                return this.Database.SqlQuery<TEntity>(query, parameters).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SpSqlQuery(string query, object[] parameters)
        {
            try
            {
                if (parameters == null)
                {
                    var result = this.Database.ExecuteSqlCommand(query);
                    return true;
                }
                var result2 = this.Database.ExecuteSqlCommand(query, parameters);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets or sets the validation results.
        /// </summary>
        /// <value>
        /// The validation results.
        /// </value>
        IList<ValidationResult> ValidationResults { get; set; }
        /// <summary>
        /// The business function validation
        /// </summary>
        private Func<IDictionary<object, object>, IList<ValidationResult>> BusinessFunctionValidation;
    





        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns> set of entity</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();

        }

        /// <summary>
        /// Saves the changes with validation.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        public IList<ValidationResult> SaveChanges(Func<IDictionary<object, object>, IList<ValidationResult>> func = null)
        {

            BusinessFunctionValidation = func;
            IList<ValidationResult> result = new List<ValidationResult>();
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                string msg = string.Format("validation throw an exception \n method:SaveChangesWithValidation \t");

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        if (!result.Any(p => p.MemberNames.FirstOrDefault() == validationError.PropertyName))
                        {
                            result.Add(new ValidationResult(validationError.ErrorMessage, new[] { validationError.PropertyName }));
                            msg += string.Format("property:{0} , errorMsg:{1}\n", validationError.PropertyName, validationError.ErrorMessage);
                        }

                    }
                    CreateLoggerInstance();
                    logger.Logger.Warn(msg);
                }
            }
            return result;

        }


        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public DbContext Db { get { return this; } }


      
        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>list of domain object</returns>
        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        /// <summary>
        /// Extension point allowing the user to customize validation of an entity or filter out validation results.
        /// Called by <see cref="M:System.Data.Entity.DbContext.GetValidationErrors" />.
        /// </summary>
        /// <param name="entityEntry">DbEntityEntry instance to be validated.</param>
        /// <param name="items">User-defined dictionary containing additional info for custom validation. It will be passed to
        /// <see cref="T:System.ComponentModel.DataAnnotations.ValidationContext" />
        /// and will be exposed as
        /// <see cref="P:System.ComponentModel.DataAnnotations.ValidationContext.Items" />
        /// . This parameter is optional and can be null.</param>
        /// <returns>
        /// Entity validation result. Possibly null when overridden.
        /// </returns>
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            items.Add("Context", this);
            //if (ValidationResults != null)
            //items.Add("BusinessValidation", ValidationResults);
            var result = base.ValidateEntity(entityEntry, items);
            string msg = "ValidateEntity function is calling:\t";
            if (result.ValidationErrors.Count == 0 && BusinessFunctionValidation != null)
            {
                ValidationResults = BusinessFunctionValidation(items);
                if (ValidationResults != null)
                    foreach (var item in ValidationResults)
                    {
                        result.ValidationErrors.Add(new DbValidationError(item.MemberNames.FirstOrDefault(), item.ErrorMessage));

                        msg += string.Format("property:{0} errorMsg:{1}\t", item.MemberNames.FirstOrDefault(), item.ErrorMessage);
                    }

            }

            CreateLoggerInstance();
            logger.Logger.Warn(msg);
            return result;
        }

        /// <summary>
        /// Saves the changes with validation.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns>list of validation result</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<ValidationResult> SaveChangesWithValidation(IList<ValidationResult> validationResult)
        {
            throw new NotImplementedException();

        }


        /// <summary>
        /// Gets the state of the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public EntityState GetEntityState(object entity)
        {

            if (entity == null)
                throw new Exception("Entity Is null. Cannot Get State of it.");
            return this.Entry(entity).State;


        }

        public string UserName { get { return "testUser"; } }

    }
}
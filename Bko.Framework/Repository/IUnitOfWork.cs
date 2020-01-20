using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Framework.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        //void MarkAsChanged(object entity);// where TEntity : class;
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        IList<ValidationResult> SaveChanges(Func<IDictionary<object, object>, IList<ValidationResult>> func = null);
        DbContext Db { get; }
        bool SpSqlQuery(string query, object[] parameters);
        IList ExecuteDatabaseFunction(Dictionary<string, string> parameters, string functionName);
        IList ExecuteDatabaseFunction2(SqlParameter[] parameters, string functionName, SqlParameter outParam, out int totalRow);
        IList<TEntity> SqlQuery<TEntity>(string query, object[] parameters) where TEntity : class;
    }
}
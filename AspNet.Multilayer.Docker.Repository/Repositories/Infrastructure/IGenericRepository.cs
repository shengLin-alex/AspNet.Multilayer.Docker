using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AspNet.Multilayer.Docker.Helper;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContext"><see cref="DbContext"/>DbContext type</typeparam>
    public interface IGenericRepository<TEntity, TDbContext> : IDisposable
        where TEntity : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// Insert one data
        /// </summary>
        /// <param name="entity">the data that inserting</param>
        /// <returns>the data that inserted</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Update one data
        /// </summary>
        /// <param name="entity">the data that updating</param>
        /// <returns>success or not</returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Delete one data
        /// </summary>
        /// <param name="entity">the data that deleting</param>
        /// <returns>success or not</returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// 取得符合條件的第一筆資料
        /// </summary>
        /// <param name="predicate">條件</param>
        /// <returns>符合條件的第一筆資料</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取得符合條件且包含指定內容的第一筆資料
        /// </summary>
        /// <param name="predicate">條件</param>
        /// <param name="includes">指定內容</param>
        /// <returns>符合條件且包含指定內容的第一筆資料</returns>
        TEntity GetInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 取得包含指定內容的所有資料
        /// </summary>
        /// <param name="includes">指定內容</param>
        /// <returns>包含指定內容的所有資料</returns>
        List<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 取得整個資料集合
        /// </summary>
        /// <returns>完整資料集</returns>
        [MethodInterceptor]
        List<TEntity> GetAll();
    }
}

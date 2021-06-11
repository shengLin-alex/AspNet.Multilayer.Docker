using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AspNet.Multilayer.Docker.Repository
{
    /// <summary>
    /// 泛型資料儲存庫
    /// </summary>
    /// <typeparam name="TEntity">實體資料型別</typeparam>
    /// <typeparam name="TDbContext"><see cref="DbContext"/> 實際型別</typeparam>
    public class GenericRepository<TEntity, TDbContext> : IGenericRepository<TEntity, TDbContext>
        where TEntity : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool IsDisposed;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="dbContext"><see cref="DbContext"/> 實際型別物件</param>
        protected GenericRepository(TDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GenericRepository{TEntity,TDbContext}"/> class.
        /// </summary>
        ~GenericRepository()
        {
            this.Dispose(false);
        }

        protected TDbContext DbContext { get; }

        /// <summary>
        /// 新增一筆資料
        /// </summary>
        /// <param name="entity">欲新增之實體資料</param>
        /// <returns>新增成功的實體資料</returns>
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            EntityEntry<TEntity> entityCreated = this.DbContext.Set<TEntity>().Add(entity);
            this.DbContext.SaveChanges();

            return entityCreated.Entity;
        }

        /// <summary>
        /// 更新一筆資料
        /// </summary>
        /// <param name="entity">欲更新的實體資料</param>
        /// <returns>是否更新成功</returns>
        public bool Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbContext.Entry(entity).State = EntityState.Modified;

            return this.DbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 刪除一筆資料
        /// </summary>
        /// <param name="entity">欲刪除的實體資料</param>
        /// <returns>是否刪除成功</returns>
        public bool Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.DbContext.Entry(entity).State = EntityState.Deleted;

            return this.DbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 取得符合條件的第一筆資料
        /// </summary>
        /// <param name="predicate">條件</param>
        /// <returns>符合條件的第一筆資料</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 取得符合條件且包含指定內容的第一筆資料
        /// </summary>
        /// <param name="predicate">條件</param>
        /// <param name="includes">指定內容</param>
        /// <returns>符合條件且包含指定內容的第一筆資料</returns>
        public TEntity GetInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.DbContext.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }

            return query.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 取得包含指定內容的所有資料
        /// </summary>
        /// <param name="includes">指定內容</param>
        /// <returns>包含指定內容的所有資料</returns>
        public List<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.DbContext.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }

            return query.ToList();
        }

        /// <summary>
        /// 取得整個資料集合
        /// </summary>
        /// <returns>完整資料集</returns>
        public List<TEntity> GetAll()
        {
            return this.DbContext.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method. Therefore, you should call
            // GC.SupressFinalize to take this object off the finalization queue and prevent
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, dispose all managed and unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (this.IsDisposed)
            {
                return;
            }

            // If disposing equals true, dispose all managed and unmanaged resources.
            if (disposing)
            {
                // Dispose managed resources.
                this.DbContext.Dispose();
            }

            // Note disposing has been done.
            this.IsDisposed = true;
        }
    }
}

using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hectre.BackEnd.Data
{
    /// <summary>
    /// interface for an entity model repository
    /// </summary>
    /// <typeparam name="TEntity">entity model</typeparam>
    /// <typeparam name="TDbContext">database context which contains the entity model</typeparam>
    public interface IEfRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        /// <summary>
        /// Get item by it's identity
        /// </summary>
        /// <param name="id">id in string</param>
        /// <returns>specific item</returns>
        TEntity GetById(string id);

        /// <summary>
        /// List all items
        /// </summary>
        /// <returns>list all entity items</returns>
        IReadOnlyList<TEntity> ListAll();

        /// <summary>
        /// List all items which satisfy the specific data specification
        /// </summary>
        /// <param name="spec">data specification represent for set of query condition</param>
        /// <returns>list if entity items</returns>
        IReadOnlyList<TEntity> List(ISpecification<TEntity> spec);

        /// <summary>
        /// add new item to the database
        /// </summary>
        /// <param name="entity">entity item</param>
        /// <returns>added item</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// update an entity item to the database
        /// </summary>
        /// <param name="entity">updated item</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete an item from database
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Count the number of items which satisfy the specific data specification
        /// </summary>
        /// <param name="spec">data specification represent for set of query condition</param>
        /// <returns>the number of items</returns>
        int Count(ISpecification<TEntity> spec);
    }
}

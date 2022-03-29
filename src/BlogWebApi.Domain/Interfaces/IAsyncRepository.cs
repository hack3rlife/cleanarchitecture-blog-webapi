using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        /// <summary>
        /// Get a single item based on the parameter <para>guid</para>
        /// </summary>
        /// <param name="guid">The guid to be retrieved</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid guid);

        /// <summary>
        /// Get all the elements
        /// </summary>
        /// <param name="skip">Bypasses a specified number of elements in a sequence and then returns the remaining elements.</param>
        /// <param name="take">Returns a specified number of contiguous elements from the start of a sequence.</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> ListAllAsync(int skip, int take);

        /// <summary>
        /// Add an item to the data store
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Update an item in the data store
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Delete an item from the data store
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
    }
}

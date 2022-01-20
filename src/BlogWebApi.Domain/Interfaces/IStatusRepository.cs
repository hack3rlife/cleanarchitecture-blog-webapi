using System.Threading.Tasks;

namespace BlogWebApi.Domain.Interfaces
{
    public interface IStatusRepository
    {
        /// <summary>
        /// Gets the service status
        /// </summary>
        /// <returns>The Service <see cref="Status"/></returns>
        Task<Status> GetStatusAsync();

        /// <summary>
        /// Insert Service Status when service starts
        /// </summary>
        /// <returns></returns>
        Task UpsertStatusAsync();
    }
}

using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain.Interfaces;
using System.Threading.Tasks;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<StatusResponse> GetStatusAsync()
        {
            var status = await _statusRepository.GetStatusAsync();

            return StatusResponse(status);
        }

        private static StatusResponse StatusResponse(Status status)
        {
            return new StatusResponse
            {
                AssemblyVersion = status.AssemblyVersion,
                OsVersion = status.OsVersion,
                Server = status.Server,
                Started = status.Started,
            };
        }
    }
}

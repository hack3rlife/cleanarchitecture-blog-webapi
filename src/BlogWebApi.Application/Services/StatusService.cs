using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain.Interfaces;
using System.Threading.Tasks;
using BlogWebApi.Domain;
using System;

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

        public async Task SetStatusAsync()
        {
            await _statusRepository.UpsertStatusAsync();
        }

        private static StatusResponse StatusResponse(Status status)
        {
            DateTime.TryParse(status.Started, out DateTime result);

            var elapsedTime = DateTime.UtcNow - result;
            return new StatusResponse
            {
                AssemblyVersion = status.AssemblyVersion,
                OsVersion = status.OsVersion,
                Server = status.Server,
                Started = status.Started,
                ProcessorCount = status.ProcessorCount,
                ElapsedTime = elapsedTime.ToString()
            };
        }
    }
}

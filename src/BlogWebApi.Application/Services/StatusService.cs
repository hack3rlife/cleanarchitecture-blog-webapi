using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain.Interfaces;
using System.Threading.Tasks;
using BlogWebApi.Domain;
using System;
using AutoMapper;

namespace BlogWebApi.Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<StatusResponse> GetStatusAsync()
        {
            var status = await _statusRepository.GetStatusAsync();

            DateTime.TryParse(status.Started, out DateTime result);

            //TODO: Check how to set elapsedtime when calling _mapper.Map<StatusResponse>(status);
            var statusResponse =  _mapper.Map<StatusResponse>(status);

            var elapsedTime = DateTime.UtcNow - result;
            statusResponse.ElapsedTime = elapsedTime.ToString();

            return statusResponse;
        }       

        public async Task SetStatusAsync()
        {
            await _statusRepository.UpsertStatusAsync();
        }

       
    }
}

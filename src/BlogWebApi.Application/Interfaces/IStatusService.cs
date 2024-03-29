﻿using BlogWebApi.Application.Dto;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Interfaces
{
    public interface IStatusService
    {
        /// <summary>
        /// Get the status of the service
        /// </summary>
        /// <returns>The <see cref="StatusResponse"/></returns>
        Task<StatusResponse> GetStatusAsync();

        /// <summary>
        /// Inserts the Status when the Service Starts.
        /// </summary>
        /// <remarks>This is used internally.</remarks>
        /// <returns></returns>
        Task SetStatusAsync();
    }
}

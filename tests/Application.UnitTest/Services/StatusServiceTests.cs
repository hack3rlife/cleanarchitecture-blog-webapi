using System;
using BlogWebApi.Application.Interfaces;
using Moq;
using System.Threading.Tasks;
using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Services;
using BlogWebApi.Domain;
using BlogWebApi.Domain.Interfaces;
using Xunit;

namespace Application.UnitTest.Services
{
    public class StatusServiceTests
    {
        private readonly Mock<IStatusRepository> _mockStatusRepository;
        private readonly IStatusService _statusService;

        public StatusServiceTests()
        {
            _mockStatusRepository = new Mock<IStatusRepository>();
            _statusService = new StatusService(_mockStatusRepository.Object);
        }

        [Fact]
        public async Task StatusService_CheckStatus_Success()
        {
            // Arrange
            var status = new Status
            {
                AssemblyVersion = "TheAssemblyVersion",
                OsVersion = "OsVersion",
                Server = "TheServerName",
                Started = DateTime.UtcNow
            };

            _mockStatusRepository
                .Setup(x => x.GetStatusAsync())
                .ReturnsAsync(status)
                .Verifiable();

            // Act
            var statusResponse = await _statusService.GetStatusAsync();

            // Arrange
            Assert.NotNull(statusResponse);
            _mockStatusRepository.Verify(x => x.GetStatusAsync(), Times.Once());

        }
    }
}

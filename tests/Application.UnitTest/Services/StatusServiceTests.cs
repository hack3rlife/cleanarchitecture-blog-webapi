using AutoMapper;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Profiles;
using BlogWebApi.Application.Services;
using BlogWebApi.Domain;
using BlogWebApi.Domain.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services
{
    public class StatusServiceTests
    {
        private readonly Mock<IStatusRepository> _mockStatusRepository;
        private readonly IStatusService _statusService;

        public StatusServiceTests()
        {
            var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ProfileMapper>(); });

            var mapper = configurationProvider.CreateMapper();

            _mockStatusRepository = new Mock<IStatusRepository>();
            _statusService = new StatusService(_mockStatusRepository.Object, mapper);
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
                Started = DateTime.UtcNow.ToString("s"),
                ProcessorCount = 1
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

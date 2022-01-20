using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories
{
    [Collection("DatabaseCollectionFixture")]
    public class StatusRepositoryTests
    {
        private readonly StatusRepository _statusRepository;
        private readonly DatabaseFixture _fixture;

        public StatusRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _statusRepository = new StatusRepository(_fixture.BlogDbContext);
        }

        [Fact]
        public async Task StatusRepository_CheckStatus_Success()
        {
            // Arrange
            var started = DateTime.UtcNow.ToString("s");
            var server = "test-server";
            var osVersion = "test";
            var assemblyVersion = "1.0.0.0";

            await _fixture.BlogDbContext.Status.AddAsync(
                new Status
                {
                    Started = started,
                    Server = server,
                    OsVersion = osVersion,
                    AssemblyVersion = assemblyVersion
                });

            await _fixture.BlogDbContext.SaveChangesAsync();

            // Act

            var response = await _statusRepository.GetStatusAsync();

            // Assert
            Assert.Equal(started, response.Started);
            Assert.Equal(server, response.Server);
            Assert.Equal(osVersion, response.OsVersion);
            Assert.Equal(assemblyVersion, response.AssemblyVersion);
        }
    }
}

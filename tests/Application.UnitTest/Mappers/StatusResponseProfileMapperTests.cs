using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class StatusResponseProfileMapperTests : ProfileMapperTestBase
    {
        [Fact]
        public void PostUpdateRequestMapper_MapToPost_Success()
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

            // Act
            var statusResponse = _mapper.Map<StatusResponse>(status);

            // Assert
            Assert.NotNull(statusResponse);
            Assert.Equal(status.AssemblyVersion, statusResponse.AssemblyVersion);
            Assert.Equal(status.OsVersion, statusResponse.OsVersion);
            Assert.Equal(status.Server, statusResponse.Server);
            Assert.Equal(status.Started, statusResponse.Started);
            Assert.Equal(status.ProcessorCount, statusResponse.ProcessorCount);

            //TODO: Uncomment this when chicking how to set elapsedtime when calling _mapper.Map<StatusResponse>(status) is fixed.
            //Assert.Equal(status.ElapsedTime, statusResponse.ElapsedTime);
        }
    }
}

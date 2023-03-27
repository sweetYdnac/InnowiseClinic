using AutoFixture;
using Moq;
using Profiles.Business.Implementations.Services;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.Interfaces.Repositories;

namespace Profiles.API.Tests
{
    public class ProfilesServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProfilesRepository> _profileRepositoryMock;
        private readonly IProfilesService _profilesService;

        public ProfilesServiceTests()
        {
            _fixture = new Fixture();
            _profileRepositoryMock = new Mock<IProfilesRepository>();
            _profilesService = new ProfilesService(_profileRepositoryMock.Object);
        }

        [Fact]
        public async Task SetInactiveStatusToPersonalAsync_WithAnyOfficeId_CallsRepository()
        {
            // Arrange
            var officeId = _fixture.Create<Guid>();

            // Act
            await _profilesService.SetInactiveStatusToPersonalAsync(officeId);

            // Assert
            _profileRepositoryMock.Verify(x => x.SetInactiveStatusToPersonalAsync(officeId),
                Times.Once());
        }

        [Fact]
        public async Task UpdateOfficeAddressAsync_WithCorrectArguments_CallsRepository()
        {
            // Arrange
            var officeId = _fixture.Create<Guid>();
            var officeAddress = _fixture.Create<string>();

            // Act
            await _profilesService.UpdateOfficeAddressAsync(officeId, officeAddress);

            // Assert
            _profileRepositoryMock.Verify(x => x.UpdateOfficeAddressAsync(officeId, officeAddress),
                Times.Once);
        }
    }
}

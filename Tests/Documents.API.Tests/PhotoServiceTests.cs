using AutoFixture;
using Documents.Business.Implementations;
using Documents.Business.Interfaces;
using Documents.Data.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Shared.Exceptions;
using Shared.Models.Response.Documents;

namespace Documents.API.Tests
{
    public class PhotoServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IPhotosRepository> _photosRepositoryMock;
        private readonly IPhotoService _photoService;

        public PhotoServiceTests()
        {
            _fixture = new Fixture();
            _photosRepositoryMock = new Mock<IPhotosRepository>();
            _photoService = new PhotoService(_photosRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsBlobResponse()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var expectedResponse = _fixture.Create<BlobResponse>();

            _photosRepositoryMock.Setup(x => x.GetBlobAsync(id)).ReturnsAsync(expectedResponse);

            // Act
            var response = await _photoService.GetByIdAsync(id);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse).And.NotBeNull();
            _photosRepositoryMock.Verify(x => x.GetBlobAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _photosRepositoryMock.Setup(x => x.GetBlobAsync(id))
                .ThrowsAsync(new NotFoundException($"Blob with name = {id} doesn't exist."));

            // Act
            var act = async () => await _photoService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Blob with name = {id} doesn't exist.");

            _photosRepositoryMock.Verify(x => x.GetBlobAsync(id), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithValidFile_CallsRepository()
        {
            // Arrange
            var file = new Mock<IFormFile>();

            // Act
            await _photoService.CreateAsync(file.Object);

            // Assert
            _photosRepositoryMock.Verify(x => x.AddOrUpdateBlobAsync(
                It.IsAny<Guid>(), It.IsAny<Stream>(), file.Object.ContentType), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithAnyArguments_CallRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var file = new Mock<IFormFile>();

            // Act
            await _photoService.UpdateAsync(id, file.Object);

            // Assert
            _photosRepositoryMock.Verify(x => x.AddOrUpdateBlobAsync(
                id, It.IsAny<Stream>(), file.Object.ContentType), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithExistingId_CallsRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            // Act
            await _photoService.DeleteAsync(id);

            // Assert
            _photosRepositoryMock.Verify(x => x.DeleteBlobAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _photosRepositoryMock.Setup(x => x.DeleteBlobAsync(id))
                .ThrowsAsync(new NotFoundException($"Blob with name = {id} doesn't exist."));

            // Act
            var act = async () => await _photoService.DeleteAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Blob with name = {id} doesn't exist.");

            _photosRepositoryMock.Verify(x => x.DeleteBlobAsync(id), Times.Once);
        }
    }
}

using AutoFixture;
using Documents.Business.Implementations;
using Documents.Business.Interfaces;
using Documents.Data.Interfaces;
using FluentAssertions;
using Moq;
using Shared.Exceptions;
using Shared.Models;
using Shared.Models.Response.Documents;

namespace Documents.API.Tests
{
    public class AppointmentResultsServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentResultsRepository> _appointmentResultsRepositoryMock;
        private readonly IAppointmentResultsService _appointmentResultsService;

        public AppointmentResultsServiceTests()
        {
            _fixture = new Fixture();
            _appointmentResultsRepositoryMock = new Mock<IAppointmentResultsRepository>();
            _appointmentResultsService = new AppointmentsResultService(
                _appointmentResultsRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByNameAsync_WithExistingName_ReturnsBlobResponse()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var expectedResponse = _fixture.Create<BlobResponse>();

            _appointmentResultsRepositoryMock.Setup(x => x.GetBlobAsync(id)).ReturnsAsync(expectedResponse);

            // Act
            var response = await _appointmentResultsService.GetByNameAsync(id);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse).And.NotBeNull();
            _appointmentResultsRepositoryMock.Verify(x => x.GetBlobAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByNameAsync_WithNoExistingName_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _appointmentResultsRepositoryMock.Setup(x => x.GetBlobAsync(id))
                .ThrowsAsync(new NotFoundException($"Blob with name = {id} doesn't exist."));

            // Act
            var act = async () => await _appointmentResultsService.GetByNameAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Blob with name = {id} doesn't exist.");

            _appointmentResultsRepositoryMock.Verify(x => x.GetBlobAsync(id), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithAnyArguments_CallsRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var pdf = _fixture.Create<PdfResult>();

            // Act
            await _appointmentResultsService.CreateAsync(id, pdf);

            // Assert
            _appointmentResultsRepositoryMock.Verify(x => x.AddOrUpdateBlobAsync(
                id, It.IsAny<Stream>(), pdf.ContentType), Times.Once);
        }
    }
}

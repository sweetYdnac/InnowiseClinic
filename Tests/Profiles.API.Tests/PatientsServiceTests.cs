using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Profiles.Business.Implementations.Services;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs.Patient;
using Profiles.Data.Interfaces.Repositories;
using Shared.Exceptions;
using Shared.Messages;
using Shared.Models;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.API.Tests
{
    public class PatientsServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IPatientsRepository> _patientsRepositoryMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IPatientsService _patientsService;

        public PatientsServiceTests()
        {
            _fixture = new Fixture();
            _patientsRepositoryMock = new Mock<IPatientsRepository>();
            _messageServiceMock = new Mock<IMessageService>();
            _mapperMock = new Mock<IMapper>();
            _patientsService = new PatientsService(
                _patientsRepositoryMock.Object,
                _messageServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsPatientResponse()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var expectedResponse = _fixture.Build<PatientResponse>()
                .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _patientsRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedResponse);

            // Act
            var response = await _patientsService.GetByIdAsync(id);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse).And.NotBeNull();
            _patientsRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ReturnsPatientResponse()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            _patientsRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(null as PatientResponse);

            // Act
            var act = async () => await _patientsService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Patient's profile with id = {id} doesn't exist.");

            _patientsRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetPagedAndFilteredAsync_WithValidDto_ReturnsPagedResponse()
        {
            // Arrange
            var dto = _fixture.Create<GetPatientsDTO>();
            var pagedResult = _fixture.Build<PagedResult<PatientInformationResponse>>()
                .With(
                x => x.Items,
                _fixture.Build<PatientInformationResponse>()
                    .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                    .CreateMany())
                .Create();

            _patientsRepositoryMock.Setup(x => x.GetPatients(dto)).ReturnsAsync(pagedResult);

            // Act
            var response = await _patientsService.GetPagedAndFilteredAsync(dto);

            // Assert
            response.Items.Should().BeEquivalentTo(pagedResult.Items);
            response.PageSize.Should().Be(dto.PageSize);
            response.CurrentPage.Should().Be(dto.CurrentPage);
            response.TotalCount.Should().Be(pagedResult.TotalCount);

            _patientsRepositoryMock.Verify(x => x.GetPatients(dto), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithValidDto_CallsRepository()
        {
            // Arrange
            var dto = _fixture.Build<CreatePatientDTO>()
                .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            // Act
            await _patientsService.CreateAsync(dto);

            // Assert
            _patientsRepositoryMock.Verify(x => x.AddAsync(dto), Times.Once);
        }

        [Fact]
        public async Task GetMatchedPatientAsync_WithValidDto_CallsRepository()
        {
            // Arrange
            var dto = _fixture.Build<GetMatchedPatientDTO>()
                .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            // Act
            await _patientsService.GetMatchedPatientAsync(dto);

            // Assert
            _patientsRepositoryMock.Verify(x => x.GetMatchAsync(dto), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WithExistingId_CallsAllRepositoriesAndServices()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _patientsRepositoryMock.Setup(x => x.RemoveAsync(id)).ReturnsAsync(1);

            // Act
            await _patientsService.RemoveAsync(id);

            // Assert
            _patientsRepositoryMock.Verify(x => x.GetPhotoIdAsync(It.IsAny<Guid>()), Times.Once);
            _patientsRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Once);
            _messageServiceMock.Verify(x => x.SendDeletePhotoMessageAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _patientsRepositoryMock.Setup(x => x.RemoveAsync(id)).ReturnsAsync(0);

            // Act
            var act = async () => await _patientsService.RemoveAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Patient's profile with id = {id} doesn't exist.");

            _patientsRepositoryMock.Verify(x => x.GetPhotoIdAsync(It.IsAny<Guid>()), Times.Once);
            _patientsRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Once);
            _messageServiceMock.Verify(x => x.SendDeletePhotoMessageAsync(It.IsAny<Guid>()), Times.Never);

        }

        [Fact]
        public async Task UpdateAsync_WithExistingId_CallsAllRepositoriesAndServices()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Build<UpdatePatientDTO>()
                .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _patientsRepositoryMock.Setup(x => x.UpdateAsync(id, dto)).ReturnsAsync(1);

            // Act
            await _patientsService.UpdateAsync(id, dto);

            // Assert
            _patientsRepositoryMock.Verify(x => x.UpdateAsync(id, dto), Times.Once);
            _messageServiceMock.Verify(x => x.SendUpdatePatientMessageAsync(
                It.IsAny<UpdatePatientMessage>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithNoExistingId_CallsOnlyRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Build<UpdatePatientDTO>()
                .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _patientsRepositoryMock.Setup(x => x.UpdateAsync(id, dto)).ReturnsAsync(0);

            // Act
            await _patientsService.UpdateAsync(id, dto);

            // Assert
            _patientsRepositoryMock.Verify(x => x.UpdateAsync(id, dto), Times.Once);
            _messageServiceMock.Verify(x => x.SendUpdatePatientMessageAsync(
                It.IsAny<UpdatePatientMessage>()), Times.Never);
        }
    }
}

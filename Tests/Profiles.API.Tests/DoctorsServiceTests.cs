using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Profiles.Business.Implementations.Services;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Doctor;
using Profiles.Data.DTOs.DoctorSummary;
using Profiles.Data.Interfaces.Repositories;
using Shared.Core.Enums;
using Shared.Exceptions;
using Shared.Messages;
using Shared.Models;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.API.Tests
{
    public class DoctorsServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IDoctorsRepository> _doctorsRepositoryMock;
        private readonly Mock<IDoctorSummaryRepository> _doctorsSummaryRepositoryMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IDoctorsService _doctorsService;

        public DoctorsServiceTests()
        {
            _fixture = new Fixture();
            _doctorsRepositoryMock = new Mock<IDoctorsRepository>();
            _doctorsSummaryRepositoryMock = new Mock<IDoctorSummaryRepository>();
            _messageServiceMock = new Mock<IMessageService>();
            _mapperMock = new Mock<IMapper>();
            _doctorsService = new DoctorsService(
                _doctorsRepositoryMock.Object,
                _doctorsSummaryRepositoryMock.Object,
                _messageServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsDoctorResponse()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var expectedResponse = _fixture.Build<DoctorResponse>()
                .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _doctorsRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedResponse);

            // Act
            var response = await _doctorsService.GetByIdAsync(id);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse).And.NotBeNull();
            _doctorsRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _doctorsRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(null as DoctorResponse);

            // Act
            var act = async () => await _doctorsService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Doctor's profile with id = {id} doesn't exist.");

            _doctorsRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetPagedAndFilteredAsync_WithValidDto_ReturnsPagedResponse()
        {
            // Arrange
            var dto = _fixture.Create<GetDoctorsDTO>();
            var pagedResult = _fixture.Build<PagedResult<DoctorInformationResponse>>()
                .With(
                x => x.Items,
                _fixture.Build<DoctorInformationResponse>()
                    .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                    .CreateMany())
                .Create();

            _doctorsRepositoryMock.Setup(x => x.GetDoctors(dto)).ReturnsAsync(pagedResult);

            // Act
            var response = await _doctorsService.GetPagedAndFilteredAsync(dto);

            // Assert
            response.Items.Should().BeEquivalentTo(pagedResult.Items);
            response.PageSize.Should().Be(dto.PageSize);
            response.CurrentPage.Should().Be(dto.CurrentPage);
            response.TotalCount.Should().Be(pagedResult.TotalCount);

            _doctorsRepositoryMock.Verify(x => x.GetDoctors(dto), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithValidDto_CallsRepositories()
        {
            // Arrange
            var dto = _fixture.Create<CreateDoctorDTO>();
            var createDoctorSummaryDTO = _fixture.Create<CreateDoctorSummaryDTO>();

            _mapperMock.Setup(x => x.Map<CreateDoctorSummaryDTO>(It.IsAny<CreateDoctorDTO>()))
                .Returns(createDoctorSummaryDTO);

            // Act
            await _doctorsService.CreateAsync(dto);

            // Assert
            _doctorsRepositoryMock.Verify(x => x.AddAsync(dto), Times.Once);
            _doctorsSummaryRepositoryMock.Verify(x => x.AddAsync(createDoctorSummaryDTO), Times.Once);
            _messageServiceMock.Verify(x => x.SendCreateAccountEmailAsync(It.IsAny<SendCreateAccountEmailMessage>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithExistingId_CallsAllRepositoriesAndServices()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<UpdateDoctorDTO>();

            _doctorsRepositoryMock.Setup(x => x.UpdateAsync(id, dto)).ReturnsAsync(1);
            _mapperMock.Setup(x => x.Map<UpdateDoctorMessage>(dto))
                .Returns(_fixture.Create<UpdateDoctorMessage>());

            // Act
            await _doctorsService.UpdateAsync(id, dto);

            // Assert
            _doctorsRepositoryMock.Verify(x => x.UpdateAsync(id, dto), Times.Once);
            _doctorsSummaryRepositoryMock.Verify(x => x.UpdateAsync(id, It.IsAny<UpdateDoctorSummaryDTO>()), Times.Once);
            _messageServiceMock.Verify(x => x.SendUpdateDoctorMessageAsync(It.IsAny<UpdateDoctorMessage>()), Times.Once);
            _messageServiceMock.Verify(x => x.SendUpdateAccountStatusMessageAsync(
                It.IsAny<Guid>(),It.IsAny<AccountStatuses>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithNoExistingId_CallsOnlyDoctorRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<UpdateDoctorDTO>();

            _doctorsRepositoryMock.Setup(x => x.UpdateAsync(id, dto)).ReturnsAsync(0);

            // Act
            await _doctorsService.UpdateAsync(id, dto);

            // Assert
            _doctorsRepositoryMock.Verify(x => x.UpdateAsync(id, dto), Times.Once);
            _doctorsSummaryRepositoryMock.Verify(x => x.UpdateAsync(id, It.IsAny<UpdateDoctorSummaryDTO>()), Times.Never);
            _messageServiceMock.Verify(x => x.SendUpdateDoctorMessageAsync(It.IsAny<UpdateDoctorMessage>()), Times.Never);
            _messageServiceMock.Verify(x => x.SendUpdateAccountStatusMessageAsync(
                It.IsAny<Guid>(), It.IsAny<AccountStatuses>(), It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public async Task RemoveAsync_WithExistingId_CallsAllRepositoriesAndServices()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _doctorsRepositoryMock.Setup(x => x.RemoveAsync(id)).ReturnsAsync(1);

            // Act
            await _doctorsService.RemoveAsync(id);

            // Assert
            _doctorsRepositoryMock.Verify(x => x.GetPhotoIdAsync(It.IsAny<Guid>()), Times.Once);
            _doctorsRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Once);
            _messageServiceMock.Verify(x => x.SendDeletePhotoMessageAsync(It.IsAny<Guid>()), Times.Once);
            _doctorsSummaryRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _doctorsRepositoryMock.Setup(x => x.RemoveAsync(id)).ReturnsAsync(0);

            // Act
            var act = async () => await _doctorsService.RemoveAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Doctor's profile with id = {id} doesn't exist.");

            _doctorsRepositoryMock.Verify(x => x.GetPhotoIdAsync(It.IsAny<Guid>()), Times.Once);
            _doctorsRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Once);
            _messageServiceMock.Verify(x => x.SendDeletePhotoMessageAsync(It.IsAny<Guid>()), Times.Never);
            _doctorsSummaryRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Never);
        }

        [Fact]
        public async Task ChangeStatusAsync_WithExistingId_CallsAllRepositoriesAndServices()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<ChangeStatusDTO>();

            _doctorsSummaryRepositoryMock.Setup(x => x.ChangeStatus(id, dto.Status))
                .ReturnsAsync(1);

            // Act
            await _doctorsService.ChangeStatusAsync(id, dto);

            // Assert
            _doctorsSummaryRepositoryMock.Verify(x => x.ChangeStatus(id, dto.Status), Times.Once);
            _messageServiceMock.Verify(x => x.SendUpdateAccountStatusMessageAsync(
                It.IsAny<Guid>(), It.IsAny<AccountStatuses>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task ChangeStatusAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<ChangeStatusDTO>();

            _doctorsSummaryRepositoryMock.Setup(x => x.ChangeStatus(id, dto.Status))
                .ReturnsAsync(0);

            // Act
            var act = async () => await _doctorsService.ChangeStatusAsync(id, dto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Doctor's profile with id = {id} doesn't exist.");

            _doctorsSummaryRepositoryMock.Verify(x => x.ChangeStatus(id, dto.Status), Times.Once);
            _messageServiceMock.Verify(x => x.SendUpdateAccountStatusMessageAsync(
                It.IsAny<Guid>(), It.IsAny<AccountStatuses>(), It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public async Task SetInactiveStatusAsync_WithAnySpecializationId_CallsRepository()
        {
            // Arrange
            var specializationId = _fixture.Create<Guid>();

            // Act
            await _doctorsService.SetInactiveStatusAsync(specializationId);

            // Assert
            _doctorsRepositoryMock.Verify(x => x.SetInactiveStatusAsync(specializationId), Times.Once);
        }

        [Fact]
        public async Task UpdateSpecializationName_WithAnyArguments_CallsRepository()
        {
            // Arrange
            var specializationId = _fixture.Create<Guid>();
            var specializationName = _fixture.Create<string>();

            // Act
            await _doctorsService.UpdateSpecializationName(specializationId, specializationName);

            // Assert
            _doctorsRepositoryMock.Verify(
                x => x.UpdateSpecializationName(specializationId, specializationName), Times.Once);
        }
    }
}

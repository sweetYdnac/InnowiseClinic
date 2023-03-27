using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Profiles.Business.Implementations.Services;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Receptionist;
using Profiles.Data.DTOs.ReceptionistSummary;
using Profiles.Data.Interfaces.Repositories;
using Shared.Exceptions;
using Shared.Models;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.API.Tests
{
    public class ReceptionistsServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IReceptionistsRepository> _receptionistsRepositoryMock;
        private readonly Mock<IReceptionistSummaryRepository> _receptionistSummaryRepositoryMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IReceptionistsService _receptionistsService;

        public ReceptionistsServiceTests()
        {
            _fixture = new Fixture();
            _receptionistsRepositoryMock = new Mock<IReceptionistsRepository>();
            _receptionistSummaryRepositoryMock = new Mock<IReceptionistSummaryRepository>();
            _messageServiceMock = new Mock<IMessageService>();
            _mapperMock = new Mock<IMapper>();
            _receptionistsService = new ReceptionistsService(
                _receptionistsRepositoryMock.Object,
                _receptionistSummaryRepositoryMock.Object,
                _messageServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsReceptionistResponse()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var expectedResponse = _fixture.Create<ReceptionistResponse>();

            _receptionistsRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedResponse);

            // Act
            var response = await _receptionistsService.GetByIdAsync(id);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse).And.NotBeNull();
            _receptionistsRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _receptionistsRepositoryMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(null as ReceptionistResponse);

            // Act
            var act = async () => await _receptionistsService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Receptionist's profile with id = {id} doesn't exist.");

            _receptionistsRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);

        }

        [Fact]
        public async Task GetPagedAndFilteredAsync_WithValidDto_ReturnsPagedResponse()
        {
            // Arrange
            var dto = _fixture.Create<GetReceptionistsDTO>();
            var pagedResult = _fixture
                .Create<PagedResult<ReceptionistInformationResponse>>();

            _receptionistsRepositoryMock.Setup(x => x.GetPagedAsync(dto)).ReturnsAsync(pagedResult);

            // Act
            var response = await _receptionistsService.GetPagedAsync(dto);

            // Assert
            response.Items.Should().BeEquivalentTo(pagedResult.Items);
            response.PageSize.Should().Be(dto.PageSize);
            response.CurrentPage.Should().Be(dto.CurrentPage);
            response.TotalCount.Should().Be(pagedResult.TotalCount);

            _receptionistsRepositoryMock.Verify(x => x.GetPagedAsync(dto), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithValidDto_CallsRepositories()
        {
            // Arrange
            var dto = _fixture.Create<CreateReceptionistDTO>();

            // Act
            await _receptionistsService.CreateAsync(dto);

            // Assert
            _receptionistsRepositoryMock.Verify(x => x.AddAsync(dto), Times.Once);
            _receptionistSummaryRepositoryMock.Verify(
                x => x.AddAsync(It.IsAny<CreateReceptionistSummaryDTO>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithExistingId_CallsAllRepositoriesAndServices()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<UpdateReceptionistDTO>();

            _receptionistsRepositoryMock.Setup(x => x.UpdateAsync(id, dto)).ReturnsAsync(1);

            // Act
            await _receptionistsService.UpdateAsync(id, dto);

            // Assert
            _receptionistsRepositoryMock.Verify(x => x.UpdateAsync(id, dto), Times.Once);
            _receptionistsRepositoryMock.Verify(x => x.GetAccountIdAsync(id), Times.Once);
            _messageServiceMock.Verify(x => x.SendUpdateAccountStatusMessageAsync(
                It.IsAny<Guid>(), dto.Status, dto.UpdaterId), Times.Once);
            _receptionistSummaryRepositoryMock.Verify(x => x.UpdateAsync(
                id, It.IsAny<UpdateReceptionistSummaryDTO>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithNoExistingId_CallsOnlyRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<UpdateReceptionistDTO>();

            _receptionistsRepositoryMock.Setup(x => x.UpdateAsync(id, dto)).ReturnsAsync(0);

            // Act
            await _receptionistsService.UpdateAsync(id, dto);

            // Assert
            _receptionistsRepositoryMock.Verify(x => x.UpdateAsync(id, dto), Times.Once);
            _receptionistsRepositoryMock.Verify(x => x.GetAccountIdAsync(id), Times.Never);
            _messageServiceMock.Verify(x => x.SendUpdateAccountStatusMessageAsync(
                It.IsAny<Guid>(), dto.Status, dto.UpdaterId), Times.Never);
            _receptionistSummaryRepositoryMock.Verify(x => x.UpdateAsync(
                id, It.IsAny<UpdateReceptionistSummaryDTO>()), Times.Never);
        }

        [Fact]
        public async Task RemoveAsync_WithExistingId_CallsAllRepositoriesAndServices()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            _receptionistsRepositoryMock.Setup(x => x.RemoveAsync(id)).ReturnsAsync(1);

            // Act
            await _receptionistsService.RemoveAsync(id);

            // Assert
            _receptionistsRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Once);
            _receptionistsRepositoryMock.Verify(x => x.GetPhotoIdAsync(id), Times.Once);
            _messageServiceMock.Verify(x => x.SendDeletePhotoMessageAsync(It.IsAny<Guid>()), Times.Once);
            _receptionistSummaryRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            _receptionistsRepositoryMock.Setup(x => x.RemoveAsync(id)).ReturnsAsync(0);

            // Act
            var act = async () => await _receptionistsService.RemoveAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Receptionist's profile with id = {id} doesn't exist.");

            _receptionistsRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Once);
            _receptionistsRepositoryMock.Verify(x => x.GetPhotoIdAsync(id), Times.Once);
            _messageServiceMock.Verify(x => x.SendDeletePhotoMessageAsync(It.IsAny<Guid>()), Times.Never);
            _receptionistSummaryRepositoryMock.Verify(x => x.RemoveAsync(id), Times.Never);
        }

        [Fact]
        public async Task ChangeStatus_WithExistingId_CallsAllRepositoriesAndServices()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<ChangeStatusDTO>();

            _receptionistSummaryRepositoryMock.Setup(x => x.ChangeStatus(id, dto.Status))
                .ReturnsAsync(1);

            // Act
            await _receptionistsService.ChangeStatus(id, dto);

            // Assert
            _receptionistSummaryRepositoryMock.Verify(x => x.ChangeStatus(id, dto.Status), Times.Once);
            _receptionistsRepositoryMock.Verify(x => x.GetAccountIdAsync(id), Times.Once);
            _messageServiceMock.Verify(x => x.SendUpdateAccountStatusMessageAsync(
                It.IsAny<Guid>(), dto.Status, dto.UpdaterId), Times.Once);
        }

        [Fact]
        public async Task ChangeStatus_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<ChangeStatusDTO>();

            _receptionistSummaryRepositoryMock.Setup(x => x.ChangeStatus(id, dto.Status))
                .ReturnsAsync(0);

            // Act
            var act = async () => await _receptionistsService.ChangeStatus(id, dto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Receptionist's profile with id = {id} doesn't exist.");

            _receptionistSummaryRepositoryMock.Verify(x => x.ChangeStatus(id, dto.Status), Times.Once);
            _receptionistsRepositoryMock.Verify(x => x.GetAccountIdAsync(id), Times.Never);
            _messageServiceMock.Verify(x => x.SendUpdateAccountStatusMessageAsync(
                It.IsAny<Guid>(), dto.Status, dto.UpdaterId), Times.Never);
        }
    }
}

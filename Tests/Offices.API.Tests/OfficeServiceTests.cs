using AutoFixture;
using FluentAssertions;
using Moq;
using Offices.Business.Implementations.Services;
using Offices.Business.Interfaces.Services;
using Offices.Data.DTOs;
using Offices.Data.Interfaces.Repositories;
using Shared.Exceptions;
using Shared.Models.Response.Offices;

namespace Offices.API.Tests
{
    public class OfficeServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IOfficeRepository> _officeRepositoryMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly OfficeService _officeService;

        public OfficeServiceTests()
        {
            _fixture = new Fixture();
            _officeRepositoryMock = new Mock<IOfficeRepository>();
            _messageServiceMock = new Mock<IMessageService>();
            _officeService = new OfficeService(_officeRepositoryMock.Object, _messageServiceMock.Object);
        }

        [Fact]
        public async Task ChangeStatus_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var dto = _fixture.Create<ChangeOfficeStatusDTO>();
            _officeRepositoryMock.Setup(x => x.ChangeStatusAsync(dto)).ReturnsAsync(0);

            // Act
            var act = async () => await _officeService.ChangeStatus(dto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Office with id = {dto.Id} doesn't exist.");

            _officeRepositoryMock.Verify(x => x.ChangeStatusAsync(dto), Times.Once());
        }

        [Fact]
        public async Task CreateAsync_WithValidDto_CallsAddAsyncOnRepository()
        {
            // Arrange
            var dto = _fixture.Create<CreateOfficeDTO>();

            // Act
            await _officeService.CreateAsync(dto);

            // Assert
            _officeRepositoryMock.Verify(x => x.AddAsync(dto), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsOfficeResponse()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var office = _fixture.Create<OfficeResponse>();
            _officeRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(office);

            // Act
            var result = await _officeService.GetByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(office);
            _officeRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            _officeRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(null as OfficeResponse);

            // Act
            var act = async () => await _officeService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Office with id = {id} doesn't exist.");

            _officeRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<UpdateOfficeDTO>();
            _officeRepositoryMock.Setup(x => x.UpdateAsync(id, dto)).ReturnsAsync(0);

            // Act
            var act = async () => await _officeService.UpdateAsync(id, dto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Office with id = {id} doesn't exist.");

            _officeRepositoryMock.Verify(x => x.UpdateAsync(id, dto), Times.Once());
        }
    }
}

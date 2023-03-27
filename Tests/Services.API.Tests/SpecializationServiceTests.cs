using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Services.Business.Implementations;
using Services.Business.Interfaces;
using Services.Data.DTOs.Specialization;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models;
using Shared.Models.Response.Services.Specialization;
using System.Linq.Expressions;

namespace Profiles.API.Tests
{
    public class SpecializationServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRepository<Specialization>> _specializationRepositoryMock;
        private readonly Mock<IServicesRepository> _servicesRepositoryMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ISpecializationService _specializationService;

        public SpecializationServiceTests()
        {
            _fixture = new Fixture();
            _specializationRepositoryMock = new Mock<IRepository<Specialization>>();
            _servicesRepositoryMock = new Mock<IServicesRepository>();
            _messageServiceMock = new Mock<IMessageService>();
            _mapperMock = new Mock<IMapper>();
            _specializationService = new SpecializationService(
                _specializationRepositoryMock.Object,
                _servicesRepositoryMock.Object,
                _messageServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsSpecializationResponse()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var specialization = _fixture.Build<Specialization>()
                .Without(x => x.Services)
                .Create();

            var expectedResponse = _fixture.Create<SpecializationResponse>();

            _specializationRepositoryMock.Setup(x => x.GetByIdAsync(
                id, It.IsAny<Expression<Func<Specialization, object>>[]>()))
                .ReturnsAsync(specialization);

            _mapperMock.Setup(x => x.Map<SpecializationResponse>(It.IsAny<Specialization>()))
                .Returns(expectedResponse);

            // Act
            var response = await _specializationService.GetByIdAsync(id);

            // Assert
            response.Should().BeEquivalentTo(expectedResponse);
            _specializationRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _specializationRepositoryMock.Setup(x => x.GetByIdAsync(
                id, It.IsAny<Expression<Func<Specialization, object>>[]>()))
                .ReturnsAsync(null as Specialization);

            // Act
            var act = async () => await _specializationService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Specialization with id = {id} doesn't exist.");

            _specializationRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once());
        }

        [Fact]
        public async Task CreateAsync_WithValidDto_CallsAddAsyncInRepository()
        {
            // Arrange
            var dto = _fixture.Create<CreateSpecializationDTO>();
            var service = _fixture.Build<Specialization>()
                 .Without(x => x.Services)
                 .Create();

            _mapperMock.Setup(x => x.Map<Specialization>(It.IsAny<CreateSpecializationDTO>()))
                .Returns(service);

            // Act
            await _specializationService.CreateAsync(dto);

            // Assert
            _specializationRepositoryMock.Verify(x => x.AddAsync(service), Times.Once);
        }

        [Fact]
        public async Task GetTaskAsync_WithValidDto_ReturnsPagedResponse()
        {
            // Arrange
            var dto = _fixture.Build<GetSpecializationsDTO>()
                .With(x => x.CurrentPage, 2)
                .With(x => x.PageSize, 5)
                .Create();

            var specializations = _fixture.Build<Specialization>()
                 .Without(x => x.Services)
                 .CreateMany(5);

            var pagedResult = _fixture.Build<PagedResult<Specialization>>()
                .With(x => x.Items, specializations)
                .With(x => x.TotalCount, 20)
                .Create();

            var expectedResponse = _fixture.Create<IEnumerable<SpecializationResponse>>();

            _specializationRepositoryMock.Setup(x => x.GetPagedAndFilteredAsync(
                dto.CurrentPage,
                dto.PageSize,
                It.IsAny<Expression<Func<Specialization, bool>>[]>()))
                .ReturnsAsync(pagedResult);

            _mapperMock.Setup(x => x.Map<IEnumerable<SpecializationResponse>>(
                It.IsAny<IEnumerable<Specialization>>()))
                .Returns(expectedResponse);

            // Act
            var response = await _specializationService.GetPagedAsync(dto);

            // Assert
            response.Items.Should().BeEquivalentTo(expectedResponse);
            response.TotalCount.Should().Be(pagedResult.TotalCount);

            _specializationRepositoryMock.Verify(x => x.GetPagedAndFilteredAsync(
                dto.CurrentPage,
                dto.PageSize,
                It.IsAny<Expression<Func<Specialization, bool>>[]>()), Times.Once);
        }

        [Fact]
        public async Task ChangeStatusAsync_WithAnyParameters_CallsChangeStatusInRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var isActive = _fixture.Create<bool>();

            // Act
            await _specializationService.ChangeStatusAsync(id, isActive);

            // Assert
            _specializationRepositoryMock.Verify(x => x.ChangeStatusAsync(id, isActive), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<UpdateSpecializationDTO>();

            var specialization = _fixture.Build<Specialization>()
                .With(x => x.Id, id)
                .Without(x => x.Services)
                .Create();

            _mapperMock.Setup(x => x.Map<Specialization>(It.IsAny<UpdateSpecializationDTO>()))
                .Returns(specialization);

            _specializationRepositoryMock.Setup(x => x.UpdateAsync(specialization))
                .ThrowsAsync(new NotFoundException($"Entity with id = {id} doesn't exist"));

            // Act
            var act = async () => await _specializationService.UpdateAsync(id, dto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Entity with id = {id} doesn't exist");

            _specializationRepositoryMock.Verify(x => x.UpdateAsync(specialization), Times.Once);
        }
    }
}

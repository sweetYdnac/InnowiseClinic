using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services.Business.Implementations;
using Services.Business.Interfaces;
using Services.Data.DTOs.Service;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models;
using Shared.Models.Response.Services.Service;
using System.Linq.Expressions;

namespace Profiles.API.Tests
{
    public class ServicesServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IServicesRepository> _serviceRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IServicesService _servicesService;

        public ServicesServiceTests()
        {
            _fixture = new Fixture();
            _serviceRepositoryMock = new Mock<IServicesRepository>();
            _mapperMock = new Mock<IMapper>();
            _servicesService = new ServicesService(
                _serviceRepositoryMock.Object,
                new Mock<IMessageService>().Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsServiceCategoryResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = _fixture.Build<Service>()
                .Without(x => x.Specialization)
                .Without(x => x.Category)
                .Create();

            var expectedResponse = _fixture.Create<ServiceResponse>();

            _serviceRepositoryMock.Setup(x => x.GetByIdAsync(
                id, It.IsAny<Expression<Func<Service, object>>[]>()))
                .ReturnsAsync(service);

            _mapperMock.Setup(x => x.Map<ServiceResponse>(It.IsAny<Service>()))
                .Returns(expectedResponse);

            // Act
            var result = await _servicesService.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expectedResponse);
            _serviceRepositoryMock.Verify(x => x.GetByIdAsync(
                id, It.IsAny<Expression<Func<Service, object>>[]>()), Times.Once());
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            _serviceRepositoryMock.Setup(x => x.GetByIdAsync(
                id, It.IsAny<Expression<Func<Service, object>>[]>()))
                .ReturnsAsync(null as Service);

            // Act
            var act = async () => await _servicesService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Service with id = {id} doesn't exist.");

            _serviceRepositoryMock.Verify(x => x.GetByIdAsync(
                id, It.IsAny<Expression<Func<Service, object>>[]>()), Times.Once());
        }

        [Fact]
        public async Task GetTaskAsync_WithValidDto_ReturnsPagedResponse()
        {
            // Arrange
            var dto = _fixture.Build<GetServicesDTO>()
                .With(x => x.CurrentPage, 2)
                .With(x => x.PageSize, 5)
                .Create();

            var services = _fixture.Build<Service>()
                 .Without(x => x.Specialization)
                 .Without(x => x.Category)
                 .CreateMany(5);

            var pagedResult = _fixture.Build<PagedResult<Service>>()
                .With(x => x.Items, services)
                .With(x => x.TotalCount, 20)
                .Create();

            var expectedResponse = _fixture.Create<IEnumerable<ServiceResponse>>();

            _serviceRepositoryMock.Setup(x => x.GetPagedAndFilteredAsync(
                dto.CurrentPage,
                dto.PageSize,
                It.IsAny<IEnumerable<Expression<Func<Service, object>>>>(),
                It.IsAny<Expression<Func<Service, bool>>[]>()))
                .ReturnsAsync(pagedResult);

            _mapperMock.Setup(x => x.Map<IEnumerable<ServiceResponse>>(It.IsAny<IEnumerable<Service>>()))
                .Returns(expectedResponse);

            // Act
            var response = await _servicesService.GetPagedAsync(dto);

            // Assert
            response.Items.Should().BeEquivalentTo(expectedResponse);
            response.TotalCount.Should().Be(pagedResult.TotalCount);

            _serviceRepositoryMock.Verify(x => x.GetPagedAndFilteredAsync(
                dto.CurrentPage,
                dto.PageSize,
                It.IsAny<IEnumerable<Expression<Func<Service, object>>>>(),
                It.IsAny<Expression<Func<Service, bool>>[]>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithValidDto_CallsAddAsyncInRepository()
        {
            // Arrange
            var dto = _fixture.Create<CreateServiceDTO>();
            var service = _fixture.Build<Service>()
                 .Without(x => x.Specialization)
                 .Without(x => x.Category)
                 .Create();

            _mapperMock.Setup(x => x.Map<Service>(It.IsAny<CreateServiceDTO>()))
                .Returns(service);

            // Act
            await _servicesService.CreateAsync(dto);

            // Assert
            _serviceRepositoryMock.Verify(x => x.AddAsync(service), Times.Once);
        }

        [Fact]
        public async Task ChangeStatusAsync_WithAnyParameters_CallsChangeStatusInRepository()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var isActive = _fixture.Create<bool>();

            // Act
            await _servicesService.ChangeStatusAsync(id, isActive);

            // Assert
            _serviceRepositoryMock.Verify(x => x.ChangeStatusAsync(id, isActive), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var dto = _fixture.Create<UpdateServiceDTO>();

            var service = _fixture.Build<Service>()
                .With(x => x.Id, id)
                .Without(x => x.Specialization)
                .Without(x => x.Category)
                .Create();

            _mapperMock.Setup(x => x.Map<Service>(It.IsAny<UpdateServiceDTO>()))
                .Returns(service);

            _serviceRepositoryMock.Setup(x => x.UpdateAsync(service))
                .ThrowsAsync(new DbUpdateConcurrencyException());

            // Act
            var act = async () => await _servicesService.UpdateAsync(id, dto);

            // Assert
            await act.Should().ThrowAsync<DbUpdateConcurrencyException>();

            _serviceRepositoryMock.Verify(x => x.UpdateAsync(service), Times.Once);
        }
    }
}

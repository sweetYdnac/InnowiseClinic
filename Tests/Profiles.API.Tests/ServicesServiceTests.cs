using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Services.Business.Implementations;
using Services.Business.Interfaces;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models.Response.Services.Service;
using Shared.Models.Response.Services.ServiceCategories;
using System.Linq.Expressions;

namespace Profiles.API.Tests
{
    public class ServicesServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IServicesRepository> _serviceRepositoryMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IServicesService _servicesService;

        public ServicesServiceTests()
        {
            _fixture = new Fixture();
            _serviceRepositoryMock = new Mock<IServicesRepository>();
            _messageServiceMock = new Mock<IMessageService>();
            _mapperMock = new Mock<IMapper>();
            _servicesService = new ServicesService(
                _serviceRepositoryMock.Object, _messageServiceMock.Object, _mapperMock.Object);
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

            _serviceRepositoryMock.Setup(x => x.GetByIdAsync(id, It.IsAny<Expression<Func<Service, object>>>())).ReturnsAsync(service);
            _mapperMock.Setup(x => x.Map<ServiceResponse>(It.IsAny<Service>())).Returns(expectedResponse);

            // Act
            var result = await _servicesService.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            _serviceRepositoryMock.Setup(x => x.GetByIdAsync(id, It.IsAny<Expression<Func<Service, object>>>()))
                .ReturnsAsync(null as Service);

            // Act
            var act = async () => await _servicesService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Service with id = {id} doesn't exist.");
        }

        [Fact]
        public async Task GetTaskAsync_WithValidDto_ReturnsPagedResponse()
        {

        }
    }
}

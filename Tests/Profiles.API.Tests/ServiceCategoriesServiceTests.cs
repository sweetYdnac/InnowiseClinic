using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Services.Business.Implementations;
using Services.Business.Interfaces;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models.Response.Services.ServiceCategories;

namespace Profiles.API.Tests
{
    public class ServiceCategoriesServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IServiceCategoriesRepository> _serviceCategoriesRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IServiceCategoriesService _serviceCategoriesService;

        public ServiceCategoriesServiceTests()
        {
            _fixture = new Fixture();
            _serviceCategoriesRepositoryMock = new Mock<IServiceCategoriesRepository>();
            _mapperMock = new Mock<IMapper>();
            _serviceCategoriesService = new ServiceCategoriesService(
                _serviceCategoriesRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsServiceCategoryResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var category = _fixture.Build<ServiceCategory>().Without(x => x.Services).Create();
            var expectedResponse = _fixture.Create<ServiceCategoryResponse>();

            _serviceCategoriesRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(category);
            _mapperMock.Setup(x => x.Map<ServiceCategoryResponse>(It.IsAny<ServiceCategory>())).Returns(expectedResponse);

            // Act
            var result = await _serviceCategoriesService.GetByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetByIdAsync_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedResponse = _fixture.Create<ServiceCategoryResponse>();

            _serviceCategoriesRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(null as ServiceCategory);

            // Act
            var act = async () => await _serviceCategoriesService.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Service category with id = {id} doesn't exist.");
        }

        [Fact]
        public async Task GetAllAsync_WithExistingMap_ReturnsServiceCategoryResponse()
        {
            // Arrange
            var categories = _fixture.Build<ServiceCategory>()
                .Without(x => x.Services)
                .CreateMany();

            var expectedResponse = _fixture.CreateMany<ServiceCategoryResponse>();

            _serviceCategoriesRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);
            _mapperMock.Setup(x => x.Map<IEnumerable<ServiceCategoryResponse>>(It.IsAny<IEnumerable<ServiceCategory>>()))
                .Returns(expectedResponse);

            // Act
            var response = await _serviceCategoriesService.GetAllAsync();

            // Assert
            response.Categories.Should().NotBeNull().And.BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetAllAsync_WithNoExistingMap_ThrowsAutoMapperMappingException()
        {
            // Arrange
            var categories = _fixture.Build<ServiceCategory>()
                .Without(x => x.Services)
                .CreateMany();

            _serviceCategoriesRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);
            _mapperMock.Setup(x => x.Map<IEnumerable<ServiceCategoryResponse>>(It.IsAny<IEnumerable<ServiceCategory>>()))
                .Throws<AutoMapperMappingException>();

            // Act
            var act = _serviceCategoriesService.GetAllAsync;

            // Assert
            await act.Should().ThrowAsync<AutoMapperMappingException>();
        }
    }
}

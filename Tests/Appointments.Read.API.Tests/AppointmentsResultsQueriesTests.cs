using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Features.Queries.AppointmentsResults;
using Appointments.Read.Application.Interfaces.Repositories;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Shared.Exceptions;
using Shared.Models.Response.Appointments.AppointmentResult;

namespace Appointments.Read.API.Tests
{
    public class AppointmentsResultsQueriesTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentsResultsRepository> _appointmentsResultsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly GetAppointmentResultQueryHandler _getAppointmentResultQueryHandler;

        public AppointmentsResultsQueriesTests()
        {
            _fixture = new Fixture();
            _appointmentsResultsRepositoryMock = new Mock<IAppointmentsResultsRepository>();
            _mapperMock = new Mock<IMapper>();

            _getAppointmentResultQueryHandler = new GetAppointmentResultQueryHandler(
                _appointmentsResultsRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAppointmentResult_WithExistingId_ReturnsAppointmentResultResponse()
        {
            // Arrange
            var request = _fixture.Create<GetAppointmentResultQuery>();

            var dto = _fixture.Build<AppointmentResultDTO>()
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            var expectedResult = _fixture.Build<AppointmentResultResponse>()
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _appointmentsResultsRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync(dto);

            _mapperMock.Setup(x => x.Map<AppointmentResultResponse>(dto))
                .Returns(expectedResult);

            // Act
            var response = await _getAppointmentResultQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            response.Should().BeEquivalentTo(expectedResult).And.NotBeNull();
            _appointmentsResultsRepositoryMock.Verify(x => x.GetByIdAsync(request.Id), Times.Once);
        }

        [Fact]
        public async Task GetAppointmentResult_WithNoExistingId_ThrowsNotFoundException()
        {
            // Arrange
            var request = _fixture.Create<GetAppointmentResultQuery>();

            _appointmentsResultsRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                .ReturnsAsync(null as AppointmentResultDTO);

            // Act
            var act = async () => await _getAppointmentResultQueryHandler.Handle(request,
                It.IsAny<CancellationToken>());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Appointment result with id = {request.Id} doesn't exist");

            _appointmentsResultsRepositoryMock.Verify(x => x.GetByIdAsync(request.Id), Times.Once);
            _mapperMock.Verify(x => x.Map<AppointmentResultResponse>(It.IsAny<AppointmentResultDTO>()),
                Times.Never);
        }
    }
}

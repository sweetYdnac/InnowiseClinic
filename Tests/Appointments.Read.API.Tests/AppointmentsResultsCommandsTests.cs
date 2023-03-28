using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Features.Commands.AppointmentsResults;
using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using AutoFixture;
using AutoMapper;
using Moq;

namespace Appointments.Read.API.Tests
{
    public class AppointmentsResultsCommandsTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentsResultsRepository> _appointmentsResultsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly CreateAppointmentResultCommandHandler _createAppointmentResultCommandHandler;
        private readonly EditAppointmentResultCommandHandler _editAppointmentResultCommandHandler;

        public AppointmentsResultsCommandsTests()
        {
            _fixture = new Fixture();
            _appointmentsResultsRepositoryMock = new Mock<IAppointmentsResultsRepository>();
            _mapperMock = new Mock<IMapper>();

            _createAppointmentResultCommandHandler = new CreateAppointmentResultCommandHandler(_appointmentsResultsRepositoryMock.Object, _mapperMock.Object);
            _editAppointmentResultCommandHandler = new EditAppointmentResultCommandHandler(_appointmentsResultsRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAppointmentResult_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Create<CreateAppointmentResultCommand>();

            // Act
            await _createAppointmentResultCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsResultsRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AppointmentResult>()));
        }

        [Fact]
        public async Task EditAppointment_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Create<EditAppointmentResultCommand>();

            // Act
            await _editAppointmentResultCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsResultsRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<EditAppointmentResultDTO>()));
        }
    }
}

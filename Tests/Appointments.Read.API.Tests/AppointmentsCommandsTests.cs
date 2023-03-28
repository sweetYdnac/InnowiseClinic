using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Application.Features.Commands.Appointments;
using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using AutoFixture;
using AutoMapper;
using Moq;

namespace Appointments.Read.API.Tests
{
    public class AppointmentsCommandsTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentsRepository> _appointmentRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly ApproveAppointmentCommandHandler _approveAppointmentCommandHandler;
        private readonly CancelAppointmentCommandHandler _cancelAppointmentCommandHandler;
        private readonly CreateAppointmentCommandHandler _createAppointmentCommandHandler;
        private readonly RescheduleAppointmentCommandHandler _rescheduleAppointmentCommandHandler;
        private readonly UpdateDoctorCommandHandler _updateDoctorCommandHandler;
        private readonly UpdatePatientCommandHandler _updatePatientCommandHandler;
        private readonly UpdateServiceCommandHandler _updateServiceCommandHandler;

        public AppointmentsCommandsTests()
        {
            _fixture = new Fixture();
            _appointmentRepositoryMock = new Mock<IAppointmentsRepository>();
            _mapperMock = new Mock<IMapper>();

            _approveAppointmentCommandHandler = new ApproveAppointmentCommandHandler(
                _appointmentRepositoryMock.Object);
            _cancelAppointmentCommandHandler = new CancelAppointmentCommandHandler(
                _appointmentRepositoryMock.Object);
            _createAppointmentCommandHandler = new CreateAppointmentCommandHandler(
                _appointmentRepositoryMock.Object, _mapperMock.Object);
            _rescheduleAppointmentCommandHandler = new RescheduleAppointmentCommandHandler(
                _appointmentRepositoryMock.Object, _mapperMock.Object);
            _updateDoctorCommandHandler = new UpdateDoctorCommandHandler(
                _appointmentRepositoryMock.Object, _mapperMock.Object);
            _updatePatientCommandHandler = new UpdatePatientCommandHandler(
                _appointmentRepositoryMock.Object, _mapperMock.Object);
            _updateServiceCommandHandler = new UpdateServiceCommandHandler(
                _appointmentRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ApproveAppointment_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Create<ApproveAppointmentCommand>();

            // Act
            await _approveAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentRepositoryMock.Verify(x => x.ApproveAsync(request.Id), Times.Once);
        }

        [Fact]
        public async Task CancelAppointment_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Create<CancelAppointmentCommand>();

            // Act
            await _cancelAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentRepositoryMock.Verify(x => x.DeleteByIdAsync(request.Id), Times.Once);
        }

        [Fact]
        public async Task CreateAppointment_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Build<CreateAppointmentCommand>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .With(x => x.Time, TimeOnly.FromDateTime(DateTime.UtcNow))
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            // Act
            await _createAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Appointment>()), Times.Once);
        }

        [Fact]
        public async Task RescheduleAppointment_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Build<RescheduleAppointmentCommand>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .With(x => x.Time, TimeOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            // Act
            await _rescheduleAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentRepositoryMock.Verify(x => x.RescheduleAsync(It.IsAny<RescheduleAppointmentDTO>()));
        }

        [Fact]
        public async Task UpdateDoctor_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Create<UpdateDoctorCommand>();

            // Act
            await _updateDoctorCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentRepositoryMock.Verify(x => x.UpdateDoctorAsync(It.IsAny<UpdateDoctorDTO>()));
        }

        [Fact]
        public async Task UpdatePatient_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Build<UpdatePatientCommand>()
                .With(x => x.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            // Act
            await _updatePatientCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentRepositoryMock.Verify(x => x.UpdatePatientAsync(It.IsAny<UpdatePatientDTO>()));
        }

        [Fact]
        public async Task UpdateService_WithAnyRequest_CallsRepository()
        {
            // Arrange
            var request = _fixture.Create<UpdateServiceCommand>();

            // Act
            await _updateServiceCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentRepositoryMock.Verify(x => x.UpdateServiceAsync(It.IsAny<UpdateServiceDTO>()));
        }
    }
}

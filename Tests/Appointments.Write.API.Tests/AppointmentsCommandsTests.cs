using Appointments.Write.Application.Features.Commands.Appointments;
using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using Appointments.Write.Domain.Entities;
using AutoFixture;
using AutoMapper;
using Moq;
using Shared.Messages;

namespace Appointments.Write.API.Tests
{
    public class AppointmentsCommandsTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentsRepository> _appointmentsRepositoryMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly ApproveAppointmentCommandHandler _approveAppointmentCommandHandler;
        private readonly CancelAppointmentCommandHandler _cancelAppointmentCommandHandler;
        private readonly CreateAppointmentCommandHandler _createAppointmentCommandHandler;
        private readonly RescheduleAppointmentCommandHandler _rescheduleAppointmentCommandHandler;

        public AppointmentsCommandsTests()
        {
            _fixture = new Fixture();
            _appointmentsRepositoryMock = new Mock<IAppointmentsRepository>();
            _messageServiceMock = new Mock<IMessageService>();
            _mapperMock = new Mock<IMapper>();

            _approveAppointmentCommandHandler = new ApproveAppointmentCommandHandler(
                _appointmentsRepositoryMock.Object, _messageServiceMock.Object);
            _cancelAppointmentCommandHandler = new CancelAppointmentCommandHandler(
                _appointmentsRepositoryMock.Object, _messageServiceMock.Object);
            _createAppointmentCommandHandler = new CreateAppointmentCommandHandler(
                _appointmentsRepositoryMock.Object, _mapperMock.Object, _messageServiceMock.Object);
            _rescheduleAppointmentCommandHandler = new RescheduleAppointmentCommandHandler(
                _appointmentsRepositoryMock.Object, _messageServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ApproveAppointment_WithAnyRequest_CallsRepositoryAndService()
        {
            // Arrange
            var request = _fixture.Create<ApproveAppointmentCommand>();

            // Act
            await _approveAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsRepositoryMock.Verify(x => x.ApproveAsync(request.Id), Times.Once);
            _messageServiceMock.Verify(x => x.SendApproveAppointmentMessageAsync(request.Id), Times.Once);
        }

        [Fact]
        public async Task CancelAppointment_WithExistingId_CallsRepositoryAndService()
        {
            // Arrange
            var request = _fixture.Create<CancelAppointmentCommand>();

            _appointmentsRepositoryMock.Setup(x => x.DeleteByIdAsync(request.Id))
                .ReturnsAsync(1);

            // Act
            await _cancelAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsRepositoryMock.Verify(x => x.DeleteByIdAsync(request.Id), Times.Once);
            _messageServiceMock.Verify(x => x.SendDeleteAppointmentMessageAsync(request.Id), Times.Once);
        }

        [Fact]
        public async Task CancelAppointment_WithNoExistingId_CallsOnlyRepository()
        {
            // Arrange
            var request = _fixture.Create<CancelAppointmentCommand>();

            _appointmentsRepositoryMock.Setup(x => x.DeleteByIdAsync(request.Id))
                .ReturnsAsync(0);

            // Act
            await _cancelAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsRepositoryMock.Verify(x => x.DeleteByIdAsync(request.Id), Times.Once);
            _messageServiceMock.Verify(x => x.SendDeleteAppointmentMessageAsync(request.Id), Times.Never);
        }

        [Fact]
        public async Task CreateAppointment_WithAnyRequest_CallsRepositoryAndService()
        {
            // Arrange
            var request = _fixture.Build<CreateAppointmentCommand>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            // Act
            await _createAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Appointment>()), Times.Once);
            _messageServiceMock.Verify(x => x.SendCreateAppointmentMessageAsync(
                It.IsAny<CreateAppointmentMessage>()), Times.Once);
        }

        [Fact]
        public async Task RescheduleAppointment_WithExistingId_CallsRepositoryAndService()
        {
            // Arrange
            var request = _fixture.Build<RescheduleAppointmentCommand>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _appointmentsRepositoryMock.Setup(x => x.RescheduleAppointment(request))
                .ReturnsAsync(1);

            // Act
            await _rescheduleAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsRepositoryMock.Verify(x => x.RescheduleAppointment(request), Times.Once);
            _messageServiceMock.Verify(x => x.SendRescheduleAppointmentMessageAsync(
                It.IsAny<RescheduleAppointmentMessage>()), Times.Once);
        }

        [Fact]
        public async Task RescheduleAppointment_WithNoExistingId_CallsOnlyRepository()
        {
            // Arrange
            var request = _fixture.Build<RescheduleAppointmentCommand>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _appointmentsRepositoryMock.Setup(x => x.RescheduleAppointment(request))
                .ReturnsAsync(0);

            // Act
            await _rescheduleAppointmentCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsRepositoryMock.Verify(x => x.RescheduleAppointment(request), Times.Once);
            _messageServiceMock.Verify(x => x.SendRescheduleAppointmentMessageAsync(
                It.IsAny<RescheduleAppointmentMessage>()), Times.Never);
        }
    }
}

using Appointments.Write.Application.DTOs.AppointmentResult;
using Appointments.Write.Application.Features.Commands.AppointmentsResults;
using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Application.Interfaces.Services;
using Appointments.Write.Domain.Entities;
using AutoFixture;
using AutoMapper;
using Moq;
using Shared.Messages;

namespace Appointments.Write.API.Tests
{
    public class AppointmentsResultsCommandsTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentsResultsRepository> _appointmentsResultsRepositoryMock;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly CreateAppointmentResultCommandHandler _createAppointmentResultCommandHandler;
        private readonly EditAppointmentResultCommandHandler _editAppointmentResultCommandHandler;

        public AppointmentsResultsCommandsTests()
        {
            _fixture = new Fixture();
            _appointmentsResultsRepositoryMock = new Mock<IAppointmentsResultsRepository>();
            _messageServiceMock = new Mock<IMessageService>();
            _mapperMock = new Mock<IMapper>();

            _createAppointmentResultCommandHandler = new CreateAppointmentResultCommandHandler(
                _appointmentsResultsRepositoryMock.Object,
                _messageServiceMock.Object,
                _mapperMock.Object);
            _editAppointmentResultCommandHandler = new EditAppointmentResultCommandHandler(
                _appointmentsResultsRepositoryMock.Object,
                _messageServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAppointmentResult_WithAnyRequest_CallsRepositoryAndService()
        {
            // Arrange
            var request = _fixture.Build<CreateAppointmentResultCommand>()
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            // Act
            await _createAppointmentResultCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsResultsRepositoryMock.Verify(x => x.AddAsync(
                It.IsAny<AppointmentResult>()), Times.Once);
            _messageServiceMock.Verify(x => x.SendCreateAppointmentResultMessageAsync(
                It.IsAny<CreateAppointmentResultMessage>()), Times.Once);
            _messageServiceMock.Verify(x => x.SendGeneratePdfMessageAsync(
                It.IsAny<GeneratePdfMessage>()), Times.Once);
        }

        [Fact]
        public async Task EditAppointmentResult_WithExistingId_CallsRepositoryAndService()
        {
            // Arrange
            var request = _fixture.Build<EditAppointmentResultCommand>()
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _appointmentsResultsRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<EditAppointmentResultDTO>()))
                .ReturnsAsync(1);

            // Act
            await _editAppointmentResultCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsResultsRepositoryMock.Verify(x => x.UpdateAsync(
                It.IsAny<EditAppointmentResultDTO>()), Times.Once);
            _messageServiceMock.Verify(x => x.SendEditAppointmentResultMessageAsync(
                It.IsAny<EditAppointmentResultMessage>()), Times.Once);
            _messageServiceMock.Verify(x => x.SendGeneratePdfMessageAsync(
                It.IsAny<GeneratePdfMessage>()), Times.Once);
        }

        [Fact]
        public async Task EditAppointmentResult_WithNoExistingId_CallsOnlyRepository()
        {
            // Arrange
            var request = _fixture.Build<EditAppointmentResultCommand>()
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            _appointmentsResultsRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<EditAppointmentResultDTO>()))
                .ReturnsAsync(0);

            // Act
            await _editAppointmentResultCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            _appointmentsResultsRepositoryMock.Verify(x => x.UpdateAsync(
                It.IsAny<EditAppointmentResultDTO>()), Times.Once);
            _messageServiceMock.Verify(x => x.SendEditAppointmentResultMessageAsync(
                It.IsAny<EditAppointmentResultMessage>()), Times.Never);
            _messageServiceMock.Verify(x => x.SendGeneratePdfMessageAsync(
                It.IsAny<GeneratePdfMessage>()), Times.Never);
        }
    }
}

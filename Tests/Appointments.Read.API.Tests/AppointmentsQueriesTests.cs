using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Application.Features.Queries.Appointments;
using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Shared.Models;
using Shared.Models.Response.Appointments.Appointment;
using System.Linq.Expressions;

namespace Appointments.Read.API.Tests
{
    public class AppointmentsQueriesTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentsRepository> _appointmentsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly GetAppointmentsQueryHandler _getAppointmentsQueryHandler;
        private readonly GetDoctorScheduleQueryHandler _getDoctorScheduleQueryHandler;
        private readonly GetPatientHistoryQueryHandler _getPatientHistoryQueryHandler;
        private readonly GetTimeSlotsQueryHandler _getTimeSlotsQueryHandler;

        public AppointmentsQueriesTests()
        {
            _fixture = new Fixture();
            _appointmentsRepositoryMock = new Mock<IAppointmentsRepository>();
            _mapperMock = new Mock<IMapper>();

            _getAppointmentsQueryHandler = new GetAppointmentsQueryHandler(
                _appointmentsRepositoryMock.Object, _mapperMock.Object);
            _getDoctorScheduleQueryHandler = new GetDoctorScheduleQueryHandler(
                _appointmentsRepositoryMock.Object, _mapperMock.Object);
            _getPatientHistoryQueryHandler = new GetPatientHistoryQueryHandler(
                _appointmentsRepositoryMock.Object, _mapperMock.Object);
            _getTimeSlotsQueryHandler = new GetTimeSlotsQueryHandler(
                _appointmentsRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAppointments_WithValidDto_ReturnsPagedResponse()
        {
            // Arrange
            var request = _fixture.Build<GetAppointmentsQuery>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            var appointments = _fixture.Create<PagedResult<AppointmentDTO>>();

            var expectedAppointments = _fixture.Create<IEnumerable<AppointmentResponse>>();

            _appointmentsRepositoryMock.Setup(x => x.GetAppointmentsAsync(
                request.CurrentPage,
                request.PageSize,
                It.IsAny<IDictionary<Expression<Func<Appointment, object>>, bool>>(),
                It.IsAny<Expression<Func<Appointment, bool>>[]>()))
                .ReturnsAsync(appointments);

            _mapperMock.Setup(x => x.Map<IEnumerable<AppointmentResponse>>(appointments.Items))
                .Returns(expectedAppointments);

            // Act
            var response = await _getAppointmentsQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            response.Items.Should().BeEquivalentTo(expectedAppointments).And.NotBeNull();

            _appointmentsRepositoryMock.Verify(x => x.GetAppointmentsAsync(
                request.CurrentPage,
                request.PageSize,
                It.IsAny<IDictionary<Expression<Func<Appointment, object>>, bool>>(),
                It.IsAny<Expression<Func<Appointment, bool>>[]>()),
                Times.Once);
        }

        [Fact]
        public async Task GetDoctorSchedule_WithValidDto_ReturnsPagedResponse()
        {
            // Arrange
            var request = _fixture.Build<GetDoctorScheduleQuery>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .Create();

            var appointments = _fixture.Build<DoctorScheduledAppointmentDTO>()
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .CreateMany(request.PageSize);

            var pagedResult = _fixture.Build<PagedResult<DoctorScheduledAppointmentDTO>>()
                .With(x => x.Items, appointments)
                .Create();

            var expectedAppointments = _fixture.Build<DoctorScheduledAppointmentResponse>()
                .With(x => x.PatientDateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow))
                .CreateMany(request.PageSize);

            _appointmentsRepositoryMock.Setup(x => x.GetDoctorScheduleAsync(
                request.CurrentPage,
                request.PageSize,
                It.IsAny<Expression<Func<Appointment, object>>[]>(),
                It.IsAny<IDictionary<Expression<Func<Appointment, object>>, bool>>(),
                It.IsAny<Expression<Func<Appointment, bool>>[]>()))
                .ReturnsAsync(pagedResult);

            _mapperMock.Setup(x => x.Map<IEnumerable<DoctorScheduledAppointmentResponse>>(pagedResult.Items))
                .Returns(expectedAppointments);

            // Act
            var response = await _getDoctorScheduleQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            response.Items.Should().BeEquivalentTo(expectedAppointments).And.NotBeNull();

            _appointmentsRepositoryMock.Verify(x => x.GetDoctorScheduleAsync(
                request.CurrentPage,
                request.PageSize,
                It.IsAny<Expression<Func<Appointment, object>>[]>(),
                It.IsAny<IDictionary<Expression<Func<Appointment, object>>, bool>>(),
                It.IsAny<Expression<Func<Appointment, bool>>[]>()),
                Times.Once);
        }

        [Fact]
        public async Task GetPatientHistory_WithValidDto_ReturnsPagedResponse()
        {
            // Arrange
            var request = _fixture.Create<GetPatientHistoryQuery>();

            var appointments = _fixture.Build<AppointmentHistoryDTO>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .CreateMany(request.PageSize);

            var pagedResult = _fixture.Build<PagedResult<AppointmentHistoryDTO>>()
                .With(x => x.Items, appointments)
                .Create();

            var expectedAppointments = _fixture.Build<AppointmentHistoryResponse>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .CreateMany(request.PageSize);

            _appointmentsRepositoryMock.Setup(x => x.GetAppointmentHistoryAsync(
                request.CurrentPage,
                request.PageSize,
                It.IsAny<Expression<Func<Appointment, object>>[]>(),
                It.IsAny<IDictionary<Expression<Func<Appointment, object>>, bool>>(),
                It.IsAny<Expression<Func<Appointment, bool>>[]>()))
                .ReturnsAsync(pagedResult);

            _mapperMock.Setup(x => x.Map<IEnumerable<AppointmentHistoryResponse>>(pagedResult.Items))
                .Returns(expectedAppointments);

            // Act
            var response = await _getPatientHistoryQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            response.Items.Should().BeEquivalentTo(expectedAppointments).And.NotBeNull();

            _appointmentsRepositoryMock.Verify(x => x.GetAppointmentHistoryAsync(
                request.CurrentPage,
                request.PageSize,
                It.IsAny<Expression<Func<Appointment, object>>[]>(),
                It.IsAny<IDictionary<Expression<Func<Appointment, object>>, bool>>(),
                It.IsAny<Expression<Func<Appointment, bool>>[]>()),
                Times.Once);
        }

        [Fact]
        public async Task GetTimeSlots_WithNoAppointments_ReturnsAllTimeSlots()
        {
            // Arrange
            var doctors = new Guid[]
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };

            var request = _fixture.Build<GetTimeSlotsQuery>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .With(x => x.Doctors, doctors)
                .With(x => x.Duration, 30)
                .With(x => x.StartTime, new TimeOnly(8, 00))
                .With(x => x.EndTime, new TimeOnly(13, 00))
                .Create();

            _appointmentsRepositoryMock.Setup(x => x.GetAppointmentsAsync(request.Date, request.Doctors))
                .ReturnsAsync(Enumerable.Empty<TimeSlotAppointmentDTO>());

            var expectedResponse = new Dictionary<TimeOnly, HashSet<Guid>>
            {
                { new TimeOnly(8, 00), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 10), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 20), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 30), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 40), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 50), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 00), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 10), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 20), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 30), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 40), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 50), new HashSet<Guid>(doctors) },
                { new TimeOnly(10,00), new HashSet<Guid>(doctors) },
                { new TimeOnly(10,10), new HashSet<Guid>(doctors) },
                { new TimeOnly(10,20), new HashSet<Guid>(doctors) },
                { new TimeOnly(10,30), new HashSet<Guid>(doctors) },
                { new TimeOnly(10,40), new HashSet<Guid>(doctors) },
                { new TimeOnly(10,50), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,00), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,10), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,20), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,30), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,40), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,50), new HashSet<Guid>(doctors) },
                { new TimeOnly(12,00), new HashSet<Guid>(doctors) },
                { new TimeOnly(12,10), new HashSet<Guid>(doctors) },
                { new TimeOnly(12,20), new HashSet<Guid>(doctors) },
                { new TimeOnly(12,30), new HashSet<Guid>(doctors) },
            };

            // Act
            var response = await _getTimeSlotsQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            response.TimeSlots.Should().BeEquivalentTo(expectedResponse);
            _appointmentsRepositoryMock.Verify(x => x.GetAppointmentsAsync(request.Date, request.Doctors), Times.Once);
        }

        [Fact]
        public async Task GetTimeSlots_WithFindedAppointments_ReturnsSpecificTimeSlots()
        {
            // Arrange
            var doctors = new Guid[]
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };

            var request = _fixture.Build<GetTimeSlotsQuery>()
                .With(x => x.Date, DateOnly.FromDateTime(DateTime.UtcNow))
                .With(x => x.Doctors, doctors)
                .With(x => x.Duration, 20)
                .With(x => x.StartTime, new TimeOnly(8,00))
                .With(x => x.EndTime, new TimeOnly(13,00))
                .Create();

            var appointments = new[]
            {
                new TimeSlotAppointmentDTO
                {
                    StartTime = new TimeOnly(09,40),
                    EndTime = new TimeOnly(10,10),
                    DoctorId = doctors[2]
                },
                new TimeSlotAppointmentDTO
                {
                    StartTime = new TimeOnly(10,10),
                    EndTime = new TimeOnly(10,40),
                    DoctorId = doctors[0]
                },
                new TimeSlotAppointmentDTO
                {
                    StartTime = new TimeOnly(10,20),
                    EndTime = new TimeOnly(10,40),
                    DoctorId = doctors[1]
                },
                new TimeSlotAppointmentDTO
                {
                    StartTime = new TimeOnly(12,20),
                    EndTime = new TimeOnly(12,30),
                    DoctorId = doctors[2]
                }
            };

            _appointmentsRepositoryMock.Setup(x => x.GetAppointmentsAsync(request.Date, request.Doctors))
                .ReturnsAsync(appointments);

            var expectedResponse = new Dictionary<TimeOnly, HashSet<Guid>>
            {
                { new TimeOnly(8, 00), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 10), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 20), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 30), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 40), new HashSet<Guid>(doctors) },
                { new TimeOnly(8, 50), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 00), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 10), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 20), new HashSet<Guid>(doctors) },
                { new TimeOnly(9, 30), new HashSet<Guid>(new Guid[] {doctors[0], doctors[1] }) },
                { new TimeOnly(9, 40), new HashSet<Guid>(new Guid[] {doctors[0], doctors[1] }) },
                { new TimeOnly(9, 50), new HashSet<Guid>(new Guid[] {doctors[0], doctors[1] }) },
                { new TimeOnly(10,00), new HashSet<Guid>(new Guid[] { doctors[1] }) },
                { new TimeOnly(10,20), new HashSet<Guid>(new Guid[] { doctors[2] }) },
                { new TimeOnly(10,30), new HashSet<Guid>(new Guid[] { doctors[2] }) },
                { new TimeOnly(10,40), new HashSet<Guid>(new Guid[] { doctors[2] }) },
                { new TimeOnly(10,50), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,00), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,10), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,20), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,30), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,40), new HashSet<Guid>(doctors) },
                { new TimeOnly(11,50), new HashSet<Guid>(doctors) },
                { new TimeOnly(12,00), new HashSet<Guid>(doctors) },
                { new TimeOnly(12,10), new HashSet<Guid>(new Guid[] {doctors[0], doctors[1] }) },
                { new TimeOnly(12,20), new HashSet<Guid>(new Guid[] {doctors[0], doctors[1] }) },
                { new TimeOnly(12,30), new HashSet<Guid>(new Guid[] {doctors[0], doctors[1] }) },
                { new TimeOnly(12,40), new HashSet<Guid>(doctors) },
            };

            // Act
            var response = await _getTimeSlotsQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            response.TimeSlots.Should().BeEquivalentTo(expectedResponse);
            _appointmentsRepositoryMock.Verify(x => x.GetAppointmentsAsync(request.Date, request.Doctors), Times.Once);
        }
    }
}

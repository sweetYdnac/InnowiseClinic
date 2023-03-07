﻿using Appointments.Write.Application.Interfaces.Services;
using MassTransit;
using Shared.Messages;

namespace Appointments.Write.Persistence.Implementations.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageService(ISendEndpointProvider sendEndpointProvider) =>
            _sendEndpointProvider = sendEndpointProvider;

        public async Task SendCreateAppointmentMessageAsync(CreateAppointmentMessage message) =>
            await _sendEndpointProvider.Send(message);

        public async Task SendRescheduleAppointmentMessageAsync(RescheduleAppointmentMessage message) =>
            await _sendEndpointProvider.Send(message);
    }
}

using Documents.Business.Interfaces;
using MassTransit;
using Shared.Messages;

namespace Documents.API.Consumers.Photo
{
    public class DeletePhotoConsumer : IConsumer<DeletePhotoMessage>
    {
        private readonly IPhotoService _photoService;

        public DeletePhotoConsumer(IPhotoService photoService) => _photoService = photoService;

        public async Task Consume(ConsumeContext<DeletePhotoMessage> context)
        {
            await _photoService.DeleteAsync(context.Message.PhotoId);
        }
    }
}

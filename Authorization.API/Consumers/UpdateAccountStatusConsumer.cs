using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using AutoMapper;
using MassTransit;
using Shared.Messages;

namespace Authorization.API.Consumers
{
    public class UpdateAccountStatusConsumer : IConsumer<UpdateAccountStatusMessage>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UpdateAccountStatusConsumer(IAccountService accountService, IMapper mapper) =>
            (_accountService, _mapper) = (accountService, mapper);

        public async Task Consume(ConsumeContext<UpdateAccountStatusMessage> context)
        {
            await _accountService.UpdateAsync(context.Message.AccountId, _mapper.Map<PatchAccountDTO>(context.Message));
        }
    }
}

using Authorization.Business.Abstractions;
using Authorization.Data.DataTransferObjects;
using AutoMapper;
using MassTransit;
using Shared.Models.Messages;

namespace Authorization.API.Consumers
{
    public class StatusUpdatedConsumer : IConsumer<AccountStatusUpdated>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public StatusUpdatedConsumer(IAccountService accountService, IMapper mapper) => 
            (_accountService, _mapper) = (accountService, mapper);

        public async Task Consume(ConsumeContext<AccountStatusUpdated> context)
        {
            await _accountService.UpdateAsync(context.Message.AccountId, _mapper.Map<PatchAccountDTO>(context.Message));
        }
    }
}

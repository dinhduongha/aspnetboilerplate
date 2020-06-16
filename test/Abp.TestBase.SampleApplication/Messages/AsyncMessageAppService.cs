using System;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;

namespace Abp.TestBase.SampleApplication.Messages
{
    [AbpAuthorize("AsyncMessageAppService_Permission")]
    public class AsyncMessageAppService : AsyncCrudAppService<Message, MessageDto, Guid>, IAsyncMessageAppService
    {
        public AsyncMessageAppService(IRepository<Message, Guid> repository)
            : base(repository)
        {

        }
    }
}
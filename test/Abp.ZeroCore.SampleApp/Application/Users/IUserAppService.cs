using Abp.Application.Services;
using System;

namespace Abp.ZeroCore.SampleApp.Application.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, Guid>
    {
        
    }
}

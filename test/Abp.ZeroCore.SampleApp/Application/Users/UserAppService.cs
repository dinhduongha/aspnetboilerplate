using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ZeroCore.SampleApp.Core;
using System;

namespace Abp.ZeroCore.SampleApp.Application.Users
{
    public class UserAppService : AsyncCrudAppService<User, UserDto, Guid>, IUserAppService
    {
        public UserAppService(IRepository<User, Guid> repository) 
            : base(repository)
        {
            
        }
    }
}
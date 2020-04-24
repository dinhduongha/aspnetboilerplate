using Microsoft.AspNet.Identity;
using System;

namespace Abp.Authorization.Users
{
    public interface IUserTokenProviderAccessor
    {
        IUserTokenProvider<TUser, Guid> GetUserTokenProviderOrNull<TUser>() 
            where TUser : AbpUser<TUser>;
    }
}
using Abp.Dependency;
using Microsoft.AspNet.Identity;
using System;

namespace Abp.Authorization.Users
{
    public class NullUserTokenProviderAccessor : IUserTokenProviderAccessor, ISingletonDependency
    {
        public IUserTokenProvider<TUser, Guid> GetUserTokenProviderOrNull<TUser>() where TUser : AbpUser<TUser>
        {
            return null;
        }
    }
}
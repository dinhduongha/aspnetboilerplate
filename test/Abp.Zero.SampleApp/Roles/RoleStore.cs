using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Zero.SampleApp.Users;
using System;

namespace Abp.Zero.SampleApp.Roles
{
    public class RoleStore : AbpRoleStore<Role, User>
    {
        public RoleStore(
            IRepository<Role, Guid> roleRepository,
            IRepository<UserRole, Guid> userRoleRepository,
            IRepository<RolePermissionSetting, Guid> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}
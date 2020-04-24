using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using Abp.Zero.SampleApp.Users;
using System;

namespace Abp.Zero.SampleApp.Roles
{
    public class RoleManager : AbpRoleManager<Role, User>
    {
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<OrganizationUnit, Guid> organizationUnitRepository,
            IRepository<OrganizationUnitRole, Guid> organizationUnitRoleRepository)
            : base(
            store,
            permissionManager,
            roleManagementConfig,
            cacheManager,
            unitOfWorkManager,
                organizationUnitRepository,
                organizationUnitRoleRepository)
        {
        }
    }
}
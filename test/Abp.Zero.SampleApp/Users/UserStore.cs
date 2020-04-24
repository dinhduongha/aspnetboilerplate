using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Zero.SampleApp.Roles;
using System;

namespace Abp.Zero.SampleApp.Users
{
    public class UserStore : AbpUserStore<Role, User>
    {
        public UserStore(
            IRepository<User, Guid> userRepository,
            IRepository<UserLogin, Guid> userLoginRepository,
            IRepository<UserRole, Guid> userRoleRepository,
            IRepository<Role, Guid> roleRepository,
            IRepository<UserPermissionSetting, Guid> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserClaim, Guid> userClaimRepository,
            IRepository<UserOrganizationUnit, Guid> userOrganizationUnitRepository,
            IRepository<OrganizationUnitRole, Guid> organizationUnitRoleRepository)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager,
                userClaimRepository,
                userOrganizationUnitRepository,
                organizationUnitRoleRepository)
        {

        }
    }
}
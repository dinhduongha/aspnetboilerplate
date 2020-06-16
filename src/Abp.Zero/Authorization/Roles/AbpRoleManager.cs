using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.Zero;
using Abp.Zero.Configuration;
using Microsoft.AspNet.Identity;

namespace Abp.Authorization.Roles
{
    /// <summary>
    /// Extends <see cref="RoleManager{TRole,TKey}"/> of ASP.NET Identity Framework.
    /// Applications should derive this class with appropriate generic arguments.
    /// </summary>
    public abstract class AbpRoleManager<TRole, TUser>
        : RoleManager<TRole, Guid>,
        IDomainService
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {
        public ILocalizationManager LocalizationManager { get; set; }

        protected string LocalizationSourceName { get; set; }

        public IAbpSession AbpSession { get; set; }

        public IRoleManagementConfig RoleManagementConfig { get; private set; }

        public FeatureDependencyContext FeatureDependencyContext { get; set; }

        private IRolePermissionStore<TRole> RolePermissionStore
        {
            get
            {
                if (!(Store is IRolePermissionStore<TRole>))
                {
                    throw new AbpException("Store is not IRolePermissionStore");
                }

                return Store as IRolePermissionStore<TRole>;
            }
        }

        protected AbpRoleStore<TRole, TUser> AbpStore { get; private set; }

        protected IPermissionManager PermissionManager { get; }

        protected ICacheManager CacheManager { get; }

        protected IUnitOfWorkManager UnitOfWorkManager { get; }

        private readonly IRepository<OrganizationUnit, Guid> _organizationUnitRepository;
        private readonly IRepository<OrganizationUnitRole, Guid> _organizationUnitRoleRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AbpRoleManager(
            AbpRoleStore<TRole, TUser> store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<OrganizationUnit, Guid> organizationUnitRepository,
            IRepository<OrganizationUnitRole, Guid> organizationUnitRoleRepository)
            : base(store)
        {
            PermissionManager = permissionManager;
            CacheManager = cacheManager;
            UnitOfWorkManager = unitOfWorkManager;
            _organizationUnitRepository = organizationUnitRepository;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;

            RoleManagementConfig = roleManagementConfig;
            AbpStore = store;
            AbpSession = NullAbpSession.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// </summary>
        /// <param name="roleName">The role's name to check it's permission</param>
        /// <param name="permissionName">Name of the permission</param>
        /// <returns>True, if the role has the permission</returns>
        public virtual async Task<bool> IsGrantedAsync(string roleName, string permissionName)
        {
            return await IsGrantedAsync((await GetRoleByNameAsync(roleName)).Id, PermissionManager.GetPermission(permissionName));
        }

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// </summary>
        /// <param name="roleName">The role's name to check it's permission</param>
        /// <param name="permissionName">Name of the permission</param>
        /// <returns>True, if the role has the permission</returns>
        public virtual bool IsGranted(string roleName, string permissionName)
        {
            return IsGranted((GetRoleByName(roleName)).Id, PermissionManager.GetPermission(permissionName));
        }

        /// <summary>
        /// Checks if a role has a permission.
        /// </summary>
        /// <param name="roleId">The role's id to check it's permission</param>
        /// <param name="permissionName">Name of the permission</param>
        /// <returns>True, if the role has the permission</returns>
        public virtual async Task<bool> IsGrantedAsync(Guid roleId, string permissionName)
        {
            return await IsGrantedAsync(roleId, PermissionManager.GetPermission(permissionName));
        }

        /// <summary>
        /// Checks if a role has a permission.
        /// </summary>
        /// <param name="roleId">The role's id to check it's permission</param>
        /// <param name="permissionName">Name of the permission</param>
        /// <returns>True, if the role has the permission</returns>
        public virtual bool IsGranted(Guid roleId, string permissionName)
        {
            return IsGranted(roleId, PermissionManager.GetPermission(permissionName));
        }

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// </summary>
        /// <param name="role">The role</param>
        /// <param name="permission">The permission</param>
        /// <returns>True, if the role has the permission</returns>
        public Task<bool> IsGrantedAsync(TRole role, Permission permission)
        {
            return IsGrantedAsync(role.Id, permission);
        }

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// </summary>
        /// <param name="role">The role</param>
        /// <param name="permission">The permission</param>
        /// <returns>True, if the role has the permission</returns>
        public bool IsGranted(TRole role, Permission permission)
        {
            return IsGranted(role.Id, permission);
        }

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// </summary>
        /// <param name="roleId">role id</param>
        /// <param name="permission">The permission</param>
        /// <returns>True, if the role has the permission</returns>
        public virtual async Task<bool> IsGrantedAsync(Guid roleId, Permission permission)
        {
            //Get cached role permissions
            var cacheItem = await GetRolePermissionCacheItemAsync(roleId);

            //Check the permission
            return cacheItem.GrantedPermissions.Contains(permission.Name);
        }

        /// <summary>
        /// Checks if a role is granted for a permission.
        /// </summary>
        /// <param name="roleId">role id</param>
        /// <param name="permission">The permission</param>
        /// <returns>True, if the role has the permission</returns>
        public virtual bool IsGranted(Guid roleId, Permission permission)
        {
            //Get cached role permissions
            var cacheItem = GetRolePermissionCacheItem(roleId);

            //Check the permission
            return cacheItem.GrantedPermissions.Contains(permission.Name);
        }

        /// <summary>
        /// Gets granted permission names for a role.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <returns>List of granted permissions</returns>
        public virtual async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(Guid roleId)
        {
            return await GetGrantedPermissionsAsync(await GetRoleByIdAsync(roleId));
        }

        /// <summary>
        /// Gets granted permission names for a role.
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>List of granted permissions</returns>
        public virtual async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(string roleName)
        {
            return await GetGrantedPermissionsAsync(await GetRoleByNameAsync(roleName));
        }

        /// <summary>
        /// Gets granted permissions for a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <returns>List of granted permissions</returns>
        public virtual async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(TRole role)
        {
            var permissionList = new List<Permission>();

            foreach (var permission in PermissionManager.GetAllPermissions())
            {
                if (await IsGrantedAsync(role.Id, permission))
                {
                    permissionList.Add(permission);
                }
            }

            return permissionList;
        }

        /// <summary>
        /// Sets all granted permissions of a role at once.
        /// Prohibits all other permissions.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="permissions">Permissions</param>
        public virtual async Task SetGrantedPermissionsAsync(Guid roleId, IEnumerable<Permission> permissions)
        {
            await SetGrantedPermissionsAsync(await GetRoleByIdAsync(roleId), permissions);
        }

        /// <summary>
        /// Sets all granted permissions of a role at once.
        /// Prohibits all other permissions.
        /// </summary>
        /// <param name="role">The role</param>
        /// <param name="permissions">Permissions</param>
        public virtual async Task SetGrantedPermissionsAsync(TRole role, IEnumerable<Permission> permissions)
        {
            var oldPermissions = await GetGrantedPermissionsAsync(role);
            var newPermissions = permissions.ToArray();

            foreach (var permission in oldPermissions.Where(p => !newPermissions.Contains(p, PermissionEqualityComparer.Instance)))
            {
                await ProhibitPermissionAsync(role, permission);
            }

            foreach (var permission in newPermissions.Where(p => !oldPermissions.Contains(p, PermissionEqualityComparer.Instance)))
            {
                await GrantPermissionAsync(role, permission);
            }
        }

        /// <summary>
        /// Grants a permission for a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <param name="permission">Permission</param>
        public async Task GrantPermissionAsync(TRole role, Permission permission)
        {
            if (await IsGrantedAsync(role.Id, permission))
            {
                return;
            }

            await RolePermissionStore.RemovePermissionAsync(role, new PermissionGrantInfo(permission.Name, false));
            await RolePermissionStore.AddPermissionAsync(role, new PermissionGrantInfo(permission.Name, true));
        }

        /// <summary>
        /// Prohibits a permission for a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <param name="permission">Permission</param>
        public async Task ProhibitPermissionAsync(TRole role, Permission permission)
        {
            if (!await IsGrantedAsync(role.Id, permission))
            {
                return;
            }

            await RolePermissionStore.RemovePermissionAsync(role, new PermissionGrantInfo(permission.Name, true));
            await RolePermissionStore.AddPermissionAsync(role, new PermissionGrantInfo(permission.Name, false));
        }

        /// <summary>
        /// Prohibits all permissions for a role.
        /// </summary>
        /// <param name="role">Role</param>
        public async Task ProhibitAllPermissionsAsync(TRole role)
        {
            foreach (var permission in PermissionManager.GetAllPermissions())
            {
                await ProhibitPermissionAsync(role, permission);
            }
        }

        /// <summary>
        /// Resets all permission settings for a role.
        /// It removes all permission settings for the role.
        /// </summary>
        /// <param name="role">Role</param>
        public async Task ResetAllPermissionsAsync(TRole role)
        {
            await RolePermissionStore.RemoveAllPermissionSettingsAsync(role);
        }

        /// <summary>
        /// Creates a role.
        /// </summary>
        /// <param name="role">Role</param>
        public override async Task<IdentityResult> CreateAsync(TRole role)
        {
            role.SetNormalizedName();

            var result = await CheckDuplicateRoleNameAsync(role.Id, role.Name, role.DisplayName);
            if (!result.Succeeded)
            {
                return result;
            }

            var tenantId = GetCurrentTenantId();
            if (tenantId.HasValue && !role.TenantId.HasValue)
            {
                role.TenantId = tenantId.Value;
            }

            return await base.CreateAsync(role);
        }

        public override async Task<IdentityResult> UpdateAsync(TRole role)
        {
            role.SetNormalizedName();

            var result = await CheckDuplicateRoleNameAsync(role.Id, role.Name, role.DisplayName);
            if (!result.Succeeded)
            {
                return result;
            }

            return await base.UpdateAsync(role);
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="role">Role</param>
        public async override Task<IdentityResult> DeleteAsync(TRole role)
        {
            if (role.IsStatic)
            {
                return AbpIdentityResult.Failed(string.Format(L("CanNotDeleteStaticRole"), role.Name));
            }

            return await base.DeleteAsync(role);
        }

        /// <summary>
        /// Gets a role by given id.
        /// Throws exception if no role with given id.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <returns>Role</returns>
        /// <exception cref="AbpException">Throws exception if no role with given id</exception>
        public virtual async Task<TRole> GetRoleByIdAsync(Guid roleId)
        {
            var role = await FindByIdAsync(roleId);
            if (role == null)
            {
                throw new AbpException("There is no role with id: " + roleId);
            }

            return role;
        }

        /// <summary>
        /// Gets a role by given name.
        /// Throws exception if no role with given roleName.
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>Role</returns>
        /// <exception cref="AbpException">Throws exception if no role with given roleName</exception>
        public virtual async Task<TRole> GetRoleByNameAsync(string roleName)
        {
            var role = await FindByNameAsync(roleName);
            if (role == null)
            {
                throw new AbpException("There is no role with name: " + roleName);
            }

            return role;
        }

        /// <summary>
        /// Gets a role by given name.
        /// Throws exception if no role with given roleName.
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>Role</returns>
        /// <exception cref="AbpException">Throws exception if no role with given roleName</exception>
        public virtual TRole GetRoleByName(string roleName)
        {
            var role = AbpStore.FindByName(roleName);
            if (role == null)
            {
                throw new AbpException("There is no role with name: " + roleName);
            }

            return role;
        }

        public async Task GrantAllPermissionsAsync(TRole role)
        {
            FeatureDependencyContext.TenantId = role.TenantId;

            var permissions = PermissionManager.GetAllPermissions(role.GetMultiTenancySide())
                                                .Where(permission =>
                                                    permission.FeatureDependency == null ||
                                                    permission.FeatureDependency.IsSatisfied(FeatureDependencyContext)
                                                );

            await SetGrantedPermissionsAsync(role, permissions);
        }

        [UnitOfWork]
        public virtual async Task<IdentityResult> CreateStaticRoles(Guid tenantId)
        {
            var staticRoleDefinitions = RoleManagementConfig.StaticRoles.Where(sr => sr.Side == MultiTenancySides.Tenant);

            using (UnitOfWorkManager.Current.SetTenantId(tenantId))
            {
                foreach (var staticRoleDefinition in staticRoleDefinitions)
                {
                    var role = new TRole
                    {
                        TenantId = tenantId,
                        Name = staticRoleDefinition.RoleName,
                        DisplayName = staticRoleDefinition.RoleDisplayName,
                        IsStatic = true
                    };

                    role.SetNormalizedName();

                    var identityResult = await CreateAsync(role);
                    if (!identityResult.Succeeded)
                    {
                        return identityResult;
                    }
                }
            }

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> CheckDuplicateRoleNameAsync(Guid? expectedRoleId, string name, string displayName)
        {
            var role = await FindByNameAsync(name);
            if (role != null && role.Id != expectedRoleId)
            {
                return AbpIdentityResult.Failed(string.Format(L("RoleNameIsAlreadyTaken"), name));
            }

            role = await FindByDisplayNameAsync(displayName);
            if (role != null && role.Id != expectedRoleId)
            {
                return AbpIdentityResult.Failed(string.Format(L("RoleDisplayNameIsAlreadyTaken"), displayName));
            }

            return IdentityResult.Success;
        }

        [UnitOfWork]
        public virtual Task<List<TRole>> GetRolesInOrganizationUnit(OrganizationUnit organizationUnit, bool includeChildren = false)
        {
            if (!includeChildren)
            {
                var query = from uor in _organizationUnitRoleRepository.GetAll()
                            join role in Roles on uor.RoleId equals role.Id
                            where uor.OrganizationUnitId == organizationUnit.Id
                            select role;

                return Task.FromResult(query.ToList());
            }
            else
            {
                var query = from uor in _organizationUnitRoleRepository.GetAll()
                            join role in Roles on uor.RoleId equals role.Id
                            join ou in _organizationUnitRepository.GetAll() on uor.OrganizationUnitId equals ou.Id
                            where ou.Code.StartsWith(organizationUnit.Code)
                            select role;

                return Task.FromResult(query.ToList());
            }
        }

        public virtual async Task SetOrganizationUnitsAsync(Guid roleId, params Guid[] organizationUnitIds)
        {
            await SetOrganizationUnitsAsync(
                await GetRoleByIdAsync(roleId),
                organizationUnitIds
            );
        }

        public virtual async Task SetOrganizationUnitsAsync(TRole role, params Guid[] organizationUnitIds)
        {
            if (organizationUnitIds == null)
            {
                organizationUnitIds = new Guid[0];
            }

            var currentOus = await GetOrganizationUnitsAsync(role);

            //Remove from removed OUs
            foreach (var currentOu in currentOus)
            {
                if (!organizationUnitIds.Contains(currentOu.Id))
                {
                    await RemoveFromOrganizationUnitAsync(role, currentOu);
                }
            }

            //Add to added OUs
            foreach (var organizationUnitId in organizationUnitIds)
            {
                if (currentOus.All(ou => ou.Id != organizationUnitId))
                {
                    await AddToOrganizationUnitAsync(
                        role,
                        await _organizationUnitRepository.GetAsync(organizationUnitId)
                    );
                }
            }
        }

        public virtual async Task<bool> IsInOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            return await IsInOrganizationUnitAsync(
                await GetRoleByIdAsync(roleId),
                await _organizationUnitRepository.GetAsync(ouId)
            );
        }

        public virtual async Task<bool> IsInOrganizationUnitAsync(TRole role, OrganizationUnit ou)
        {
            return await _organizationUnitRoleRepository.CountAsync(uou =>
                       uou.RoleId == role.Id && uou.OrganizationUnitId == ou.Id
                   ) > 0;
        }

        public virtual async Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            await AddToOrganizationUnitAsync(
                await GetRoleByIdAsync(roleId),
                await _organizationUnitRepository.GetAsync(ouId)
            );
        }

        public virtual async Task AddToOrganizationUnitAsync(TRole role, OrganizationUnit ou)
        {
            if (await IsInOrganizationUnitAsync(role, ou))
            {
                return;
            }

            await _organizationUnitRoleRepository.InsertAsync(new OrganizationUnitRole(role.TenantId, role.Id, ou.Id));
        }

        public async Task RemoveFromOrganizationUnitAsync(Guid roleId, Guid organizationUnitId)
        {
            await RemoveFromOrganizationUnitAsync(
                await GetRoleByIdAsync(roleId),
                await _organizationUnitRepository.GetAsync(organizationUnitId)
            );
        }

        public virtual async Task RemoveFromOrganizationUnitAsync(TRole role, OrganizationUnit ou)
        {
            await _organizationUnitRoleRepository.DeleteAsync(uor => uor.RoleId == role.Id && uor.OrganizationUnitId == ou.Id);
        }

        [UnitOfWork]
        public virtual Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(TRole role)
        {
            var query = from uor in _organizationUnitRoleRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uor.OrganizationUnitId equals ou.Id
                        where uor.RoleId == role.Id
                        select ou;

            return Task.FromResult(query.ToList());
        }

        private Task<TRole> FindByDisplayNameAsync(string displayName)
        {
            return AbpStore.FindByDisplayNameAsync(displayName);
        }

        private async Task<RolePermissionCacheItem> GetRolePermissionCacheItemAsync(Guid roleId)
        {
            //var cacheKey = roleId + "@" + (GetCurrentTenantId() ?? 0);
            var cacheKey = $"{roleId}@{GetCurrentTenantId()}";
            return await CacheManager.GetRolePermissionCache().GetAsync(cacheKey, async () =>
            {
                var newCacheItem = new RolePermissionCacheItem(roleId);

                var role = await Store.FindByIdAsync(roleId);
                if (role == null)
                {
                    throw new AbpException("There is no role with given id: " + roleId);
                }

                var staticRoleDefinition = RoleManagementConfig.StaticRoles.FirstOrDefault(r =>
                    r.RoleName == role.Name && r.Side == role.GetMultiTenancySide());

                if (staticRoleDefinition != null)
                {
                    foreach (var permission in PermissionManager.GetAllPermissions())
                    {
                        if (staticRoleDefinition.IsGrantedByDefault(permission))
                        {
                            newCacheItem.GrantedPermissions.Add(permission.Name);
                        }
                    }
                }

                foreach (var permissionInfo in await RolePermissionStore.GetPermissionsAsync(roleId))
                {
                    if (permissionInfo.IsGranted)
                    {
                        newCacheItem.GrantedPermissions.AddIfNotContains(permissionInfo.Name);
                    }
                    else
                    {
                        newCacheItem.GrantedPermissions.Remove(permissionInfo.Name);
                    }
                }

                return newCacheItem;
            });
        }

        private RolePermissionCacheItem GetRolePermissionCacheItem(Guid roleId)
        {
            //var cacheKey = roleId + "@" + (GetCurrentTenantId() ?? 0);
            var cacheKey = $"{roleId}@{GetCurrentTenantId()}";
            return CacheManager.GetRolePermissionCache().Get(cacheKey, () =>
            {
                var newCacheItem = new RolePermissionCacheItem(roleId);

                var role = AbpStore.FindById(roleId);
                if (role == null)
                {
                    throw new AbpException("There is no role with given id: " + roleId);
                }

                var staticRoleDefinition = RoleManagementConfig.StaticRoles.FirstOrDefault(r =>
                    r.RoleName == role.Name && r.Side == role.GetMultiTenancySide());

                if (staticRoleDefinition != null)
                {
                    foreach (var permission in PermissionManager.GetAllPermissions())
                    {
                        if (staticRoleDefinition.IsGrantedByDefault(permission))
                        {
                            newCacheItem.GrantedPermissions.Add(permission.Name);
                        }
                    }
                }

                foreach (var permissionInfo in RolePermissionStore.GetPermissions(roleId))
                {
                    if (permissionInfo.IsGranted)
                    {
                        newCacheItem.GrantedPermissions.AddIfNotContains(permissionInfo.Name);
                    }
                    else
                    {
                        newCacheItem.GrantedPermissions.Remove(permissionInfo.Name);
                    }
                }

                return newCacheItem;
            });
        }

        protected virtual string L(string name)
        {
            return LocalizationManager.GetString(LocalizationSourceName, name);
        }

        protected virtual string L(string name, CultureInfo cultureInfo)
        {
            return LocalizationManager.GetString(LocalizationSourceName, name, cultureInfo);
        }

        private Guid? GetCurrentTenantId()
        {
            if (UnitOfWorkManager.Current != null)
            {
                return UnitOfWorkManager.Current.GetTenantId();
            }

            return AbpSession.TenantId;
        }
    }
}
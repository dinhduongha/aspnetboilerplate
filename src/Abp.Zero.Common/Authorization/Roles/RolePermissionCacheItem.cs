using System;
using System.Collections.Generic;

namespace Abp.Authorization.Roles
{
    /// <summary>
    /// Used to cache permissions of a role.
    /// </summary>
    [Serializable]
    public class RolePermissionCacheItem
    {
        public const string CacheStoreName = "AbpZeroRolePermissions";

        public Guid RoleId { get; set; }

        public HashSet<string> GrantedPermissions { get; set; }

        public RolePermissionCacheItem()
        {
            GrantedPermissions = new HashSet<string>();
        }

        public RolePermissionCacheItem(Guid roleId)
            : this()
        {
            RoleId = roleId;
        }
    }
}
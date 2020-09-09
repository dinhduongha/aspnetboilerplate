using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Represents role record of a user. 
    /// </summary>
    [Table("AbpUserRoles")]
    public class UserRole : CreationAuditedEntity<long>, IMayHaveTenant
    {
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// User id.
        /// </summary>
        public virtual Guid UserId { get; set; }

        /// <summary>
        /// Role id.
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// </summary>
        public UserRole()
        {
            /// TODO: DinhHa cause run test case failure.
            //Id = SequentialGuidGenerator.Instance.Create();
        }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// </summary>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="userId">User id</param>
        /// <param name="roleId">Role id</param>
        public UserRole(Guid? tenantId, Guid userId, int roleId)
        {
            //Id = SequentialGuidGenerator.Instance.Create();
            TenantId = tenantId;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
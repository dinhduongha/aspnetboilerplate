using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Abp.Organizations
{
    /// <summary>
    /// Represents membership of a User to an OU.
    /// </summary>
    [Table("AbpOrganizationUnitRoles")]
    public class OrganizationUnitRole : CreationAuditedEntity<Guid>, IMayHaveTenant, ISoftDelete
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        /// <summary>
        /// Id of the Role.
        /// </summary>
        public virtual Guid RoleId { get; set; }

        /// <summary>
        /// Id of the <see cref="OrganizationUnit"/>.
        /// </summary>
        public virtual Guid OrganizationUnitId { get; set; }

        /// <summary>
        /// Specifies if the organization is soft deleted or not.
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationUnitRole"/> class.
        /// </summary>
        public OrganizationUnitRole()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationUnitRole"/> class.
        /// </summary>
        /// <param name="tenantId">TenantId</param>
        /// <param name="roleId">Id of the User.</param>
        /// <param name="organizationUnitId">Id of the <see cref="OrganizationUnit"/>.</param>
        public OrganizationUnitRole(Guid? tenantId, Guid roleId, Guid organizationUnitId)
        {
            Id = SequentialGuidGenerator.Instance.Create();
            TenantId = tenantId;
            RoleId = roleId;
            OrganizationUnitId = organizationUnitId;
        }
    }
}

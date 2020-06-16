using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Abp.Authorization.Roles
{
    [Table("AbpRoleClaims")]
    public class RoleClaim : CreationAuditedEntity<Guid>, IMayHaveTenant
    {
        /// <summary>
        /// Maximum length of the <see cref="ClaimType"/> property.
        /// </summary>
        public const int MaxClaimTypeLength = 256;

        public virtual Guid? TenantId { get; set; }

        public virtual Guid RoleId { get; set; }

        [StringLength(MaxClaimTypeLength)]
        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }

        public RoleClaim()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public RoleClaim(AbpRoleBase role, Claim claim)
        {
            Id = SequentialGuidGenerator.Instance.Create();
            TenantId = role.TenantId;
            RoleId = role.Id;
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}

using Abp.NHibernate.EntityMappings;
using Abp.Organizations;
using System;

namespace Abp.Zero.NHibernate.EntityMappings
{
    public class OrganizationUnitRoleMap : EntityMap<OrganizationUnitRole, Guid>
    {
        public OrganizationUnitRoleMap()
            : base("AbpOrganizationUnitRoles")
        {
            Map(x => x.TenantId);
            Map(x => x.RoleId);
            Map(x => x.OrganizationUnitId);

            this.MapCreationAudited();
        }
    }
}
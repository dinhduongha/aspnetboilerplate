using System;
using Abp.Authorization.Roles;

namespace Abp.ZeroCore.SampleApp.Core
{
    public class Role : AbpRole<User>
    {
        public Role()
        {

        }

        public Role(Guid? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}
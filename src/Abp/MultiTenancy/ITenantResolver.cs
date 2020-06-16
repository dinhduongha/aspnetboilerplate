using System;

namespace Abp.MultiTenancy
{
    public interface ITenantResolver
    {
        Guid? ResolveTenantId();
    }
}
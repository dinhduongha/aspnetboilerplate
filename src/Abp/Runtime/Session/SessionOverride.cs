using System;

namespace Abp.Runtime.Session
{
    public class SessionOverride
    {
        public Guid? UserId { get; }

        public int? TenantId { get; }

        public SessionOverride(int? tenantId, Guid? userId)
        {
            TenantId = tenantId;
            UserId = userId;
        }
    }
}
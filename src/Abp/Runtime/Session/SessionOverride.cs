namespace Abp.Runtime.Session
{
    public class SessionOverride
    {
        public long? UserId { get; }

        public long? TenantId { get; }

        public SessionOverride(long? tenantId, long? userId)
        {
            TenantId = tenantId;
            UserId = userId;
        }
    }
}
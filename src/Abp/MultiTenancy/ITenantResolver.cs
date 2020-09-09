namespace Abp.MultiTenancy
{
    public interface ITenantResolver
    {
        long? ResolveTenantId();
    }
}
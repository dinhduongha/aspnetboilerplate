namespace Abp.MultiTenancy
{
    public interface ITenantResolveContributor
    {
        long? ResolveTenantId();
    }
}
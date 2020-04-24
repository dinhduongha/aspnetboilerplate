using Abp.Configuration.Startup;
using Abp.Runtime.Session;
using Shouldly;
using System;
using Xunit;

namespace Abp.TestBase.Tests.Runtime.Session
{
    public class SessionTests : AbpIntegratedTestBase<AbpKernelModule>
    {
        [Fact]
        public void Should_Be_Default_On_Startup()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = false;

            AbpSession.UserId.ShouldBe(null);
            AbpSession.TenantId.ShouldBe(1);

            Resolve<IMultiTenancyConfig>().IsEnabled = true;

            AbpSession.UserId.ShouldBe(null);
            AbpSession.TenantId.ShouldBe(null);
        }

        [Fact]
        public void Can_Change_Session_Variables()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;

            AbpSession.UserId = new Guid("0171ac9e-a5ec-0851-09c7-7a53338a7a00");
            AbpSession.TenantId = 42;

            var resolvedAbpSession = LocalIocManager.Resolve<IAbpSession>();

            resolvedAbpSession.UserId.ShouldBe(new Guid("0171ac9e-a5ec-0851-09c7-7a53338a7a00"));
            resolvedAbpSession.TenantId.ShouldBe(42);

            Resolve<IMultiTenancyConfig>().IsEnabled = false;

            AbpSession.UserId.ShouldBe(new Guid("0171ac9e-a5ec-0851-09c7-7a53338a7a00"));
            AbpSession.TenantId.ShouldBe(1);
        }
    }
}

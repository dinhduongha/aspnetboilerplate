using System;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Abp.Zero.SampleApp.MultiTenancy;
using Shouldly;
using Xunit;

namespace Abp.Zero.SampleApp.Tests.MultiTenancy
{
    public class TenantCache_Tests : SampleAppTestBase
    {
        private readonly ITenantCache _tenantCache;
        private readonly IRepository<Tenant, Guid> _tenantRepository;

        public TenantCache_Tests()
        {
            _tenantCache = Resolve<ITenantCache>();
            _tenantRepository = Resolve<IRepository<Tenant, Guid>>();
        }

        [Fact]
        public void Should_Get_Tenant_By_Id()
        {
            //Act
            var tenant = _tenantCache.Get(new Guid("00000000-0000-0000-0000-000000000001"));

            //Assert
            tenant.TenancyName.ShouldBe(Tenant.DefaultTenantName);
        }
        
        [Fact]
        public void Should_Get_Tenant_By_TenancyName()
        {
            //Act
            var tenant = _tenantCache.GetOrNull(Tenant.DefaultTenantName);

            //Assert
            tenant.Id.ShouldBe(new Guid("00000000-0000-0000-0000-000000000001"));
        }

        [Fact]
        public void Should_Get_Null_Tenant_By_TenancyName_If_Not_Found()
        {
            //Act
            var tenant = _tenantCache.GetOrNull("unknown-tenancy-name");

            //Assert
            tenant.ShouldBeNull();
        }
        
        [Fact]
        public void Should_Refresh_Cache_If_Tenancy_Name_Changes()
        {
            //--- Get a known tenant from cache

            //Act
            var tenant = _tenantCache.GetOrNull(Tenant.DefaultTenantName);

            //Assert
            tenant.Id.ShouldBe(new Guid("00000000-0000-0000-0000-000000000001"));
            tenant.IsActive.ShouldBeTrue();

            //--- Change tenant name

            _tenantRepository.Update(tenant.Id, t =>
            {
                t.TenancyName = "Default-Changed";
                t.IsActive = false;
            });

            //--- Get again from cache

            //Can not get with old name
            tenant = _tenantCache.GetOrNull(Tenant.DefaultTenantName);
            tenant.ShouldBeNull();

            //Can get with new name
            tenant = _tenantCache.GetOrNull("Default-Changed");
            tenant.ShouldNotBeNull();
            tenant.Id.ShouldBe(new Guid("00000000-0000-0000-0000-000000000001"));
            tenant.IsActive.ShouldBeFalse();
        }
    }
}
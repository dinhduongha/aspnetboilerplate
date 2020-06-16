using System;
using System.Threading.Tasks;
using Abp.ZeroCore.SampleApp.Core;
using Shouldly;
using Xunit;

namespace Abp.Zero.Users
{
    public class UserManager_Options_Tests : AbpZeroTestBase
    {
        [Fact]
        public async Task InitializeOptionsAsync_Should_Not_Reset_Default_Options()
        {
            var userManager = LocalIocManager.Resolve<UserManager>();
            userManager.Options.Tokens.ProviderMap.Count.ShouldBeGreaterThan(0);
            
            await userManager.InitializeOptionsAsync(new Guid("00000000-0000-0000-0000-000000000001"));
            userManager.Options.Tokens.ProviderMap.Count.ShouldBeGreaterThan(0);
        }
    }
}
using Shouldly;
using Xunit;

namespace Abp.Tests
{
    public class UserIdentifier_Tests
    {
        [Fact]
        public void GetHashCode_Test()
        {
            UserIdentifier.Parse("00000000-0000-0000-0000-000000000001@00000000-0000-0000-0000-000000000004").GetHashCode().ShouldBe(UserIdentifier.Parse("00000000-0000-0000-0000-000000000001@00000000-0000-0000-0000-000000000004").GetHashCode());

            //UserIdentifier.Parse("0171acb7-487d-0761-04e6-3c88f24409c0@1").GetHashCode().ShouldNotBe(UserIdentifier.Parse("0171ad2f-f018-13e1-0a8c-782a576ba100@2").GetHashCode());

            //UserIdentifier.Parse("0171acb7-487d-0761-04e6-3c88f24409c0@0").GetHashCode().ShouldNotBe(UserIdentifier.Parse("0@0171acb7-487d-0761-04e6-3c88f24409c0").GetHashCode());

            //UserIdentifier.Parse("0171acb7-487d-0761-04e6-3c88f24409c0@2").GetHashCode().ShouldNotBe(UserIdentifier.Parse("2@0171acb7-487d-0761-04e6-3c88f24409c0").GetHashCode());
        }
    }
}
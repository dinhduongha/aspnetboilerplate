using System;
using System.Transactions;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Shouldly;
using Xunit;

namespace Abp.TestBase.SampleApplication.Tests.Uow
{
    public class UnitOfWork_Nested_Tests : SampleApplicationTestBase
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWork_Nested_Tests()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();
        }

        [Fact]
        public void Should_Copy_Filters_To_Nested_Uow()
        {
            AbpSession.TenantId.ShouldBe(null);

            using (var outerUow = _unitOfWorkManager.Begin())
            {
                _unitOfWorkManager.Current.GetTenantId().ShouldBe(null);

                using (_unitOfWorkManager.Current.SetTenantId(new Guid("00000000-0000-0000-0000-000000000001")))
                {
                    _unitOfWorkManager.Current.GetTenantId().ShouldBe(new Guid("00000000-0000-0000-0000-000000000001"));

                    using (var nestedUow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
                    {
                        AbpSession.TenantId.ShouldBe(null);
                        _unitOfWorkManager.Current.GetTenantId().ShouldBe(new Guid("00000000-0000-0000-0000-000000000001")); //Because nested transaction copies outer uow's filters.

                        nestedUow.Complete();
                    }

                    _unitOfWorkManager.Current.GetTenantId().ShouldBe(new Guid("00000000-0000-0000-0000-000000000001"));
                }

                _unitOfWorkManager.Current.GetTenantId().ShouldBe(null);

                outerUow.Complete();
            }
        }
    }
}

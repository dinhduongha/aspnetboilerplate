using System;
using System.Linq;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.TestBase.SampleApplication.Messages;
using Abp.TestBase.SampleApplication.Tests.People;
using Abp.Timing;
using Shouldly;
using Xunit;

namespace Abp.TestBase.SampleApplication.Tests.ContactLists
{
    public class Messages_MultiTenancy_Tests : SampleApplicationTestBase
    {
        private readonly IRepository<Message, Guid> _messageRepository;

        public Messages_MultiTenancy_Tests()
        {
            _messageRepository = Resolve<IRepository<Message, Guid>>();
        }

        protected override void CreateInitialData()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;
            base.CreateInitialData();
        }

        [Fact]
        public void EntityAuditProperty_Tests_Cross_Tenant_User()
        {
            AbpSession.TenantId = null;
            AbpSession.UserId = new Guid("00000000-0000-0000-0000-000000000999");

            Message tenant1MessageNew;

            //Can not get tenant's data from host user
            var tenant1Message1 = _messageRepository.FirstOrDefault(m => m.Text == "tenant-1-message-1");
            tenant1Message1.ShouldBeNull();

            var unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                //Should start UOW with TenantId in the session
                unitOfWorkManager.Current.GetTenantId().ShouldBeNull();

                using (unitOfWorkManager.Current.SetTenantId(new Guid("00000000-0000-0000-0000-000000000001")))
                {
                    unitOfWorkManager.Current.GetTenantId().ShouldBe(new Guid("00000000-0000-0000-0000-000000000001"));

                    tenant1Message1 = _messageRepository.FirstOrDefault(m => m.Text == "tenant-1-message-1");
                    tenant1Message1.ShouldNotBeNull(); //Can get tenant's data from host since we used SetTenantId()
                    tenant1Message1.LastModifierUserId.ShouldBeNull();
                    tenant1Message1.LastModificationTime.ShouldBeNull();

                    tenant1Message1.Text = "tenant-1-message-1-modified";

                    var tenant1Message2 = _messageRepository.Single(m => m.Text == "tenant-1-message-2");
                    _messageRepository.Delete(tenant1Message2);

                    tenant1MessageNew = _messageRepository.Insert(new Message(new Guid("00000000-0000-0000-0000-000000000001"), "tenant-1-message-new"));
                }

                unitOfWork.Complete();
            }

            //Creation audit check
            tenant1MessageNew.IsTransient().ShouldBeFalse(); //It should be saved to database
            tenant1MessageNew.CreationTime.ShouldBeGreaterThan(Clock.Now.Subtract(TimeSpan.FromMinutes(1)));
            tenant1MessageNew.CreatorUserId.ShouldBeNull(); //It's not set since user in the AbpSession is not that tenant's user!

            //Modification audit check
            tenant1Message1.LastModificationTime.ShouldNotBeNull(); //It's set since we modified Text
            tenant1Message1.LastModifierUserId.ShouldBeNull(); //It's not set since user in the AbpSession is not that tenant's user!

            //Deletion audit check
            UsingDbContext(context =>
            {
                var tenant1Message2 = context.Messages.FirstOrDefault(m => m.Text == "tenant-1-message-2");
                tenant1Message2.ShouldNotBeNull();
                tenant1Message2.IsDeleted.ShouldBeTrue();
                tenant1Message2.DeletionTime.ShouldNotBeNull();
                tenant1Message2.DeleterUserId.ShouldBeNull(); //It's not set since user in the AbpSession is not that tenant's user!
            });
        }

        [Fact]
        public void EntityAuditProperty_Tests_Same_Tenant_User()
        {
            AbpSession.TenantId = new Guid("00000000-0000-0000-0000-000000000001");
            AbpSession.UserId = new Guid("00000000-0000-0000-0000-000000000999");

            Message tenant1MessageNew;

            //Can get tenant's data from same tenant user
            var tenant1Message1 = _messageRepository.FirstOrDefault(m => m.Text == "tenant-1-message-1");
            tenant1Message1.ShouldNotBeNull();

            var unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                //Should start UOW with TenantId in the session
                unitOfWorkManager.Current.GetTenantId().ShouldBe(new Guid("00000000-0000-0000-0000-000000000001"));

                tenant1Message1 = _messageRepository.FirstOrDefault(m => m.Text == "tenant-1-message-1");
                tenant1Message1.ShouldNotBeNull();
                tenant1Message1.LastModifierUserId.ShouldBeNull();
                tenant1Message1.LastModificationTime.ShouldBeNull();

                tenant1Message1.Text = "tenant-1-message-1-modified";

                var tenant1Message2 = _messageRepository.Single(m => m.Text == "tenant-1-message-2");
                _messageRepository.Delete(tenant1Message2);

                tenant1MessageNew = _messageRepository.Insert(new Message(new Guid("00000000-0000-0000-0000-000000000001"), "tenant-1-message-new"));

                unitOfWork.Complete();
            }

            //Creation audit check
            tenant1MessageNew.IsTransient().ShouldBeFalse(); //It should be saved to database
            tenant1MessageNew.CreationTime.ShouldBeGreaterThan(Clock.Now.Subtract(TimeSpan.FromMinutes(1)));
            tenant1MessageNew.CreatorUserId.ShouldBe(new Guid("00000000-0000-0000-0000-000000000999")); //It set since user in the AbpSession is tenant's user!

            //Modification audit check
            tenant1Message1.LastModificationTime.ShouldNotBeNull(); //It's set since we modified Text
            tenant1Message1.LastModifierUserId.ShouldBe(new Guid("00000000-0000-0000-0000-000000000999")); //It set since user in the AbpSession is tenant's user!

            //Deletion audit check
            UsingDbContext(context =>
            {
                var tenant1Message2 = context.Messages.FirstOrDefault(m => m.Text == "tenant-1-message-2");
                tenant1Message2.ShouldNotBeNull();
                tenant1Message2.IsDeleted.ShouldBeTrue();
                tenant1Message2.DeletionTime.ShouldNotBeNull();
                tenant1Message2.DeleterUserId.ShouldBe(new Guid("00000000-0000-0000-0000-000000000999")); //It set since user in the AbpSession is tenant's user!
            });
        }

        [Fact]
        public void MayHaveTenant_Filter_Tests()
        {
            AbpSession.UserId = new Guid("00000000-0000-0000-0000-000000000001");

            //A tenant can reach its own data
            AbpSession.TenantId = new Guid("00000000-0000-0000-0000-000000000001");
            _messageRepository.Count().ShouldBe(2);
            _messageRepository.GetAllList().Any(m => m.TenantId != AbpSession.TenantId).ShouldBe(false);

            //Tenant 999999 has no data
            AbpSession.TenantId = new Guid("00000000-0000-0000-0000-000000999999");
            _messageRepository.Count().ShouldBe(0);

            //Host can reach its own data (since MayHaveTenant filter is enabled by default)
            AbpSession.TenantId = null;
            _messageRepository.Count().ShouldBe(1);
            _messageRepository.GetAllList().Any(m => m.TenantId != AbpSession.TenantId).ShouldBe(false);

            var unitOfWorkManager = Resolve<IUnitOfWorkManager>();
            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                unitOfWorkManager.Current.GetTenantId().ShouldBe(null);

                //We can also set tenantId parameter's value without changing AbpSession.TenantId
                using (unitOfWorkManager.Current.SetTenantId(new Guid("00000000-0000-0000-0000-000000000001")))
                {
                    unitOfWorkManager.Current.GetTenantId().ShouldBe(new Guid("00000000-0000-0000-0000-000000000001"));

                    //We should only get tenant 1's entities since we set tenantId to 1
                    _messageRepository.Count().ShouldBe(2);
                    _messageRepository.GetAllList().Any(m => m.TenantId != new Guid("00000000-0000-0000-0000-000000000001")).ShouldBe(false);
                }

                unitOfWorkManager.Current.GetTenantId().ShouldBe(null);

                //We can disable the filter to get all entities of host and tenants
                using (unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    _messageRepository.Count(m => m.TenantId != PersonRepository_Tests_For_EntityChangeEvents.PersonHandler.FakeTenantId).ShouldBe(3);
                }

                unitOfWork.Complete();
            }
        }
    }
}

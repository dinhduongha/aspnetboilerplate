using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.TestBase.SampleApplication.Messages;
using JetBrains.Annotations;
using Shouldly;
using Xunit;

namespace Abp.TestBase.SampleApplication.Tests.Session
{
    public class Session_Tests : SampleApplicationTestBase
    {
        private readonly IAbpSession _session;
        private IRepository<Message, Guid> _messageRepository;
        private IUnitOfWorkManager _unitOfWorkManager;

        public Session_Tests()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;
            _session = Resolve<IAbpSession>();

            _messageRepository = Resolve<IRepository<Message, Guid>>();
            _unitOfWorkManager = Resolve<IUnitOfWorkManager>();
        }

        [Fact]
        public void Session_Override_Test()
        {
            _session.UserId.ShouldBeNull();
            _session.TenantId.ShouldBeNull();

            using (_session.Use(new Guid("00000000-0000-0000-0000-000000000042"), new Guid("00000000-0000-0000-0000-000000000571")))
            {
                _session.TenantId.ShouldBe(new Guid("00000000-0000-0000-0000-000000000042"));
                _session.UserId.ShouldBe(new Guid("00000000-0000-0000-0000-000000000571"));

                using (_session.Use(null, new Guid("00000000-0000-0000-0000-000000000003")))
                {
                    _session.TenantId.ShouldBeNull();
                    _session.UserId.ShouldBe(new Guid("00000000-0000-0000-0000-000000000003"));
                }

                _session.TenantId.ShouldBe(new Guid("00000000-0000-0000-0000-000000000042"));
                _session.UserId.ShouldBe(new Guid("00000000-0000-0000-0000-000000000571"));
            }

            _session.UserId.ShouldBeNull();
            _session.TenantId.ShouldBeNull();
        }

        [Fact]
        public async Task Should_Uow_Set_Tenant_Null_If_Completed_Outside_Session_Use_Using()
        {
            AbpSession.TenantId = new Guid("00000000-0000-0000-0000-000000000001");

            var messageEntity = new Message() { Text = "Test Message" };

            using (var uow = _unitOfWorkManager.Begin())
            {
                using (_session.Use(null, new Guid("00000000-0000-0000-0000-000000000003")))
                {
                    await _messageRepository.InsertAsync(messageEntity);
                }
                await uow.CompleteAsync();
            }

            UsingDbContext(context =>
            {
                var entity = context.Messages.Single(x => x.Id == messageEntity.Id);
                entity.Text.ShouldBe(messageEntity.Text);
                entity.TenantId.ShouldBe(null);
            });
        }

        [Fact]
        public async Task Should_Uow_Set_Tenant_Null_If_Completed_Inside_Session_Use_Using()
        {
            AbpSession.TenantId = new Guid("00000000-0000-0000-0000-000000000001");

            var messageEntity = new Message() { Text = "Test Message" };

            using (var uow = _unitOfWorkManager.Begin())
            {
                using (_session.Use(null, new Guid("00000000-0000-0000-0000-000000000003")))
                {
                    await _messageRepository.InsertAsync(messageEntity);
                    await uow.CompleteAsync();
                }
            }

            UsingDbContext(context =>
            {
                var entity = context.Messages.Single(x => x.Id == messageEntity.Id);
                entity.Text.ShouldBe(messageEntity.Text);
                entity.TenantId.ShouldBe(null);
            });
        }
    }
}

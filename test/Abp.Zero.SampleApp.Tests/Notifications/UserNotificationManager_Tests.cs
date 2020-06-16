using System;
using System.Threading.Tasks;
using Abp.Notifications;
using Shouldly;
using Xunit;

namespace Abp.Zero.SampleApp.Tests.Notifications
{
    public class UserNotificationManager_Tests : SampleAppTestBase
    {
        private readonly IUserNotificationManager _userNotificationManager;

        public UserNotificationManager_Tests()
        {
            _userNotificationManager = Resolve<IUserNotificationManager>();
        }

        [Fact]
        public async Task GetUserNotificationCountAsync_Test()
        {
            var notificationCount = await _userNotificationManager.GetUserNotificationCountAsync(
                new UserIdentifier(new Guid("00000000-0000-0000-0000-000000000001"), new Guid("0171ac9f-3856-1611-0112-2edb41a5dab0")), UserNotificationState.Read
            );

            notificationCount.ShouldBeGreaterThanOrEqualTo(0);
        }
    }
}

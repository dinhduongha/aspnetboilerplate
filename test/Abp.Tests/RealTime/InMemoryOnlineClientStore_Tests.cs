using System;
using Abp.RealTime;
using Shouldly;
using Xunit;

namespace Abp.Tests.RealTime
{
    public class InMemoryOnlineClientStore_Tests
    {
        private readonly InMemoryOnlineClientStore _store;
        private readonly InMemoryOnlineClientStore<ChatChannel> _chatStore;

        public InMemoryOnlineClientStore_Tests()
        {
            _store = new InMemoryOnlineClientStore();
            _chatStore = new InMemoryOnlineClientStore<ChatChannel>();
        }

        [Fact]
        public void Test_All()
        {
            var connectionId = Guid.NewGuid().ToString("N");

            _store.Add(new OnlineClient(connectionId, "127.0.0.1", new Guid("00000000-0000-0000-0000-000000000001"), new Guid("0171ac9f-3856-1611-0112-2edb41a5dab0")));
            _store.TryGet(connectionId, out IOnlineClient client).ShouldBeTrue();

            _store.Contains(connectionId).ShouldBeTrue();
            _store.GetAll().Count.ShouldBe(1);
            _store.Remove(connectionId).ShouldBeTrue();
            _store.GetAll().Count.ShouldBe(0);

            _chatStore.GetAll().Count.ShouldBe(0);
            connectionId = Guid.NewGuid().ToString("N");

            _chatStore.Add(new OnlineClient(connectionId, "127.0.0.1", new Guid("00000000-0000-0000-0000-000000000001"), new Guid("0171ac9f-3856-1611-0112-2edb41a5dab0")));
            _chatStore.GetAll().Count.ShouldBe(1);
        }

        internal class ChatChannel
        {

        }
    }
}

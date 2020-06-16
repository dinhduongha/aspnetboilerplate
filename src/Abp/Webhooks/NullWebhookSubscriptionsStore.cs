using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Webhooks
{
    /// <summary>
    /// Null pattern implementation of <see cref="IWebhookSubscriptionsStore"/>.
    /// It's used if <see cref="IWebhookSubscriptionsStore"/> is not implemented by actual persistent store
    /// </summary>
    public class NullWebhookSubscriptionsStore : IWebhookSubscriptionsStore
    {
        public static NullWebhookSubscriptionsStore Instance { get; } = new NullWebhookSubscriptionsStore();

        public Task<WebhookSubscriptionInfo> GetAsync(Guid id)
        {
            return Task.FromResult<WebhookSubscriptionInfo>(default);
        }

        public WebhookSubscriptionInfo Get(Guid id)
        {
            return default;
        }

        public Task InsertAsync(WebhookSubscriptionInfo webhookSubscription)
        {
            return Task.CompletedTask;
        }

        public void Insert(WebhookSubscriptionInfo webhookSubscription)
        {
        }

        public Task UpdateAsync(WebhookSubscriptionInfo webhookSubscription)
        {
            return Task.CompletedTask;
        }

        public void Update(WebhookSubscriptionInfo webhookSubscription)
        {
        }

        public Task DeleteAsync(Guid id)
        {
            return Task.CompletedTask;
        }

        public void Delete(Guid id)
        {
        }

        public Task<List<WebhookSubscriptionInfo>> GetAllSubscriptionsAsync(Guid? tenantId)
        {
            return Task.FromResult(new List<WebhookSubscriptionInfo>());
        }

        public List<WebhookSubscriptionInfo> GetAllSubscriptions(Guid? tenantId)
        {
            return new List<WebhookSubscriptionInfo>();
        }

        public Task<List<WebhookSubscriptionInfo>> GetAllSubscriptionsAsync(Guid? tenantId, string webhookName)
        {
            return Task.FromResult(new List<WebhookSubscriptionInfo>());
        }

        public List<WebhookSubscriptionInfo> GetAllSubscriptions(Guid? tenantId, string webhookName)
        {
            return new List<WebhookSubscriptionInfo>();
        }

        public Task<bool> IsSubscribedAsync(Guid? tenantId, string webhookName)
        {
            return Task.FromResult(false);
        }

        public bool IsSubscribed(Guid? tenantId, string webhookName)
        {
            return false;
        }
    }
}
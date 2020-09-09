using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Abp.TestBase.SampleApplication.Messages
{
    [Table("Messages")]
    public class Message : FullAuditedEntity, IMayHaveTenant
    {
        public long? TenantId { get; set; }

        public string Text { get; set; }

        public Message()
        {

        }

        public Message(long? tenantId, string text)
        {
            TenantId = tenantId;
            Text = text;
        }
    }
}

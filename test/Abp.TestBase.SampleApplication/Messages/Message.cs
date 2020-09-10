using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Abp.TestBase.SampleApplication.Messages
{
    [Table("Messages")]
    public class Message : FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public Guid? TenantId { get; set; }

        public string Text { get; set; }

        public Message()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public Message(Guid? tenantId, string text)
        {
            TenantId = tenantId;
            Id = SequentialGuidGenerator.Instance.Create();
            Text = text;
        }
    }
}

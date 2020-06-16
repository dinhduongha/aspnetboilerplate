using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Abp.TestBase.SampleApplication.Messages
{
    [AutoMap(typeof(Message))]
    public class MessageDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; set; }

        public string Text { get; set; }
    }
}
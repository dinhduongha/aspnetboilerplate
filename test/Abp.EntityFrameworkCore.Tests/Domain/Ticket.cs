using Abp.Domain.Entities;
using System;

namespace Abp.EntityFrameworkCore.Tests.Domain
{
    public class Ticket : Entity, IPassivable, IMustHaveTenant
    {
        public virtual string EmailAddress { get; set; }

        public virtual string Message { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual Guid TenantId { get; set; }

        public Ticket()
        {
            IsActive = true;
        }
    }
}
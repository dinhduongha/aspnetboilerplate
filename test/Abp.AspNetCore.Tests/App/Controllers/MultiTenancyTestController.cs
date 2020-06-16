using System;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Runtime.Session;

namespace Abp.AspNetCore.App.Controllers
{
    public class MultiTenancyTestController : AbpController
    {
        private readonly IAbpSession _abpSession;

        public MultiTenancyTestController(IAbpSession abpSession)
        {
            _abpSession = abpSession;
        }

        public Guid? GetTenantId()
        {
            return _abpSession.TenantId;
        }
    }
}

using System;

namespace Abp.Zero.SampleApp.Users.Dto
{
    public class ResetPasswordInput
    {
        public Guid? TenantId { get; set; }

        public Guid UserId { get; set; }

        public string ResetCode { get; set; }

        public string Password { get; set; }
    }
}

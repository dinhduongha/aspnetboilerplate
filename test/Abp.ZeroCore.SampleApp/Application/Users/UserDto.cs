using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.ZeroCore.SampleApp.Core;
using System;

namespace Abp.ZeroCore.SampleApp.Application.Users
{
    [AutoMap(typeof(User))]
    public class UserDto : EntityDto<Guid>
    {
        public string UserName { get; set; }
    }
}
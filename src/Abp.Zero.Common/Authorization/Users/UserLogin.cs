﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Used to store a User Login for external Login services.
    /// </summary>
    [Table("AbpUserLogins")]
    public class UserLogin : Entity<Guid>, IMayHaveTenant
    {
        /// <summary>
        /// Maximum length of <see cref="LoginProvider"/> property.
        /// </summary>
        public const int MaxLoginProviderLength = 128;

        /// <summary>
        /// Maximum length of <see cref="ProviderKey"/> property.
        /// </summary>
        public const int MaxProviderKeyLength = 256;

        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Id of the User.
        /// </summary>
        public virtual Guid UserId { get; set; }

        /// <summary>
        /// Login Provider.
        /// </summary>
        [Required]
        [StringLength(MaxLoginProviderLength)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Key in the <see cref="LoginProvider"/>.
        /// </summary>
        [Required]
        [StringLength(MaxProviderKeyLength)]
        public virtual string ProviderKey { get; set; }

        public UserLogin()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public UserLogin(int? tenantId, Guid userId, string loginProvider, string providerKey)
        {
            Id = SequentialGuidGenerator.Instance.Create();
            TenantId = tenantId;
            UserId = userId;
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }
    }
}

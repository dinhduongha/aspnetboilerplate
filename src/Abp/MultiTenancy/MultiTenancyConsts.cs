using System;

namespace Abp.MultiTenancy
{
    public static class MultiTenancyConsts
    {
        /// TODO:

        /// <summary>
        /// Default tenant id: 1.
        /// </summary>
        //public static Guid DefaultTenantId = Guid.Empty; /// TODO: DinhHa
        public static Guid DefaultTenantId = new Guid("00000000-0000-0000-0000-000000000001");
    }
}
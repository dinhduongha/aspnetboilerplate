using System;
using System.Threading.Tasks;

namespace Abp.DynamicEntityParameters
{
    public interface IDynamicParameterPermissionChecker
    {
        void CheckPermission(Guid dynamicParameterId);

        Task CheckPermissionAsync(Guid dynamicParameterId);

        bool IsGranted(Guid dynamicParameterId);

        Task<bool> IsGrantedAsync(Guid dynamicParameterId);
    }
}

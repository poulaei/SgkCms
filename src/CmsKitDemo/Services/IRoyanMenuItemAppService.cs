using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.CmsKit.Menus;

namespace CmsKitDemo.Services;

public interface IRoyanMenuItemAppService : IApplicationService
{
    Task<List<HierarchyNode<RoyanMenuItemDto>>> GetListAsync();
}

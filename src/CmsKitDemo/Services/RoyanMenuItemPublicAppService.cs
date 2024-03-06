using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectMapping;
using Volo.CmsKit.Features;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Menus;
using Volo.CmsKit.Public;

namespace CmsKitDemo.Services;

[RequiresFeature(CmsKitFeatures.MenuEnable)]
[RequiresGlobalFeature(typeof(MenuFeature))]
public class RoyanMenuItemAppService : CmsKitPublicAppServiceBase, IRoyanMenuItemAppService
{
    protected IMenuItemRepository MenuItemRepository { get; }
    protected IDistributedCache<List<RoyanMenuItemDto>> DistributedCache { get; }

    public RoyanMenuItemAppService(
        IMenuItemRepository menuRepository,
        IDistributedCache<List<RoyanMenuItemDto>> distributedCache)
    {
        MenuItemRepository = menuRepository;
        DistributedCache = distributedCache;
    }
        public virtual async Task<List<HierarchyNode<RoyanMenuItemDto>>> GetListAsync()
    {
        var cachedMenu = await DistributedCache.GetOrAddAsync(
            MenuApplicationConsts.MainMenuCacheKey,
            async () =>
            {
                var menuItems = await MenuItemRepository.GetListAsync();

                if (menuItems == null)
                {
                    return new();
                }

                return ObjectMapper.Map<List<MenuItem>, List<RoyanMenuItemDto>>(menuItems);//.AsHierarchy(m => m.Id, m => m.PageId);
            });

        var hm= cachedMenu.AsHierarchy(m => m.Id, m => m.ParentId);
        return hm.ToList();// ObjectMapper.Map<List<RoyanMenuItemDto>, List<HierarchyNode<RoyanMenuItemDto>>>(hm);
    }
}

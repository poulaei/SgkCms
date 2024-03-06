using CmsKitDemo;
using CmsKitDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Identity;
using Volo.CmsKit.Features;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Menus;
using Volo.CmsKit.Public;
using Volo.CmsKit.Public.Menus;

namespace CmsKitDemo.Controllers
{
    //[Dependency(ReplaceServices = true)]
    //[ExposeServices(typeof(MenuItemPublicController))]
    [RequiresFeature(CmsKitFeatures.MenuEnable)]
    [RequiresGlobalFeature(typeof(MenuFeature))]
    [RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
    [Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
    [Route("api/cms-kit/royan-menu-items")]
    public class RoyanMenuController : CmsKitPublicControllerBase, IRoyanMenuItemAppService
    {
        protected IRoyanMenuItemAppService MenuAppService { get; }
        public RoyanMenuController(IRoyanMenuItemAppService menuAppService) //: base(menuAppService)
        {
            MenuAppService = menuAppService;
        }

        //[HttpGet]
        //[Route("my-function")]
        //public Task<string> MyFucntion()
        //{
        //    return Task.FromResult("This doesn't work");
        //}
        [HttpGet]
        public Task<List<HierarchyNode<RoyanMenuItemDto>>> GetListAsync()
        {
            var menuItems =  MenuAppService.GetListAsync().Result;
            return  Task.FromResult(menuItems); 
        }

        
    }
}
using CmsKitDemo.Entities;
using CmsKitDemo.Features;
using CmsKitDemo.GlobalFeatures;
using CmsKitDemo.Permissions;
using CmsKitDemo.Services.Boxes;
using CmsKitDemo.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectMapping;
using Volo.CmsKit;
using Volo.CmsKit.Admin;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Blogs;
using Volo.CmsKit.Comments;
using Volo.CmsKit.Pages;
using Volo.CmsKit.Permissions;
using Volo.CmsKit.Public;
using Volo.CmsKit.Reactions;
using Volo.CmsKit.Tags;

namespace CmsKitDemo.Services;

//[RequiresFeature(CmsKitDemoFeatures.BoxEnable)]
//[RequiresGlobalFeature(typeof(BoxFeature))]
public class BoxAppService : CmsKitPublicAppServiceBase, IBoxAppService
{
    private IBoxRepository BoxRepository { get; }
    protected BoxManager BoxManager { get; }
    public BoxAppService(IBoxRepository boxItemRepository, BoxManager boxManager)
    {
        //دسترسی ها برای سرویس های ویرایشی
        //فعلا به منظور سهولت توسعه دهنده فرانت حذف و نادیده گرفته شد
       
        BoxRepository = boxItemRepository;
        BoxManager = boxManager;
    }

    public async Task<ListResultDto<BoxDto>> GetDetailedListAsync()
    {

        var boxes = await BoxRepository.GetListAsync(includeDetails: true);

        return new ListResultDto<BoxDto>(ObjectMapper.Map<List<Box>, List<BoxDto>>(boxes));
    }

    //public async Task<BoxDto> GetBoxItemsAsync(Guid id)
    //{
    //    var box = await BoxRepository.FindAsync(id,includeDetails: true);
    //    return ObjectMapper.Map<Box, BoxDto>(box);
    //}

    public async Task<ListResultDto<BoxDto>> GetBoxItemsAsync(Guid boxId)
    {
        var box = await BoxRepository.FindAsync(boxId, includeDetails: true);
        var boxes = new List<Box>();
        if (box == null)
        {
            return new ListResultDto<BoxDto>();
        }
        boxes.Add(box);

        return new ListResultDto<BoxDto>(ObjectMapper.Map<List<Box>, List<BoxDto>>(boxes));
    }

    public async Task<ListResultDto<BoxDto>> GetBoxBySectionAsync(string section)
    {

        var box = await BoxRepository.GetBySectionAsync(section, true);
        var boxes = new List<Box>(1);
        if (box == null)
        {
            return new ListResultDto<BoxDto>();
        }
        boxes.Add(box);

        return new ListResultDto<BoxDto>(ObjectMapper.Map<List<Box>, List<BoxDto>>(boxes));
    }

    public async Task<BoxDto> GetAsync(Guid id)
    {
        var page = await BoxRepository.GetAsync(id);
        return ObjectMapper.Map<Box, BoxDto>(page);
    }

    public async Task<PagedResultDto<BoxDto>> GetListAsync(BoxGetListInput input)
    {
        var count = await BoxRepository.GetCountAsync(input.Filter);

        var boxes = await BoxRepository.GetListAsync(
            input.Filter,
            input.MaxResultCount,
            input.SkipCount,
            input.Sorting
        );

        return new PagedResultDto<BoxDto>(
            count,
            ObjectMapper.Map<List<Box>, List<BoxDto>>(boxes)
        );
    }

    //[Authorize(CmsKitDemoPermissions.Box.Create)]
    public async Task<BoxDto> CreateAsync(CreateBoxDto input)
    {
        var box = await BoxManager.CreateAsync(input.Section,
            input.Title, input.Action, input.ActionUrl, input.Summary, input.Status, input.Description);
        input.MapExtraPropertiesTo(box);
        await BoxRepository.InsertAsync(box);

        // await PageCache.RemoveAsync(PageCacheItem.GetKey(page.Slug));

        return ObjectMapper.Map<Box, BoxDto>(box);
    }
    //[Authorize(CmsKitDemoPermissions.Box.Update)]
    public async Task<BoxDto> UpdateAsync(Guid id, UpdateBoxDto input)
    {
        var box = await BoxRepository.GetAsync(id);

        //await PageCache.RemoveAsync(PageCacheItem.GetKey(page.Slug));

        await BoxManager.SetSectionAsync(box, input.Section);

        box.SetTitle(input.Title);
        box.SetAction(input.Action);
        box.SetActionUrl(input.ActionUrl);
        box.SetSummary(input.Summary);
        box.SetDescription(input.Description);
        box.SetStatus(input.Status);
        box.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        input.MapExtraPropertiesTo(box);

        await BoxRepository.UpdateAsync(box);

        return ObjectMapper.Map<Box, BoxDto>(box);
    }

    //[Authorize(CmsKitDemoPermissions.Box.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var box = await BoxRepository.GetAsync(id);
        await BoxRepository.DeleteAsync(box);
        // await PageCache.RemoveAsync(PageCacheItem.GetKey(page.Slug));
    }
}

//public class BoxAppService_1 : CmsKitPublicAppServiceBase
//  //ApplicationService
//  // CrudAppService<Box, BoxDto, Guid, PagedAndSortedResultRequestDto, CreateBoxDto,UpdateBoxDto>,
//  , IBoxAppService_1
//{
//    private readonly IBoxRepository _boxRepository;
//    public BoxAppService_1(IBoxRepository repository, IBoxRepository boxItemRepository)
//    {
//        //دسترسی ها برای سرویس های ویرایشی
//        //فعلا به منظور سهولت توسعه دهنده فرانت حذف و نادیده گرفته شد
//        //CreatePolicyName = CmsKitDemoPermissions.Box.Create;
//        //UpdatePolicyName = CmsKitDemoPermissions.Box.Update;
//        //DeletePolicyName = CmsKitDemoPermissions.Box.Delete;
//        _boxRepository = boxItemRepository;
//    }

//    public async Task<ListResultDto<BoxDto>> GetDetailedListAsync()
//    {

//        var boxes = await _boxRepository.GetListAsync(includeDetails: true);

//        return new ListResultDto<BoxDto>(ObjectMapper.Map<List<Box>, List<BoxDto>>(boxes));
//    }

//    public async Task<BoxDto> GetBoxItemsAsync(Guid id)
//    {

//        var box = await _boxRepository.FindAsync(id, includeDetails: true);
//        return ObjectMapper.Map<Box, BoxDto>(box);

//    }

//    //public async Task<ListResultDto<BoxDto>> GetBoxItemsAsync(Guid id)
//    //{

//    //    var box = await _boxRepository.FindAsync(id, includeDetails: true);
//    //    var boxes = new List<Box>();
//    //    if (box == null)
//    //    {
//    //        return new ListResultDto<BoxDto>();
//    //    }
//    //    boxes.Add(box);

//    //    return new ListResultDto<BoxDto>(ObjectMapper.Map<List<Box>, List<BoxDto>>(boxes));
//    //}

//    public async Task<ListResultDto<BoxDto>> GetBoxBySectionAsync(string section)
//    {

//        var box = await _boxRepository.GetBySectionAsync(section, true);
//        var boxes = new List<Box>(1);
//        if (box == null)
//        {
//            return new ListResultDto<BoxDto>();
//        }
//        boxes.Add(box);

//        return new ListResultDto<BoxDto>(ObjectMapper.Map<List<Box>, List<BoxDto>>(boxes));
//    }
//}

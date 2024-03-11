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
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.Comments;
using Volo.CmsKit.Permissions;
using Volo.CmsKit.Reactions;

namespace CmsKitDemo.Services
{
    [RequiresFeature(CmsKitDemoFeatures.BoxEnable)]
    [RequiresGlobalFeature(typeof(BoxFeature))]
    public class BoxAppService :
        CrudAppService<Box, BoxDto, Guid, PagedAndSortedResultRequestDto, CreateBoxDto,UpdateBoxDto>,
        IBoxAppService
    {
        public BoxAppService(IRepository<Box, Guid> repository) : base(repository)
        {
            //دسترسی ها برای سرویس های ویرایشی
            //فعلا به منظور سهولت توسعه دهنده فرانت حذف و نادیده گرفته شد
           // CreatePolicyName = CmsKitDemoPermissions.Box.Create;
            //UpdatePolicyName = CmsKitDemoPermissions.Box.Update;
            //DeletePolicyName = CmsKitDemoPermissions.Box.Delete;
        }

        public async Task<List<BoxDto>> GetDetailedListAsync()
        {
            var dbContext = await Repository.GetDbContextAsync();

            var boxes = await (from box in dbContext.Set<Box>() 
                                select box).ToListAsync();

            return boxes.Select(x => new BoxDto
            {
                Id = x.Id,
                Section=x.Section,
                Title = x.Title,
                Action =x.Action,
                ActionUrl=x.ActionUrl,
                Summary=x.Summary,
                Status=x.Status,
                Description = x.Description,
               
            }).ToList();
        }

      
    }
}

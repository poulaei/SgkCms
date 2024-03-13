using CmsKitDemo.Entities;
using CmsKitDemo.Permissions;
using CmsKitDemo.Services.Boxes;
using CmsKitDemo.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.CmsKit.Comments;
using Volo.CmsKit.Reactions;

namespace CmsKitDemo.Services
{
    public class BoxItemAppService :
        CrudAppService<BoxItem, BoxItemDto, Guid, PagedAndSortedResultRequestDto, CreateBoxItemDto, UpdateBoxItemDto>,
        IBoxItemAppService
    {
        public BoxItemAppService(IRepository<BoxItem, Guid> repository) : base(repository)
        {
            CreatePolicyName = CmsKitDemoPermissions.BoxItem.Create;
            UpdatePolicyName = CmsKitDemoPermissions.BoxItem.Update;
            DeletePolicyName = CmsKitDemoPermissions.BoxItem.Delete;
        }

        public async Task<List<BoxItemDto>> GetDetailedListAsync()
        {
            var dbContext = await Repository.GetDbContextAsync();

            var boxes = await (from box in dbContext.Set<BoxItem>() 
                                select box).ToListAsync();

            return boxes.Select(x => new BoxItemDto
            {
                Id = x.Id,
                BoxId= x.BoxId,
                Title = x.Title,
                Action =x.Action,
                ActionUrl=x.ActionUrl,
                Summary=x.Summary,
                Icon=x.Icon,
                MediaId=x.MediaId,  
                Description = x.Description,
               
            }).ToList();
        }

      
    }
}

using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CmsKitDemo.Services.Boxes;

public interface IBoxItemAppService : ICrudAppService<BoxItemDto, Guid, PagedAndSortedResultRequestDto, CreateBoxItemDto, UpdateBoxItemDto>
{
    Task<List<BoxItemDto>> GetDetailedListAsync();
}

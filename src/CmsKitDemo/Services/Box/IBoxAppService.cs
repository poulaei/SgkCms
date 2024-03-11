using CmsKitDemo.Services.Dtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CmsKitDemo.Services.Boxes;

public interface IBoxAppService : ICrudAppService<BoxDto, Guid, PagedAndSortedResultRequestDto, CreateBoxDto, UpdateBoxDto>
{
    Task<List<BoxDto>> GetDetailedListAsync();
}

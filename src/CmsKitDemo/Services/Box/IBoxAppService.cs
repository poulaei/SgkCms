using CmsKitDemo.Services.Dtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CmsKitDemo.Services.Boxes;

public interface IBoxAppService_CRUD : ICrudAppService<BoxDto, Guid, PagedAndSortedResultRequestDto, CreateBoxDto, UpdateBoxDto>
{
    Task<List<BoxDto>> GetDetailedListAsync();
}
public interface IBoxAppService : IApplicationService
{
    Task<List<BoxDto>> GetDetailedListAsync();
}

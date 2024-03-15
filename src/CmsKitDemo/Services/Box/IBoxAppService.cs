using CmsKitDemo.Services.Dtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CmsKitDemo.Services.Boxes;

public interface IBoxAppService : ICrudAppService<BoxDto, BoxDto, Guid, BoxGetListInput, CreateBoxDto, UpdateBoxDto>
{
    Task<ListResultDto<BoxDto>> GetDetailedListAsync();
    Task<ListResultDto<BoxDto>> GetBoxItemsAsync(Guid boxId);
    Task<ListResultDto<BoxDto>> GetBoxBySectionAsync(string section);
}
//public interface IBoxAppService_1 : IApplicationService
//{
//    Task<ListResultDto<BoxDto>> GetDetailedListAsync();
//}

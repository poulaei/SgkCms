using System;
using Volo.Abp.Application.Dtos;

namespace CmsKitDemo.Services.Boxes;

[Serializable]
public class BoxItemGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}

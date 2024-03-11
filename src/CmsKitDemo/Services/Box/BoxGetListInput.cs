using System;
using Volo.Abp.Application.Dtos;

namespace CmsKitDemo.Services.Boxes;

[Serializable]
public class BoxGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}

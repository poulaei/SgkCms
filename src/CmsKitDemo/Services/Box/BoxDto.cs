using CmsKitDemo.Entities;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CmsKitDemo.Services.Boxes;

[Serializable]
public class BoxDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
{
    public  required string Section { get; set; }
    public  string? Title { get;  set; }
    public  string? Action { get;  set; }
    public string? ActionUrl { get;  set; }
    public  string? Summary { get;  set; }
    public  BoxStatus Status { get;  set; }
    public string? Description { get;  set; }
    public string ConcurrencyStamp { get; set; }
}

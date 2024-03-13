using CmsKitDemo.Entities;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace CmsKitDemo.Services.Boxes;

[Serializable]
public class BoxItemDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
{
    public  Guid BoxId { get;  set; }
    public  string? Title { get;  set; }
    public  string? Action { get;  set; }
    public string? ActionUrl { get;  set; }
    public  string? Summary { get;  set; }
    public string? Icon { get; set; }
    public string? Description { get;  set; }
    public string ConcurrencyStamp { get; set; }
    public Guid? MediaId { get; set; }
}

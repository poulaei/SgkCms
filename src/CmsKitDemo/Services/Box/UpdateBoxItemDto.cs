using CmsKitDemo.Entities;
using CmsKitDemo;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;
using Volo.CmsKit.Blogs;

namespace CmsKitDemo.Services.Boxes;

[Serializable]
public class UpdateBoxItemDto : ExtensibleObject, IHasConcurrencyStamp
{
  
    [DynamicMaxLength(typeof(BoxItemConsts), nameof(BoxItemConsts.TitleMaxLength))]
    public string? Title { get; set; }
    [DynamicMaxLength(typeof(BoxItemConsts), nameof(BoxItemConsts.ActionMaxLength))]
    public string? Action { get; set; }
    [DynamicMaxLength(typeof(BoxItemConsts), nameof(BoxConsts.ActionUrlMaxLength))]
    public string? ActionUrl { get; set; }
    [DynamicMaxLength(typeof(BoxItemConsts), nameof(BoxItemConsts.SummaryMaxLength))]
    public string? Summary { get; set; }
    [DynamicMaxLength(typeof(BoxItemConsts), nameof(BoxItemConsts.IconMaxLength))]
    public string? Icon { get; set; }
    [DynamicMaxLength(typeof(BoxItemConsts), nameof(BoxItemConsts.DescriptionMaxLength))]
    public string? Description { get; set; }
    public Guid? MediaId { get; set; }
    public string ConcurrencyStamp { get; set; }
}

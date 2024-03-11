using CmsKitDemo;
using CmsKitDemo.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;
using Volo.CmsKit.Blogs;

namespace CmsKitDemo.Services.Boxes;

[Serializable]
public class CreateBoxItemDto : ExtensibleObject
{
    [Required]
    public  Guid BoxId { get;  set; }

    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.TitleMaxLength))]
    public string? Title { get; set; }
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.ActionMaxLength))]
    public string? Action { get; set; }
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.ActionUrlMaxLength))]
    public string? ActionUrl { get; set; }
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.SummaryMaxLength))]
    public string? Summary { get; set; }
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.DescriptionMaxLength))]
    public string? Description { get; set; }
    public Guid? MediaId { get; set; }
}

﻿using CmsKitDemo.Entities;
using CmsKitDemo;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;
using Volo.CmsKit.Blogs;

namespace CmsKitDemo.Services.Boxes;

[Serializable]
public class UpdateBoxDto : ExtensibleObject, IHasConcurrencyStamp
{
    [Required]
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.SectionMaxLength))]
    public required string Section { get; set; }

    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.TitleMaxLength))]
    public string? Title { get; set; }
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.ActionMaxLength))]
    public string? Action { get; set; }
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.ActionUrlMaxLength))]
    public string? ActionUrl { get; set; }
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.SummaryMaxLength))]
    public string? Summary { get; set; }
    public BoxStatus Status { get; set; }
    [DynamicMaxLength(typeof(BoxConsts), nameof(BoxConsts.DescriptionMaxLength))]
    public string? Description { get; set; }

    public string ConcurrencyStamp { get; set; }
}

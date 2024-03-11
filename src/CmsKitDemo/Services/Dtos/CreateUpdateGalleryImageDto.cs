using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;
using Volo.Abp.Validation;

namespace CmsKitDemo.Services.Dtos
{
    public class CreateUpdateGalleryImageDto
    {
        [NotNull]
        [StringLength(CmsKitDemoConsts.MaxDescriptionLength)]
        //[DynamicMaxLength(typeof(CmsKitDemoConsts), nameof(CmsKitDemoConsts.MaxDescriptionLength))]
        public string Description { get; set; }

        public Guid CoverImageMediaId { get; set; }
    }
}

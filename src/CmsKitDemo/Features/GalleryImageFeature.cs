using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace CmsKitDemo.GlobalFeatures;
//Added by poolaei @1402/12/18
[GlobalFeatureName(Name)]
public class GalleryImageFeature : GlobalFeature
{
    public const string Name = "CmsKitDemo.GalleryImage";

    internal GalleryImageFeature(
        [NotNull] GlobalCmsKitDemoFeatures cmsKit
    ) : base(cmsKit)
    {

    }

    public override void Enable()
    {
        var userFeature = FeatureManager.Modules.CmsKit().User;
        if (!userFeature.IsEnabled)
        {
            userFeature.Enable();
        }

        base.Enable();
    }
}

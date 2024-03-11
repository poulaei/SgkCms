using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace CmsKitDemo.GlobalFeatures;

[GlobalFeatureName(Name)]
public class BoxFeature : GlobalFeature
{
    public const string Name = "CmsKitDemo.Box";

    internal BoxFeature(
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

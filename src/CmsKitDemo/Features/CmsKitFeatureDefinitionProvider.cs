using CmsKitDemo.GlobalFeatures;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;
using Volo.CmsKit.Features;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Localization;

namespace CmsKitDemo.Features;
public class CmsKitDemoFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
        var group = context.AddGroup(CmsKitDemoFeatures.GroupName,
           L("Feature:CmsKitDemoGroup"));

        // var group = context.GetGroupOrNull(CmsKitFeatures.GroupName);//, L("Feature:CmsKitGroup"));
        //Added by poolaei @1402/12/18
        if (GlobalFeatureManager.Instance.IsEnabled<GalleryImageFeature>())
        {
            group.AddFeature(CmsKitDemoFeatures.GalleryImageEnable,
            "true",
            L("Feature:GalleryImageEnable"),
            L("Feature:GalleryImageEnableDescription"),
            new ToggleStringValueType());
        }
        //Added by poolaei @1402/12/21
        if (GlobalFeatureManager.Instance.IsEnabled<BoxFeature>())
        {
            group.AddFeature(CmsKitDemoFeatures.BoxEnable,
            "true",
            L("Feature:BoxEnable"),
            L("Feature:BoxEnableDescription"),
            new ToggleStringValueType());
        }
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CmsKitResource>(name);
    }
}

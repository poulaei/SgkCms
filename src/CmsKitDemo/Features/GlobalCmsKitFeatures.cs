using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace CmsKitDemo.GlobalFeatures;

public class GlobalCmsKitDemoFeatures : GlobalModuleFeatures
{
    public const string ModuleName = "CmsKitDemo";


    //Added by poolaei @1402/12/18
    public GalleryImageFeature Gallery => GetFeature<GalleryImageFeature>();

    //Added by poolaei @1402/12/21
    public BoxFeature Box => GetFeature<BoxFeature>();
    public GlobalCmsKitDemoFeatures([NotNull] GlobalFeatureManager featureManager)
        : base(featureManager)
    {
        //Added by poolaei @1402/12/18
        AddFeature(new GalleryImageFeature(this));
        //Added by poolaei @1402/12/21
        AddFeature(new BoxFeature(this));
    }
}

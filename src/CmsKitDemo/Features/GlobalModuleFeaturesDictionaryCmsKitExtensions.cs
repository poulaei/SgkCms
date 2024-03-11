using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.GlobalFeatures;

namespace CmsKitDemo.GlobalFeatures;
//Added by poolaei @1402/12/18
public static class GlobalModuleFeaturesDictionaryCmsKitdemoExtensions
{
    public static GlobalCmsKitDemoFeatures CmsKitDemo(
        [NotNull] this GlobalModuleFeaturesDictionary modules)
    {
        Check.NotNull(modules, nameof(modules));

        return modules
                .GetOrAdd(
                    GlobalCmsKitDemoFeatures.ModuleName,
                    _ => new GlobalCmsKitDemoFeatures(modules.FeatureManager)
                )
            as GlobalCmsKitDemoFeatures;
    }

    public static GlobalModuleFeaturesDictionary CmsKitDemo(
        [NotNull] this GlobalModuleFeaturesDictionary modules,
        [NotNull] Action<GlobalCmsKitDemoFeatures> configureAction)
    {
        Check.NotNull(configureAction, nameof(configureAction));

        configureAction(modules.CmsKitDemo());

        return modules;
    }
}

using CmsKitDemo.GlobalFeatures;
using CmsKitDemo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Localization;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Permissions;

namespace CmsKitDemo.Permissions;
//Added by poolaei @1402/12/18
public class CmsKitDemoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var cmsDemoGroup = context.GetGroupOrNull(CmsKitDemoPermissions.GroupName) ?? context.AddGroup(CmsKitDemoPermissions.GroupName, L("Permission:CmsKitDemo"));

        var galleryDemo = cmsDemoGroup.AddPermission(CmsKitDemoPermissions.GalleryImage.Management, L("Permission:ImageManagement"))
             .RequireGlobalFeatures(typeof(GalleryImageFeature)); 
        galleryDemo.AddChild(CmsKitDemoPermissions.GalleryImage.Create, L("Permission:Create")).RequireGlobalFeatures(typeof(GalleryImageFeature)); 
        galleryDemo.AddChild(CmsKitDemoPermissions.GalleryImage.Update, L("Permission:Edit")).RequireGlobalFeatures(typeof(GalleryImageFeature)); 
        galleryDemo.AddChild(CmsKitDemoPermissions.GalleryImage.Delete, L("Permission:Delete")).RequireGlobalFeatures(typeof(GalleryImageFeature)); 
        //Added by poolaei @1402/12/21 =====================================
        var boxGroup = cmsDemoGroup.AddPermission(CmsKitDemoPermissions.Box.Management, L("Permission:BoxManagement"))
            .RequireGlobalFeatures(typeof(BoxFeature));
        boxGroup.AddChild(CmsKitDemoPermissions.Box.Create, L("Permission:Create")).RequireGlobalFeatures(typeof(BoxFeature)); ;
        boxGroup.AddChild(CmsKitDemoPermissions.Box.Update, L("Permission:Edit")).RequireGlobalFeatures(typeof(BoxFeature)); ;
        boxGroup.AddChild(CmsKitDemoPermissions.Box.Delete, L("Permission:Delete")).RequireGlobalFeatures(typeof(BoxFeature)); ;

        var boxItemGroup = cmsDemoGroup.AddPermission(CmsKitDemoPermissions.BoxItem.Management, L("Permission:BoxItemManagement"))
            .RequireGlobalFeatures(typeof(BoxFeature));
        boxItemGroup.AddChild(CmsKitDemoPermissions.BoxItem.Create, L("Permission:Create")).RequireGlobalFeatures(typeof(BoxFeature)); ;
        boxItemGroup.AddChild(CmsKitDemoPermissions.BoxItem.Update, L("Permission:Edit")).RequireGlobalFeatures(typeof(BoxFeature)); ;
        boxItemGroup.AddChild(CmsKitDemoPermissions.BoxItem.Delete, L("Permission:Delete")).RequireGlobalFeatures(typeof(BoxFeature)); ;
        //===============================================================
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CmsKitDemoResource>(name);
    }
}


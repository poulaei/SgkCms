using CmsKitDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.CmsKit.Blogs;
using Volo.CmsKit.Comments;
using Volo.CmsKit.EntityFrameworkCore;
using Volo.CmsKit.GlobalResources;
using Volo.CmsKit.MediaDescriptors;
using Volo.CmsKit.Menus;
using Volo.CmsKit.Pages;
using Volo.CmsKit.Ratings;
using Volo.CmsKit.Reactions;
using Volo.CmsKit.Tags;
using Volo.CmsKit.Users;

namespace CmsKitDemo.Data;

[ReplaceDbContext(typeof(ICmsKitDbContext))]
public class CmsKitDemoDbContext : AbpDbContext<CmsKitDemoDbContext>, ICmsKitDbContext
{
    public DbSet<GalleryImage> GalleryImages { get; set; }
    public DbSet<Box> Boxs { get; set; }
    public DbSet<BoxItem> BoxItems { get; set; }

    #region CMS Kit Entities

    public DbSet<Comment> Comments { get; set; }

    public DbSet<CmsUser> User { get; set; }

    public DbSet<UserReaction> Reactions { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<EntityTag> EntityTags { get; set; }

    public DbSet<Page> Pages { get; set; }

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<BlogPost> BlogPosts { get; set; }

    public DbSet<BlogFeature> BlogFeatures { get; set; }

    public DbSet<MediaDescriptor> MediaDescriptors { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }

    public DbSet<GlobalResource> GlobalResources { get; set; }

    #endregion

    public CmsKitDemoDbContext(DbContextOptions<CmsKitDemoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureCmsKit();

        /* Configure your own entities here */
        builder.Entity<GalleryImage>(b =>
        {
            b.ToTable(CmsKitDemoConsts.DbTablePrefix + "Images", CmsKitDemoConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Box>(b =>
        {
            b.ToTable(CmsKitDemoConsts.DbTablePrefix + "Box", CmsKitDemoConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Section).IsRequired().HasMaxLength(BoxConsts.SectionMaxLength);
            b.Property(x => x.Title).HasMaxLength(BoxConsts.TitleMaxLength);
            b.Property(x => x.Action).HasMaxLength(BoxConsts.ActionMaxLength);
            b.Property(x => x.ActionUrl).HasMaxLength(BoxConsts.ActionUrlMaxLength);
            b.Property(x => x.Summary).HasMaxLength(BoxConsts.SummaryMaxLength);
            b.Property(x => x.Description).HasMaxLength(BoxConsts.DescriptionMaxLength);
            b.HasMany(x => x.BoxItems).WithOne().HasForeignKey(bd => bd.BoxId).IsRequired();
            b.HasIndex(x => new { x.Section ,x.Status});

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<BoxItem>(b =>
        {
            b.ToTable(CmsKitDemoConsts.DbTablePrefix + "BoxItems", CmsKitDemoConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Title).HasMaxLength(BoxItemConsts.TitleMaxLength);
            b.Property(x => x.Action).HasMaxLength(BoxItemConsts.ActionMaxLength);
            b.Property(x => x.ActionUrl).HasMaxLength(BoxItemConsts.ActionUrlMaxLength);
            b.Property(x => x.Summary).HasMaxLength(BoxItemConsts.SummaryMaxLength);
            b.Property(x => x.Icon).HasMaxLength(BoxItemConsts.IconMaxLength);
            b.Property(x => x.Description).HasMaxLength(BoxItemConsts.DescriptionMaxLength);
            b.Property(x => x.MediaId).HasMaxLength(BoxItemConsts.DescriptionMaxLength);
            b.ApplyObjectExtensionMappings();

        });
    }
}

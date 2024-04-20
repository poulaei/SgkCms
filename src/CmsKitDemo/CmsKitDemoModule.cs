using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using CmsKitDemo.Data;
using CmsKitDemo.Localization;
using CmsKitDemo.Menus;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Uow;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Emailing;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.CmsKit;
using Volo.CmsKit.EntityFrameworkCore;
using Volo.CmsKit.Web;
using Volo.Abp.Threading;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.CmsKit.Reactions;
using Volo.CmsKit.Comments;
using Volo.CmsKit.Web.Contents;
using Polly;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.Blogs;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.MediaDescriptors;
using Volo.CmsKit.Pages;
using Volo.CmsKit.Permissions;
using CmsKitDemo.GlobalFeatures;
using CmsKitDemo.Permissions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore.Internal;

namespace CmsKitDemo;

[DependsOn(
    // ABP Framework packages
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpEntityFrameworkCoreSqliteModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),

    // Account module packages
    typeof(AbpAccountApplicationModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpAccountWebOpenIddictModule),

    // Identity module packages
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpIdentityWebModule),

    // Audit logging module packages
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),

    // Permission Management module packages
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),

    // Tenant Management module packages
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpTenantManagementWebModule),

    // Feature Management module packages
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpFeatureManagementWebModule),

    // Setting Management module packages
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpSettingManagementWebModule),

    // CMS Kit module packages
    typeof(CmsKitApplicationModule),
    typeof(CmsKitEntityFrameworkCoreModule),
    typeof(CmsKitHttpApiModule),
    typeof(CmsKitWebModule),

    // Blob Storing module packages
    typeof(BlobStoringDatabaseDomainModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
)]
public class CmsKitDemoModule : AbpModule
{
    /* Single point to enable/disable multi-tenancy */
    public const bool IsMultiTenant = true;
    private const string DefaultCorsPolicyName = "Default";

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        CmsKitDemoGlobalFeatureConfigurator.Configure();

        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(CmsKitDemoResource)
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("CmsKitDemo");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        if (hostingEnvironment.IsDevelopment())
        {
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
        }

        ConfigureAuthentication(context);
        ConfigureMultiTenancy();
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper(context);
        //ConfigureSwagger(context.Services);
        ConfigureSwagger(context, configuration);
        //ConfigureCors(context.Services);
        ConfigureCors(context, configuration);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureVirtualFiles(hostingEnvironment);
        ConfigureLocalization();
        ConfigureEfCore(context);
        ConfigureRazorPages();
        ConfigureCmsKit(context);


        //Configure<CmsKitContentWidgetOptions>(options =>
        //{
        //    options.AddWidget("MySimpleWidget", "MySimpleWidget","name");
        //});

        Configure<CmsKitContentWidgetOptions>(options =>
        {
            options.AddWidget(widgetType: "Today", widgetName: "CmsToday", parameterWidgetName: "Format");
            // options.AddWidget("Format", "Format");
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        //context.Services.AddAuthentication("Bearer")//JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options =>
        //    {
        //        options.Authority = configuration["AuthServer:Authority"];
        //        options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
        //        options.Audience = "CmsKitDemo";
        //    });

        //context.Services.AddAuthentication()
        //       .AddJwtBearer(options =>
        //       {
        //           options.Authority = configuration["AuthServer:Authority"];
        //           options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
        //           options.Audience = "AngularMaterial";
        //           options.BackchannelHttpHandler = new HttpClientHandler
        //           {
        //               ServerCertificateCustomValidationCallback =
        //                   HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        //           };
        //       });
    }

    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = IsMultiTenant;
        });
    }


    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            //options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

            //options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            //options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                BasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<CmsKitDemoResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/CmsKitDemo");

            options.DefaultResourceType = typeof(CmsKitDemoResource);

            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("CmsKitDemo", typeof(CmsKitDemoResource));
        });
    }

    private void ConfigureVirtualFiles(IWebHostEnvironment hostingEnvironment)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitDemoModule>();
            if (hostingEnvironment.IsDevelopment())
            {
                /* Using physical files in development, so we don't need to recompile on changes */
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitDemoModule>(hostingEnvironment.ContentRootPath);
            }
        });
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsKitDemoMenuContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(CmsKitDemoModule).Assembly);
        });
    }

    private void ConfigureSwagger(ServiceConfigurationContext context, IConfiguration configuration)
    {
        //services.AddAbpSwaggerGen(
        //    options =>
        //    {
        //        options.SwaggerDoc("v1", new OpenApiInfo { Title = "CmsKitDemo API", Version = "v1" });
        //        options.DocInclusionPredicate((docName, description) => true);
        //        options.CustomSchemaIds(type => type.FullName);
        //       // options.HideAbpEndpoints();
        //    }
        //);

        //services.AddAbpSwaggerGenWithOAuth(
        //        "https://localhost:44373",             // authority issuer
        //        new Dictionary<string, string>         //
        //        {                                      // scopes
        //         {"CmsKitDemo", "CmsKitDemo API"}               //
        //        },                                     //
        //        options =>
        //        {
        //            options.SwaggerDoc("v1", new OpenApiInfo { Title = "CmsKitDemo API", Version = "v1" });
        //            options.DocInclusionPredicate((docName, description) => true);
        //            options.CustomSchemaIds(type => type.FullName);
        //        }
        //    );

        context.Services.AddAbpSwaggerGenWithOidc(
            configuration["AuthServer:Authority"],
            scopes: new[] { "CmsKitDemo" },
            // "authorization_code"
            flows: new[] { AbpSwaggerOidcFlows.AuthorizationCode },
            // When deployed on K8s, should be metadata URL of the reachable DNS over internet like https://myauthserver.company.com
             discoveryEndpoint: configuration["AuthServer:Authority"],
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CmsKitDemo API", Version = "v1" });
                if (configuration["Royan:ShowApiInSwagger"] == "All")
                {  
                    options.DocInclusionPredicate((docName, description) => true);
                }
                else {
                    options.DocInclusionPredicate((docName, description) =>
                    {
                        // Generate only api that related to my api
                        return description.RelativePath.IndexOf("/royan") >= 0
                        | description.RelativePath.IndexOf("/box") >= 0
                         | description.RelativePath.IndexOf("/media") >= 0
                         | description.RelativePath.IndexOf("/login") >= 0;
                    });
                }
               
               
                options.CustomSchemaIds(type => type.FullName);
                //options.CustomOperationIds(apiDesc =>
                //{
                //    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                //});

              // options.DocumentFilter<ApiOptionFilter>();
                //options.HideAbpEndpoints();
            });

    }

    private void ConfigureSwagger11111111111(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CmsKitDemo API", Version = "v1" });
                if (configuration["Royan:ShowApiInSwagger"] == "All")
                {
                    options.DocInclusionPredicate((docName, description) => true);
                }
                else
                {
                    options.DocInclusionPredicate((docName, description) =>
                    {
                        // Generate only api that related to my api
                        return description.RelativePath.IndexOf("/royan") >= 0
                        | description.RelativePath.IndexOf("/box") >= 0
                         | description.RelativePath.IndexOf("/media") >= 0
                         | description.RelativePath.IndexOf("/login") >= 0;
                    });
                }
                options.CustomSchemaIds(type => type.FullName);
                // options.HideAbpEndpoints();
            }
        );

              

    }
    private void ConfigureCors(IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        services.AddCors(options =>
        {
            //options.AddPolicy(DefaultCorsPolicyName, builder =>
            //{
            //    builder.AllowAnyOrigin()
            //           .AllowAnyMethod()
            //           .AllowAnyHeader();
            //});


            options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        //.WithOrigins(
                        //    configuration["App:CorsOrigins"]
                        //        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        //        .Select(o => o.RemovePostFix("/"))
                        //        .ToArray() ?? Array.Empty<string>()
                        //)
                        .WithAbpExposedHeaders()
                        .WithExposedHeaders("Access-Control-Allow-Origin")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                        //.AllowCredentials();
                });
            


        });

        //Configure<AbpAntiForgeryOptions>(options =>
        //{
        //    //options.TokenCookie.SecurePolicy = CookieSecurePolicy.None;
        //   // options.TokenCookie.Expiration = TimeSpan.FromDays(365);
        //    options.AutoValidate = false;
        //    //options.AutoValidateIgnoredHttpMethods.Remove("GET");
        //    //options.AutoValidateFilter =
        //    //    type => !type.Namespace.StartsWith("CmsKitDemo");
        //});
    }
    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? Array.Empty<string>()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CmsKitDemoModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            /* Uncomment `validate: true` if you want to enable the Configuration Validation feature.
             * See AutoMapper's documentation to learn what it is:
             * https://docs.automapper.org/en/stable/Configuration-validation.html
             */
            options.AddMaps<CmsKitDemoModule>(/* validate: true */);
        });
    }

    private void ConfigureEfCore(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CmsKitDemoDbContext>(options =>
        {
            /* You can remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots
             * Documentation: https://docs.abp.io/en/abp/latest/Entity-Framework-Core#add-default-repositories
             */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(configurationContext =>
            {
                configurationContext.UseSqlite();
            });
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }

    private void ConfigureRazorPages()
    {
        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AddPageRoute("/Gallery/Index", "image-gallery");
            options.Conventions.AddPageRoute("/Gallery/Detail", "image-gallery/{Id}/detail");

            //admin UI for image-gallery management
            options.Conventions.AddPageRoute("/Gallery/Management/Index", "ImageManagement");
        });
    }

    private void ConfigureCmsKit(ServiceConfigurationContext context)
    {
        Configure<CmsKitReactionOptions>(options =>
        {
            options.EntityTypes.Add(
                new ReactionEntityTypeDefinition(
                    entityType: CmsKitDemoConsts.ImageGalleryEntityType,
                    reactions: new[]
                    {
                        new ReactionDefinition(StandardReactions.Heart)
                    }));
        });

        Configure<CmsKitCommentOptions>(options =>
        {
            options.EntityTypes.Add(new CommentEntityTypeDefinition(CmsKitDemoConsts.ImageGalleryEntityType));
            options.IsRecaptchaEnabled = true;
        });

        //Added by poolaei @1402/12/18
        if (GlobalFeatureManager.Instance.IsEnabled<MediaFeature>())
        {
            Configure<CmsKitMediaOptions>(options =>
            {
                if (GlobalFeatureManager.Instance.IsEnabled<GalleryImageFeature>())
                {
                    options.EntityTypes.AddIfNotContains(
                        new MediaDescriptorDefinition(
                            CmsKitDemoConsts.ImageGalleryEntityType,
                            createPolicies: new[]
                            {
                                    CmsKitDemoPermissions.GalleryImage.Create,
                                    CmsKitDemoPermissions.GalleryImage.Update
                            },
                            deletePolicies: new[]
                            {
                                    CmsKitDemoPermissions.GalleryImage.Create,
                                    CmsKitDemoPermissions.GalleryImage.Update,
                                    CmsKitDemoPermissions.GalleryImage.Delete
                            }));
                }
                //Added by poolaei @1402/12/21
                if (GlobalFeatureManager.Instance.IsEnabled<BoxFeature>())
                {
                    options.EntityTypes.AddIfNotContains(
                        new MediaDescriptorDefinition(
                            BoxConsts.EntityType,
                            createPolicies: new[]
                            {
                                    CmsKitDemoPermissions.Box.Create,
                                    CmsKitDemoPermissions.Box.Update
                            },
                            deletePolicies: new[]
                            {
                                    CmsKitDemoPermissions.Box.Create,
                                    CmsKitDemoPermissions.Box.Update,
                                    CmsKitDemoPermissions.Box.Delete
                            }));
                }


            });
        }

    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (IsMultiTenant)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();

        app.UseSwagger();
        //app.UseCors(DefaultCorsPolicyName);
        app.UseCors();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "CmsKitDemo API");
            //var configuration = context.GetConfiguration();
            //options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            //options.OAuthClientSecret(configuration["AuthServer:ClientSecret"]); 
            
            //options.OAuthScopes("CmsKitDemo");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}



using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CmsKitDemo;
public class AuthResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

        if (authAttributes.Any())
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
    }
}
[AttributeUsage(AttributeTargets.Class)]
public class ShowInSwaggerAttribute : Attribute
{
}
public class ShowInSwaggerAttributeFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var filteredApis = context.ApiDescriptions.Where(a => a.CustomAttributes().Any(x =>
            x.GetType() == typeof(ShowInSwaggerAttribute)));

        foreach (var path in swaggerDoc.Paths.ToList())
        {
            if (filteredApis.All(x => ("/" + x.RelativePath) != path.Key))
                swaggerDoc.Paths.Remove(path.Key);
        }
    }
}
internal class ApiOptionFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
            if (apiDescription.TryGetMethodInfo(out var method))
                if (method.ReflectedType != null && method.ReflectedType.CustomAttributes.Any())
                {
                    var pathToRemove = swaggerDoc.Paths
                        .Where(p => p.Key.Contains("Abp", StringComparison.CurrentCultureIgnoreCase)).ToList();
                    foreach (var item in pathToRemove) swaggerDoc.Paths.Remove(item.Key);
                }
    }
}

internal class HideOrganizationUnitsFilter : IDocumentFilter
{
    private const string pathToHide = "/identity/organization-units";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var organizationUnitPaths = swaggerDoc
            .Paths
            .Where(pathItem => pathItem.Key.Contains(pathToHide, StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var item in organizationUnitPaths)
        {
            swaggerDoc.Paths.Remove(item.Key);
        }
    }
}


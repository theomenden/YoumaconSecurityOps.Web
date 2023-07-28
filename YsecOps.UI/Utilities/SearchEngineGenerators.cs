using System.Net.Mime;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using YsecOps.UI.Utilities.Sitemap;

namespace YsecOps.UI.Utilities;

public static class SearchEngineGenerators
{
    public static async Task GenerateRobotsAsync(HttpContext context, CancellationToken cancellationToken = default)
    {
        var baseUrl = GetBaseUrl(context);

        context.Response.ContentType = MediaTypeNames.Text.Plain;

        await context.Response.WriteAsync("User-agent: *\n", cancellationToken).ConfigureAwait(false);
        await context.Response.WriteAsync("Disallow: \n\n", cancellationToken).ConfigureAwait(false);
        await context.Response.WriteAsync($"Sitemap: {baseUrl}/sitemap.txt", cancellationToken).ConfigureAwait(false);
    }

    public static async Task GenerateSiteMapAsync(HttpContext context, CancellationToken cancellationToken = default)
    {
        var pages = typeof(App)
            .Assembly
            .ExportedTypes
            .Where(p => p.IsSubclassOf(typeof(ComponentBase))
                        && !String.IsNullOrWhiteSpace(p?.Namespace)
                        && p.Namespace.StartsWith("YSecOps.UI.Pages"));

        var baseUrl = GetBaseUrl(context);

        foreach (var routeAttribute in pages
                     .Where(pageType => pageType.CustomAttributes is not null)
                     .SelectMany(pageType => pageType.GetCustomAttributes<RouteAttribute>()))
        {
            await context.Response.WriteAsync($"{baseUrl}{routeAttribute.Template}\n", cancellationToken).ConfigureAwait(false);
        }
    }

    public static Task GenerateSiteMapXmlAsync(HttpContext context, CancellationToken cancellationToken = default)
    {
        var pages = typeof(App)
            .Assembly
            .ExportedTypes
            .Where(p => p.IsSubclassOf(typeof(ComponentBase))
                        && !String.IsNullOrWhiteSpace(p?.Namespace)
                        && p.Namespace.StartsWith("YsecOps.UI.Pages"));

        var baseUrl = GetBaseUrl(context);

        var nodes = pages
            .Where(x => x.CustomAttributes is not null)
            .SelectMany(x => x.GetCustomAttributes<RouteAttribute>())
            .Select(x => new MapNode(
                null,
                DateTime.UtcNow,
                null,
                $"{baseUrl}{x.Template}"));

        var serializedXml = Sitemap.Sitemap.WriteSitemapToString(nodes);

        context.Response.ContentType = MediaTypeNames.Text.Xml;

        return context.Response.WriteAsync(serializedXml, cancellationToken);
    }

    private static string GetBaseUrl(HttpContext context) =>
        $"{context.Request.Scheme}://{context.Request.Host.Value}{context.Request.PathBase.Value}";
}

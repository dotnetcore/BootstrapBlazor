// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;

namespace BootstrapBlazor.Server.Extensions;

internal static class StaticFileResponseContextExtensions
{
    public static bool IsSupportAssets(this IWebHostEnvironment webHost)
    {
#if NET9_0_OR_GREATER
        return true;
#else
        return false;
#endif
    }

    public static void ProcessCache(this StaticFileResponseContext context, IConfiguration configuration)
    {
        if (context.CanCache(configuration, out var age))
        {
            context.Context.Response.Headers[HeaderNames.CacheControl] = $"public, max-age={age}";
        }
    }

    private static bool CanCache(this StaticFileResponseContext context, IConfiguration configuration, out int age)
    {
        var ret = false;
        age = 0;

        var fileTypes = configuration.GetFileTypes();
        if (fileTypes.Any(context.CanCache))
        {
            ret = true;
            age = configuration.GetAge();
        }
        return ret;
    }

    private static bool CanCache(this StaticFileResponseContext context, string fileType)
    {
        var ext = Path.GetExtension(context.File.PhysicalPath) ?? "";
        return fileType.Equals(ext, StringComparison.OrdinalIgnoreCase);
    }

    private static List<string>? _fileTypes;
    private static List<string> GetFileTypes(this IConfiguration configuration)
    {
        _fileTypes ??= GetFilesFromConfiguration();
        return _fileTypes;

        List<string> GetFilesFromConfiguration()
        {
            var cacheSection = configuration.GetSection("Cache-Control");
            return cacheSection.GetSection("Files").Get<List<string>>() ?? [];
        }
    }

    private static int? _age;
    private static int GetAge(this IConfiguration configuration)
    {
        _age ??= GetAgeFromConfiguration();
        return _age.Value;

        int GetAgeFromConfiguration()
        {
            var cacheSection = configuration.GetSection("Cache-Control");
            return cacheSection.GetValue("Max-Age", 1000 * 60 * 10);
        }
    }
}

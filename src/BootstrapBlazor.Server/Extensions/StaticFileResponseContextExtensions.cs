// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;

namespace BootstrapBlazor.Server.Extensions;

internal static class StaticFileResponseContextExtensions
{
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

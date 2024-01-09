// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace BootstrapBlazor.Server.Extensions;

/// <summary>
/// 
/// </summary>
internal static class CacheExtensions
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

        var files = configuration.GetFiles();
        if (files.Any(i => context.CanCache(i)))
        {
            ret = true;
            age = configuration.GetAge();
        }
        return ret;
    }

    private static bool CanCache(this StaticFileResponseContext context, string file)
    {
        var ext = Path.GetExtension(context.File.PhysicalPath) ?? "";
        bool ret = file.Equals(ext, StringComparison.OrdinalIgnoreCase);
        if (ret && ext.Equals(".js", StringComparison.OrdinalIgnoreCase))
        {
            // process javascript file
            ret = false;
            if (context.Context.Request.QueryString.HasValue)
            {
                var paras = QueryHelpers.ParseQuery(context.Context.Request.QueryString.Value);
                ret = paras.ContainsKey("v");
            }
        }
        return ret;
    }

    private static List<string>? _files;
    private static List<string> GetFiles(this IConfiguration configuration)
    {
        _files ??= GetFiles();
        return _files;

        List<string> GetFiles()
        {
            var cacheSection = configuration.GetSection("Cache-Control");
            return cacheSection.GetSection("Files").Get<List<string>>() ?? [];
        }
    }

    private static int? _age;
    private static int GetAge(this IConfiguration configuration)
    {
        _age ??= GetAge();
        return _age.Value;

        int GetAge()
        {
            var cacheSection = configuration.GetSection("Cache-Control");
            return cacheSection.GetValue<int>("Max-Age", 1000 * 60 * 10);
        }
    }
}

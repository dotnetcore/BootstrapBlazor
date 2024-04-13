// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;

namespace BootstrapBlazor.Components;

/// <summary>
/// IWebHostEnvironment 扩展方法
/// </summary>
static class WebHostEnvironmentExtensions
{
    public static string? GetVersionHash(this IWebHostEnvironment env, string? href)
    {
        var ret = "";
        if (!string.IsNullOrEmpty(href))
        {
            var fileInfo = env.WebRootFileProvider.GetFileInfo(href);
            using var readStream = fileInfo.CreateReadStream();
#if NET6_0
            var length = readStream.Length;
            var buffer = new Span<byte>(new byte[length]);
            readStream.Read(buffer);
            var hash = SHA256.HashData(buffer);
#else
            var hash = SHA256.HashData(readStream);
#endif
            ret = WebEncoders.Base64UrlEncode(hash);
        }
        return $"{href}?v={ret}";
    }
}

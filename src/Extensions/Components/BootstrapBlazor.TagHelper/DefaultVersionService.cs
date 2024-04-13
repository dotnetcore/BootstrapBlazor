// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;

namespace BootstrapBlazor.Components;

class DefaultVersionService(IWebHostEnvironment webHost) : IVersionService
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public string GetVersion() => string.Empty;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public string GetVersion(string? url)
    {
        var version = "";
        if (!string.IsNullOrEmpty(url))
        {
            var fileInfo = webHost.WebRootFileProvider.GetFileInfo(url);
            using var readStream = fileInfo.CreateReadStream();
#if NET6_0
            var length = readStream.Length;
            var buffer = new Span<byte>(new byte[length]);
            readStream.Read(buffer);
            var hash = SHA256.HashData(buffer);
#else
            var hash = SHA256.HashData(readStream);
#endif
            version = WebEncoders.Base64UrlEncode(hash);
        }
        return version;

    }
}

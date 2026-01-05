// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.FileProviders;

namespace Microsoft.AspNetCore.Builder;

static class WebApplicationExtensions
{
    public static void UseUploaderStaticFiles(this WebApplication app)
    {
        var uploader = Path.Combine(app.Environment.WebRootPath, "images", "uploader");
        Directory.CreateDirectory(uploader);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(uploader),
            RequestPath = "/images/uploader"
        });
    }

    public static void UseLLMsStaticFiles(this WebApplication app)
    {
        var llms = Path.Combine(app.Environment.WebRootPath, "llms");
        Directory.CreateDirectory(llms);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(llms),
            RequestPath = "/llms",
            OnPrepareResponse = context =>
            {
                context.Context.Response.ContentType = "text/plain; charset=utf-8";
            }
        });

        app.MapGet("/llms.txt", async context =>
        {
            context.Response.Headers.ContentType = "text/plain; charset=utf-8";
            var filePath = Path.Combine(app.Environment.WebRootPath, "llms", "llms.txt");
            if (File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                context.Response.ContentLength = fileInfo.Length;

                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
                await fileStream.CopyToAsync(context.Response.Body);
                return;
            }

            var defaultContent = "no llms.txt";
            context.Response.ContentLength = defaultContent.Length;
            await context.Response.WriteAsync(defaultContent);
        });
    }
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 
/// </summary>
public static class BootstrapBlazorApplicationBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseBootstrapBlazor(this IApplicationBuilder builder)
    {
        // 获得客户端 IP 地址
        builder.UseWhen(context => context.Request.Path.StartsWithSegments("/ip.axd"), app => app.Run(async context =>
        {
            var ip = "";
            var headers = context.Request.Headers;
            if (headers.ContainsKey("X-Forwarded-For"))
            {
                var ips = new List<string>();
                foreach (var xf in headers["X-Forwarded-For"])
                {
                    if (!string.IsNullOrEmpty(xf))
                    {
                        ips.Add(xf);
                    }
                }
                ip = string.Join(";", ips);
            }
            else
            {
                ip = context.Connection.RemoteIpAddress.ToIPv4String();
            }

            context.Response.Headers.Add("Content-Type", new Microsoft.Extensions.Primitives.StringValues("application/json; charset=utf-8"));
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { Id = context.TraceIdentifier, Ip = ip }));
        }));
        return builder;
    }
}

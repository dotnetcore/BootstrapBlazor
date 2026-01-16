// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace Microsoft.Extensions.Configuration;

/// <summary>
///  <para lang="zh">IConfiguration 扩展类</para>
///  <para lang="en">IConfiguration Extensions</para>
/// </summary>
internal static class ConfigurationExtensions
{
    [ExcludeFromCodeCoverage]
    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        services.TryAddSingleton<IConfiguration>(_ =>
        {
            var builder = new ConfigurationBuilder();
            return builder.Build();
        });
        return services;
    }

    public static NameValueCollection GetEnvironmentInformation(this IConfiguration configuration)
    {
        var nv = new NameValueCollection
        {
            ["TimeStamp"] = DateTime.Now.ToString(),
            ["MachineName"] = Environment.MachineName,
            ["AppDomainName"] = AppDomain.CurrentDomain.FriendlyName,

            // <para lang="zh">收集环境变量信息</para>
            // <para lang="en">Collect environment variables</para>
            ["OS"] = GetOS(),
            ["OSArchitecture"] = RuntimeInformation.OSArchitecture.ToString(),
            ["ProcessArchitecture"] = RuntimeInformation.ProcessArchitecture.ToString(),
            ["Framework"] = RuntimeInformation.FrameworkDescription
        };

        // <para lang="zh">当前用户</para>
        // <para lang="en">Current User</para>
        var userName = configuration.GetUserName();
        if (!string.IsNullOrEmpty(userName))
        {
            nv["UserName"] = userName;
        }

        // <para lang="zh">当前环境</para>
        // <para lang="en">Current Environment</para>
        var env = configuration.GetEnvironmentName();
        if (!string.IsNullOrEmpty(env))
        {
            nv["EnvironmentName"] = env;
        }

        // <para lang="zh">IIS Root 路径</para>
        // <para lang="en">IIS Root Path</para>
        var iis = configuration.GetIISPath();
        if (!string.IsNullOrEmpty(iis))
        {
            nv["IISRootPath"] = iis;
        }

        // <para lang="zh">VisualStudio Version</para>
        // <para lang="en">VisualStudio Version</para>
        var vs = configuration.GetVisualStudioVersion();
        if (!string.IsNullOrEmpty(vs))
        {
            nv["VSIDE"] = vs;
        }
        return nv;
    }

    /// <summary>
    ///  <para lang="zh">获得 环境变量中的 OS 属性值</para>
    ///  <para lang="en">Get OS Property Value</para>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static string GetOS()
    {
        string? os = null;
        if (string.IsNullOrEmpty(os))
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                os = RuntimeInformation.OSDescription;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                os = $"OSX";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                os = "FreeBSD";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                os = $"Linux";
            }
        }
        return os ?? "Unknown";
    }

    /// <summary>
    ///  <para lang="zh">获得 环境变量中的 UserName 属性值</para>
    ///  <para lang="en">Get UserName Property Value</para>
    /// </summary>
    /// <param name="config"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string? GetUserName(this IConfiguration config, string? defaultValue = null)
    {
        var userName = config.GetValue<string?>("USERNAME");

        // <para lang="zh">Mac CentOS 系统</para>
        // <para lang="en">Mac CentOS System</para>
        if (string.IsNullOrEmpty(userName))
        {
            userName = config.GetValue<string?>("LOGNAME");
        }
        return userName ?? defaultValue;
    }

    /// <summary>
    ///  <para lang="zh">获得 环境变量中的 ASPNETCORE_ENVIRONMENT 属性值</para>
    ///  <para lang="en">Get ASPNETCORE_ENVIRONMENT Property Value</para>
    /// </summary>
    /// <param name="config"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string? GetEnvironmentName(this IConfiguration config, string? defaultValue = null)
    {
        return config.GetValue<string?>("ASPNETCORE_ENVIRONMENT") ?? defaultValue;
    }

    /// <summary>
    ///  <para lang="zh">获得 环境变量中的 ASPNETCORE_IIS_PHYSICAL_PATH 属性值</para>
    ///  <para lang="en">Get ASPNETCORE_IIS_PHYSICAL_PATH Property Value</para>
    /// </summary>
    /// <param name="config"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string? GetIISPath(this IConfiguration config, string? defaultValue = null)
    {
        return config.GetValue<string?>("ASPNETCORE_IIS_PHYSICAL_PATH") ?? defaultValue;
    }

    /// <summary>
    ///  <para lang="zh">获得 环境变量中的 VisualStudioEdition 属性值</para>
    ///  <para lang="en">Get VisualStudioEdition Property Value</para>
    /// </summary>
    /// <param name="config"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string? GetVisualStudioVersion(this IConfiguration config, string? defaultValue = null)
    {
        var edition = config.GetValue<string?>("VisualStudioEdition");
        var version = config.GetValue<string?>("VisualStudioVersion");

        var ret = $"{edition} {version}";
        if (ret == " ")
        {
            ret = defaultValue;
        }
        return ret;
    }
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Net.Http.Json;

namespace BootstrapBlazor.Shared.Services;

internal class VersionService
{
    private IHttpClientFactory Factory { get; set; }

    public string? Version { get; }

    /// <summary>
    /// 构造方法
    /// </summary>
    public VersionService(IHttpClientFactory factory)
    {
        Factory = factory;
        if (OperatingSystem.IsBrowser())
        {
            Version = typeof(BootstrapComponentBase).Assembly.GetName().Version?.ToString();
        }
        else
        {
            Version = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(BootstrapComponentBase).Assembly.Location).ProductVersion;
        }
    }

    /// <summary>
    /// 获得组件版本号方法
    /// </summary>
    /// <returns></returns>
    public Task<string> GetVersionAsync(string packageName = "bootstrapblazor") => FetchVersionAsync(packageName);

    private async Task<string> FetchVersionAsync(string packageName = "bootstrapblazor")
    {
        var version = "lastest";
        try
        {
            var url = $"https://azuresearch-usnc.nuget.org/query?q={packageName}&prerelease=true&semVerLevel=2.0.0";
            var client = Factory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            var package = await client.GetFromJsonAsync<NugetPackage>(url);
            if (package != null)
            {
                version = package.GetVersion();
            }
        }
        catch { }
        return version;
    }

    private class NugetPackage
    {
        /// <summary>
        /// Data 数据集合
        /// </summary>
        public IEnumerable<NugetPackageData> Data { get; set; } = Array.Empty<NugetPackageData>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetVersion() => Data.FirstOrDefault()?.Version ?? "";
    }

    private class NugetPackageData
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = "";
    }
}

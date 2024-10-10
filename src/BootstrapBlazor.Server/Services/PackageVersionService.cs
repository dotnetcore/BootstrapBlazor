﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Services;

class PackageVersionService
{
    private IHttpClientFactory Factory { get; }

    public string? Version { get; }

    /// <summary>
    /// 构造方法
    /// </summary>
    public PackageVersionService(IHttpClientFactory factory)
    {
        Factory = factory;
        Version = OperatingSystem.IsBrowser()
            ? typeof(BootstrapComponentBase).Assembly.GetName().Version?.ToString()
            : System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(BootstrapComponentBase).Assembly.Location).ProductVersion;

        if (!string.IsNullOrEmpty(Version))
        {
            var index = Version.IndexOf('+');
            if (index > 0)
            {
                Version = Version[..index];
            }
        }
    }

    /// <summary>
    /// 获得组件版本号方法
    /// </summary>
    /// <returns></returns>
    public Task<string> GetVersionAsync(string packageName = "bootstrapblazor") => FetchVersionAsync(packageName);

    private async Task<string> FetchVersionAsync(string packageName = "bootstrapblazor")
    {
        var version = "latest";
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
        catch
        {
            // ignored
        }

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

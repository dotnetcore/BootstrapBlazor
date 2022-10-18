// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Net.Http.Json;

namespace BootstrapBlazor.Shared.Services;

internal class VersionService
{
    private HttpClient Client { get; set; }

    private string PackageVersion { get; set; } = "latest";

    public string? Version { get; }

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="client"></param>
    public VersionService(HttpClient client)
    {
        Client = client;
        Client.Timeout = TimeSpan.FromSeconds(5);

        if (OperatingSystem.IsBrowser())
        {
            Version = typeof(BootstrapComponentBase).Assembly.GetName().Version?.ToString();
        }
        else
        {
            Version = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(BootstrapComponentBase).Assembly.Location).ProductVersion;
        }

        Task.Run(async () =>
        {
            do
            {
                await FetchVersionAsync();

                await Task.Delay(300000);
                PackageVersion = "latest";
            }
            while (true);
        });
    }

    /// <summary>
    /// 获得组件版本号方法
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetVersionAsync(string packageName = "bootstrapblazor")
    {
        PackageVersion = "latest";
        await FetchVersionAsync(packageName);
        return PackageVersion;
    }

    private async Task FetchVersionAsync(string packageName = "bootstrapblazor")
    {
        if (PackageVersion == "latest")
        {
            try
            {
                var url = $"https://azuresearch-usnc.nuget.org/query?q={packageName}&prerelease=true&semVerLevel=2.0.0";
                var package = await Client.GetFromJsonAsync<NugetPackage>(url);
                if (package != null)
                {
                    PackageVersion = package.GetVersion();
                }
            }
            catch { }
        }
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

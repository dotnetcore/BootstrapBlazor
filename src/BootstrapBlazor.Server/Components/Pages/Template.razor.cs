// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// 
/// </summary>
public sealed partial class Template
{
    /// <summary>
    /// 获得/设置 版本号字符串
    /// </summary>
    private string Version { get; set; } = "*";

    private string TemplateUrl => $"{WebsiteOption.Value.GiteeRepositoryUrl}/wikis/%E9%A1%B9%E7%9B%AE%E6%A8%A1%E6%9D%BF%E4%BD%BF%E7%94%A8%E6%95%99%E7%A8%8B?sort_id=3059284";

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync("Bootstrap.Blazor.Templates");
    }
}

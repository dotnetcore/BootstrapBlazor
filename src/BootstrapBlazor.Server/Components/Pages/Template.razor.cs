// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

    private string TemplateUrl => $"{WebsiteOption.CurrentValue.GiteeRepositoryUrl}/wikis/%E9%A1%B9%E7%9B%AE%E6%A8%A1%E6%9D%BF%E4%BD%BF%E7%94%A8%E6%95%99%E7%A8%8B?sort_id=3059284";

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync("Bootstrap.Blazor.Templates");
    }
}

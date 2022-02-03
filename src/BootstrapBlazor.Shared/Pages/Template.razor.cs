// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Pages;

/// <summary>
/// 
/// </summary>
public sealed partial class Template
{
    /// <summary>
    /// 获得/设置 版本号字符串
    /// </summary>
    private string Version { get; set; } = "*";

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync("Bootstrap.Blazor.Templates");
    }
}

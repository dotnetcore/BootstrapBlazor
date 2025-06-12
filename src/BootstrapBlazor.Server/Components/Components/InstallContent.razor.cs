// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class InstallContent
{
    /// <summary>
    /// 获得/设置 版本号字符串
    /// </summary>
    private string Version { get; set; } = "latest";

    /// <summary>
    ///
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "服务器端 Blazor 安装教程";

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string HostFile { get; set; } = "Pages/_Host.cshtml";

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? ChooseTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? SheetTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? ScriptsTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? ServicesTemplate { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Parameter]
    public RenderFragment? RootTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync();
    }
}

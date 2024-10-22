// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Icons;

/// <summary>
/// ByteDanceIconList 组件
/// </summary>
[JSModuleAutoLoader("Samples/Icons/IconParkList.razor.js")]
public partial class IconParkList : IAsyncDisposable
{
    private string? ClassString => CssBuilder.Default("icon-list")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 拷贝成功提示文字
    /// </summary>
    [Parameter]
    public string? CopiedTooltipText { get; set; }
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

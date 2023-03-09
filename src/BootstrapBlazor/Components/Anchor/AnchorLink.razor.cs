// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
[JSModuleAutoLoader]
public partial class AnchorLink
{
    /// <summary>
    /// 获得/设置 组件 Text 显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 组件 拷贝成功后 显示文字
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    /// <summary>
    /// 获得/设置 锚点图标 默认 fa-solid fa-link
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    [Inject]
    [NotNull]
    private IOptions<IconMapperOptions>? IconOptions { get; set; }

    private string? IconString => CssBuilder.Default("anchor-link-icon")
        .AddClass(Icon)
        .Build();

    private string? ClassString => CssBuilder.Default("anchor-link")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconOptions.Value.GetIcon(ComponentIcons.AnchorLinkIcon, "fa-solid fa-link");
    }
}

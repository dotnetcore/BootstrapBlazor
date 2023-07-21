// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class DemoBlock
{
    /// <summary>
    /// 获得/设置 组件 Title 属性
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 组件说明信息
    /// </summary>
    [Parameter]
    public string Introduction { get; set; } = "未设置";

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 示例代码片段 默认 null 未设置
    /// </summary>
    [Parameter]
    public Type? Demo { get; set; }

    /// <summary>
    /// 获得/设置 是否显示代码块 默认 true 显示
    /// </summary>
    [Parameter]
    public bool? ShowCode { get; set; }

    /// <summary>
    /// 获得/设置 Tooltip 提示信息文本
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DemoBlock>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 友好链接锚点名称
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    private string BlockTitle => Name ?? Title;

    private string? DemoString => Demo?.ToString().Replace("BootstrapBlazor.Shared.", "");

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Title ??= Localizer[nameof(Title)];
        TooltipText ??= Localizer[nameof(TooltipText)];

        if (!ShowCode.HasValue && Demo != null)
        {
            ShowCode = true;
        }
    }

    private RenderFragment RenderChildContent => builder =>
    {
        builder.AddContent(0, ChildContent);

        if (Demo != null)
        {
            builder.OpenComponent(1, Demo);
            builder.CloseComponent();
        }
    };
}

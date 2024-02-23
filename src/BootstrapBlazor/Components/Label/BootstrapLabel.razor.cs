// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapLabel 组件
/// </summary>
public partial class BootstrapLabel
{
    /// <summary>
    /// 获得/设置 组件值 显示文本 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Value { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Tooltip 多用于标签文字过长导致裁减时使用 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    private bool _showTooltip;

    private string? ClassString => CssBuilder.Default("form-label")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (ShowLabelTooltip.HasValue)
        {
            _showTooltip = ShowLabelTooltip.Value;
        }
        Value ??= "";
    }
}

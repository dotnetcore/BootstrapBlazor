// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class BootstrapInputGroupLabel
{
    private string? ClassString => CssBuilder.Default()
        .AddClass("input-group-text", IsInnerLabel)
        .AddClass("form-label", !IsInnerLabel)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 显示名称 已弃用，请使用 DisplayText
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 DisplayText")]
    public string? Text { get => DisplayText; set => DisplayText = value; }

    private bool IsInnerLabel { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        IsInnerLabel = InputGroup != null;
    }
}

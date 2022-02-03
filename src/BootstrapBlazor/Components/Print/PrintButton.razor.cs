// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class PrintButton
{
    /// <summary>
    /// 获得/设置 预览模板地址 默认为空
    /// </summary>
    [Parameter]
    public string? PreviewUrl { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PrintButton>? Localizer { get; set; }

    private string? Target { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        // 不需要走 base.OnInitialized 方法
        ButtonIcon = Icon;
        Text ??= Localizer[nameof(Text)];
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        // 不需要走 base.OnParametersSet 方法
        AdditionalAttributes ??= new Dictionary<string, object>();
        if (string.IsNullOrEmpty(PreviewUrl))
        {
            AdditionalAttributes.Add("onclick", "$.bb_printview(this)");
            Target = null;
        }
        else
        {
            AdditionalAttributes.Remove("onclick", out _);
            Target = "_blank";
        }

        if (string.IsNullOrEmpty(ButtonIcon))
        {
            ButtonIcon = "fa fa-print";
        }
    }
}

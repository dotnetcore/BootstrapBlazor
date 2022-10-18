// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
[JSModuleAutoLoader("icon-dialog")]
public partial class IconDialog
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? IconName { get; set; }

    /// <summary>
    /// 获得/设置 Label 显示文字
    /// </summary>
    [Parameter]
    public string? LabelText { get; set; }

    /// <summary>
    /// 获得/设置 Label 显示文字
    /// </summary>
    [Parameter]
    public string? LabelFullText { get; set; }

    /// <summary>
    /// 获得/设置 拷贝按钮显示文字
    /// </summary>
    [Parameter]
    public string? ButtonText { get; set; }

    /// <summary>
    /// 获得/设置 拷贝成功提示文字
    /// </summary>
    [Parameter]
    public string? CopiedTooltipText { get; set; }

    private string IconFullName => $"<i class=\"{IconName}\" aria-hidden=\"true\"></i>";

    [Inject]
    [NotNull]
    private IStringLocalizer<IconDialog>? Localizer { get; set; }

    private IEnumerable<SelectedItem> Items { get; } = new List<SelectedItem>()
    {
        new("solid", "Solid"),
        new("regular", "Regular")
    };

    private string IconStyle { get; set; } = "solid";

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        LabelText ??= Localizer[nameof(LabelText)];
        LabelFullText ??= Localizer[nameof(LabelFullText)];
        ButtonText ??= Localizer[nameof(ButtonText)];
        CopiedTooltipText ??= Localizer[nameof(CopiedTooltipText)];

        IconName = IconName?.Replace("fas", "fa-solid", StringComparison.OrdinalIgnoreCase)
            .Replace("far", "fa-regular", StringComparison.OrdinalIgnoreCase);
    }

    private Task OnValueChanged(string val)
    {
        if(val == "solid")
        {
            IconName = IconName.Replace("fa-regular", "fa-solid");
        }
        else
        {
            IconName = IconName.Replace("fa-solid", "fa-regular");
        }
        return Task.CompletedTask;
    }
}

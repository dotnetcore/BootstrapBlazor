// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TimePicker 组件
/// </summary>
public partial class TimePicker
{
    /// <summary>
    /// 获得/设置 取消按钮显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CancelButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确定按钮显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示秒 默认为 true
    /// </summary>
    [Parameter]
    [NotNull]
    public bool HasSeconds { get; set; } = true;

    /// <summary>
    /// 获得/设置 取消按钮回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调委托
    /// </summary>
    [Parameter]
    public Func<TimeSpan, Task>? OnConfirm { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 当前时间
    /// </summary>
    private TimeSpan CurrentTime { get; set; }

    /// <summary>
    /// 获得/设置 样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-time-picker")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CurrentTime = Value;
        CancelButtonText ??= Localizer[nameof(CancelButtonText)];
        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
    }

    /// <summary>
    /// 点击取消按钮回调此方法
    /// </summary>
    private async Task OnClickClose()
    {
        CurrentTime = Value;
        if (OnClose != null)
        {
            await OnClose();
        }
    }

    /// <summary>
    /// 点击确认按钮时回调此方法
    /// </summary>
    private async Task OnClickConfirm()
    {
        Value = CurrentTime;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (OnConfirm != null)
        {
            await OnConfirm(Value);
        }
    }
}

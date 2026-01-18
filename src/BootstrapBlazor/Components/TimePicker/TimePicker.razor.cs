// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TimePicker 组件</para>
/// <para lang="en">TimePicker Component</para>
/// </summary>
public partial class TimePicker
{
    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮显示文字</para>
    /// <para lang="en">Gets or sets the cancel button display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CancelButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确定按钮显示文字</para>
    /// <para lang="en">Gets or sets the confirm button display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示秒，默认为 true</para>
    /// <para lang="en">Gets or sets whether to display seconds. Default is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public bool HasSeconds { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮回调委托</para>
    /// <para lang="en">Gets or sets the cancel button callback delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮回调委托</para>
    /// <para lang="en">Gets or sets the confirm button callback delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TimeSpan, Task>? OnConfirm { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

    private TimeSpan CurrentTime { get; set; }

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

    private async Task OnClickClose()
    {
        CurrentTime = Value;
        if (OnClose != null)
        {
            await OnClose();
        }
    }

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

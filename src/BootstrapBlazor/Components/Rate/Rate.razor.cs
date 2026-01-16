// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Rate 组件</para>
/// <para lang="en">Rate Component</para>
/// </summary>
public partial class Rate
{
    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Style Collection</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("rate")
        .AddClass("text-nowrap", !IsWrap)
        .AddClass("disabled", IsDisable)
        .AddClass("readonly", IsReadonly)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(int i) => CssBuilder.Default("rate-item")
        .AddClass("is-on", Value >= i)
        .Build();

    /// <summary>
    /// <para lang="zh">判断是否显示部分星级</para>
    /// <para lang="en">Determine whether to show partial stars</para>
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private bool IsPartialStar(int i) => (Value + 1 - i) is > 0 and < 1;

    private string GetIcon(int i) => Value >= i ? StarIcon : UnStarIcon;

    private string GetWidthStyle(int i) => $"width: {Math.Round(Value + 1 - i, 2) * 100}%;";

    /// <summary>
    /// <para lang="zh">获得/设置 选中图标</para>
    /// <para lang="en">Get/Set Checked Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? StarIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 未选中图标</para>
    /// <para lang="en">Get/Set Unchecked Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? UnStarIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件值</para>
    /// <para lang="en">Get/Set Value</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public double Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认为 false</para>
    /// <para lang="en">Get/Set Whether disabled. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>禁用模式下图标颜色为灰色，不可点击</remarks>
    [Parameter]
    public bool IsDisable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否只读 默认为 false</para>
    /// <para lang="en">Get/Set Whether readonly. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>只读情况下图标为彩色，仅不可点击</remarks>
    [Parameter]
    public bool IsReadonly { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁止换行 默认为 true</para>
    /// <para lang="en">Get/Set Whether to disable wrap. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsWrap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Value 默认为 false</para>
    /// <para lang="en">Get/Set Whether to show Value. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子项模板</para>
    /// <para lang="en">Get/Set Item Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<double>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件值变化时回调委托</para>
    /// <para lang="en">Get/Set Value Changed Callback Delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<double> ValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件值变化时回调委托</para>
    /// <para lang="en">Get/Set Value Changed Callback Delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<double, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最大值 默认 5</para>
    /// <para lang="en">Get/Set Max Value. Default 5</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Max { get; set; } = 5;

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        StarIcon ??= IconTheme.GetIconByKey(ComponentIcons.RateStarIcon);
        UnStarIcon ??= IconTheme.GetIconByKey(ComponentIcons.RateUnStarIcon);

        if (Max < 1)
        {
            Max = 5;
        }

        if (Value < 0)
        {
            Value = 0;
        }
    }

    private async Task OnClickItem(int value)
    {
        Value = value;
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        else
        {
            StateHasChanged();
        }
    }
}

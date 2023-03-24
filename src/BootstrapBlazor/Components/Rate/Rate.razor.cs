// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Rate 组件
/// </summary>
public partial class Rate
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassString => CssBuilder.Default("rate")
        .AddClass("disabled", IsDisable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(int i) => CssBuilder.Default("rate-item")
        .AddClass("is-on", Value >= i)
        .Build();

    private string? GetIcon(int i) => Value >= i ? StarIcon : UnStarIcon;

    /// <summary>
    /// 获得/设置 选中图标 内置 fa-solid fa-star
    /// </summary>
    [Parameter]
    [NotNull]
    public string? StarIcon { get; set; }

    /// <summary>
    /// 获得/设置 未选中图标 内置 fa-regular fa-star
    /// </summary>
    [Parameter]
    [NotNull]
    public string? UnStarIcon { get; set; }

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public int Value { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认为 false
    /// </summary>
    [Parameter]
    public bool IsDisable { get; set; }

    /// <summary>
    /// 获得/设置 子项模板
    /// </summary>
    [Parameter]
    public RenderFragment<int>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化时回调委托
    /// </summary>
    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化时回调委托
    /// </summary>
    [Parameter]
    public Func<int, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 最大值 默认 5
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

        StarIcon ??= IconTheme.GetIconByKey(ComponentIcons.StarIcon);
        UnStarIcon ??= IconTheme.GetIconByKey(ComponentIcons.UnStarIcon);

        if (Max < 1)
        {
            Max = 5;
        }

        if (Value < 1)
        {
            Value = 1;
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

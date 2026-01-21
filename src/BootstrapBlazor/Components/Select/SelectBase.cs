// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SelectBase 组件基类</para>
/// <para lang="en">SelectBase component base class</para>
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public abstract class SelectBase<TValue> : PopoverSelectBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 颜色，默认值为 <see cref="Color.None"/>（无颜色）</para>
    /// <para lang="en">Gets or sets the color. The default is <see cref="Color.None"/> (no color).</para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索框，默认值为 <c>false</c></para>
    /// <para lang="en">Gets or sets a value indicating whether to show the search box. The default is <c>false</c>.</para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索图标</para>
    /// <para lang="en">Gets or sets the search icon.</para>
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索加载图标</para>
    /// <para lang="en">Gets or sets the search loading icon.</para>
    /// </summary>
    [Parameter]
    public string? SearchLoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索文本</para>
    /// <para lang="en">Gets or sets the search text.</para>
    /// </summary>
    [NotNull]
    protected string? SearchText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 未找到搜索结果时显示的文本</para>
    /// <para lang="en">Gets or sets the text to display when no search results are found.</para>
    /// </summary>
    [Parameter]
    public string? NoSearchDataText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉图标，默认值为 "fa-solid fa-angle-up"</para>
    /// <para lang="en">Gets or sets the dropdown icon. The default is "fa-solid fa-angle-up".</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容是否为 <see cref="MarkupString"/>，默认值为 <c>false</c></para>
    /// <para lang="en">Gets or sets a value indicating whether the content is a <see cref="MarkupString"/>. The default is <c>false</c>.</para>
    /// </summary>
    [Parameter]
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 字符串比较规则，默认值为 <see cref="StringComparison.OrdinalIgnoreCase"/></para>
    /// <para lang="en">Gets or sets the string comparison rule. The default is <see cref="StringComparison.OrdinalIgnoreCase"/>.</para>
    /// </summary>
    [Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <para lang="zh">获得/设置 分组项模板</para>
    /// <para lang="en">Gets or sets the group item template.</para>
    /// </summary>
    [Parameter]
    public RenderFragment<string>? GroupItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 滚动行为，默认值为 <see cref="ScrollIntoViewBehavior.Smooth"/></para>
    /// <para lang="en">Gets or sets the scroll behavior. The default is <see cref="ScrollIntoViewBehavior.Smooth"/>.</para>
    /// </summary>
    [Parameter]
    public ScrollIntoViewBehavior ScrollIntoViewBehavior { get; set; } = ScrollIntoViewBehavior.Smooth;

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="IIconTheme"/> 服务实例</para>
    /// <para lang="en">Gets or sets the <see cref="IIconTheme"/> service instance.</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 placeholder 文本</para>
    /// <para lang="en">Gets or sets the placeholder text.</para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用虚拟滚动，默认值为 false</para>
    /// <para lang="en">Gets or sets whether virtual scrolling is enabled. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 虚拟滚动的行高度，默认值为 33</para>
    /// <para lang="en">Gets or sets the row height for virtual scrolling. Default is 33.</para>
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 33f;

    /// <summary>
    /// <para lang="zh">获得/设置 虚拟滚动的超量显示数量，默认值为 4</para>
    /// <para lang="en">Gets or sets the overscan count for virtual scrolling. Default is 4.</para>
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 4;

    /// <summary>
    /// <para lang="zh">获得/设置 当清除按钮被点击时的回调方法，默认值为 null</para>
    /// <para lang="en">Gets or sets the callback method when the clear button is clicked. Default is null.</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧清除图标，默认值为 fa-solid fa-angle-up</para>
    /// <para lang="en">Gets or sets the right-side clear icon. Default is fa-solid fa-angle-up.</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择组件是否可以清除，默认值为 false</para>
    /// <para lang="en">Gets or sets whether the select component is clearable. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// <para lang="zh">获得 搜索图标的字符串，默认值为 "图标 search-图标" 类</para>
    /// <para lang="en">Gets the search icon string with default "icon search-icon" class.</para>
    /// </summary>
    protected string? SearchIconString => CssBuilder.Default("icon search-icon")
        .AddClass(SearchIcon)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 自定义类的字符串</para>
    /// <para lang="en">Gets the custom class string.</para>
    /// </summary>
    protected override string? CustomClassString => CssBuilder.Default()
        .AddClass("select", IsPopover)
        .AddClass(base.CustomClassString)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 附加类的字符串</para>
    /// <para lang="en">Gets the append class string.</para>
    /// </summary>
    protected string? AppendClassString => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 清除图标的类的字符串</para>
    /// <para lang="en">Gets the clear icon class string.</para>
    /// </summary>
    protected string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 搜索加载图标的类的字符串</para>
    /// <para lang="en">Gets the SearchLoadingIcon icon class string.</para>
    /// </summary>
    protected string? SearchLoadingIconString => CssBuilder.Default("icon searching-icon")
        .AddClass(SearchLoadingIcon)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SearchIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectSearchIcon);
        SearchLoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.SearchButtonLoadingIcon);
    }

    /// <summary>
    /// <para lang="zh">显示下拉菜单</para>
    /// <para lang="en">Shows the dropdown.</para>
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Show() => InvokeVoidAsync("show", Id);

    /// <summary>
    /// <para lang="zh">隐藏下拉菜单</para>
    /// <para lang="en">Hides the dropdown.</para>
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Hide() => InvokeVoidAsync("hide", Id);

    private bool IsNullable() => !ValueType.IsValueType || NullableUnderlyingType != null;

    /// <summary>
    /// <para lang="zh">获得 是否显示清除按钮</para>
    /// <para lang="en">Gets whether show the clear button.</para>
    /// </summary>
    protected bool GetClearable() => IsClearable && !IsDisabled && IsNullable();

    /// <summary>
    /// <para lang="zh">清除搜索文本</para>
    /// <para lang="en">Clears the search text.</para>
    /// </summary>
    public void ClearSearchText() => SearchText = null;

    /// <summary>
    /// <para lang="zh">清除选中的值</para>
    /// <para lang="en">Clears the selected value.</para>
    /// </summary>
    protected virtual async Task OnClearValue()
    {
        if (ShowSearch)
        {
            ClearSearchText();
        }
        if (OnClearAsync != null)
        {
            await OnClearAsync();
        }
        CurrentValue = default;
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SelectBase component base class
///</para>
/// <para lang="en">SelectBase component base class
///</para>
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public abstract class SelectBase<TValue> : PopoverSelectBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 the 颜色. default is <see cref="Color.None"/> (no 颜色).
    ///</para>
    /// <para lang="en">Gets or sets the color. The default is <see cref="Color.None"/> (no color).
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 a value indicating 是否 to show the search box. default is <c>false</c>.
    ///</para>
    /// <para lang="en">Gets or sets a value indicating whether to show the search box. The default is <c>false</c>.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the search 图标.
    ///</para>
    /// <para lang="en">Gets or sets the search icon.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the search loading 图标.
    ///</para>
    /// <para lang="en">Gets or sets the search loading icon.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? SearchLoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the search text.
    ///</para>
    /// <para lang="en">Gets or sets the search text.
    ///</para>
    /// </summary>
    [NotNull]
    protected string? SearchText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the text to 显示 when no search results are found.
    ///</para>
    /// <para lang="en">Gets or sets the text to display when no search results are found.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? NoSearchDataText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the dropdown 图标. default is "fa-solid fa-angle-up".
    ///</para>
    /// <para lang="en">Gets or sets the dropdown icon. The default is "fa-solid fa-angle-up".
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 a value indicating 是否 the 内容 is a <see cref="MarkupString"/>. default is <c>false</c>.
    ///</para>
    /// <para lang="en">Gets or sets a value indicating whether the content is a <see cref="MarkupString"/>. The default is <c>false</c>.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the string comparison rule. default is <see cref="StringComparison.OrdinalIgnoreCase"/>.
    ///</para>
    /// <para lang="en">Gets or sets the string comparison rule. The default is <see cref="StringComparison.OrdinalIgnoreCase"/>.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <para lang="zh">获得/设置 the group item 模板.
    ///</para>
    /// <para lang="en">Gets or sets the group item template.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<string>? GroupItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the scroll behavior. default is <see cref="ScrollIntoViewBehavior.Smooth"/>.
    ///</para>
    /// <para lang="en">Gets or sets the scroll behavior. The default is <see cref="ScrollIntoViewBehavior.Smooth"/>.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public ScrollIntoViewBehavior ScrollIntoViewBehavior { get; set; } = ScrollIntoViewBehavior.Smooth;

    /// <summary>
    /// <para lang="zh">获得/设置 the <see cref="IIconTheme"/> service 实例.
    ///</para>
    /// <para lang="en">Gets or sets the <see cref="IIconTheme"/> service instance.
    ///</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the placeholder text.
    ///</para>
    /// <para lang="en">Gets or sets the placeholder text.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 virtual scrolling is enabled. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether virtual scrolling is enabled. Default is false.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the row 高度 for virtual scrolling. 默认为 33.
    ///</para>
    /// <para lang="en">Gets or sets the row height for virtual scrolling. Default is 33.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 33f;

    /// <summary>
    /// <para lang="zh">获得/设置 the overscan count for virtual scrolling. 默认为 4.
    ///</para>
    /// <para lang="en">Gets or sets the overscan count for virtual scrolling. Default is 4.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 4;

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调方法 when the clear 按钮 is clicked. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets the callback method when the clear button is clicked. Default is null.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the right-side clear 图标. 默认为 fa-solid fa-angle-up.
    ///</para>
    /// <para lang="en">Gets or sets the right-side clear icon. Default is fa-solid fa-angle-up.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the select component is clearable. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether the select component is clearable. Default is false.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// <para lang="zh">获得 the search 图标 string with default "图标 search-图标" class.
    ///</para>
    /// <para lang="en">Gets the search icon string with default "icon search-icon" class.
    ///</para>
    /// </summary>
    protected string? SearchIconString => CssBuilder.Default("icon search-icon")
        .AddClass(SearchIcon)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 the custom class string.
    ///</para>
    /// <para lang="en">Gets the custom class string.
    ///</para>
    /// </summary>
    protected override string? CustomClassString => CssBuilder.Default()
        .AddClass("select", IsPopover)
        .AddClass(base.CustomClassString)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 the append class string.
    ///</para>
    /// <para lang="en">Gets the append class string.
    ///</para>
    /// </summary>
    protected string? AppendClassString => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 the clear 图标 class string.
    ///</para>
    /// <para lang="en">Gets the clear icon class string.
    ///</para>
    /// </summary>
    protected string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 the SearchLoadingIcon 图标 class string.
    ///</para>
    /// <para lang="en">Gets the SearchLoadingIcon icon class string.
    ///</para>
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
    /// <para lang="zh">Shows the dropdown.
    ///</para>
    /// <para lang="en">Shows the dropdown.
    ///</para>
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Show() => InvokeVoidAsync("show", Id);

    /// <summary>
    /// <para lang="zh">Hides the dropdown.
    ///</para>
    /// <para lang="en">Hides the dropdown.
    ///</para>
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Hide() => InvokeVoidAsync("hide", Id);

    private bool IsNullable() => !ValueType.IsValueType || NullableUnderlyingType != null;

    /// <summary>
    /// <para lang="zh">获得 是否 show the clear 按钮.
    ///</para>
    /// <para lang="en">Gets whether show the clear button.
    ///</para>
    /// </summary>
    /// <returns></returns>
    protected bool GetClearable() => IsClearable && !IsDisabled && IsNullable();

    /// <summary>
    /// <para lang="zh">Clears the search text.
    ///</para>
    /// <para lang="en">Clears the search text.
    ///</para>
    /// </summary>
    public void ClearSearchText() => SearchText = null;

    /// <summary>
    /// <para lang="zh">Clears the selected value.
    ///</para>
    /// <para lang="en">Clears the selected value.
    ///</para>
    /// </summary>
    /// <returns></returns>
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

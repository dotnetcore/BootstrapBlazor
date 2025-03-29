// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// SelectBase component base class
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public abstract class SelectBase<TValue> : PopoverSelectBase<TValue>
{
    /// <summary>
    /// Gets or sets the color. The default is <see cref="Color.None"/> (no color).
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the search box. The default is <c>false</c>.
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// Gets or sets the search icon.
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// Gets or sets the search loading icon.
    /// </summary>
    [Parameter]
    public string? SearchLoadingIcon { get; set; }

    /// <summary>
    /// Gets or sets the search text.
    /// </summary>
    [NotNull]
    protected string? SearchText { get; set; }

    /// <summary>
    /// Gets or sets the text to display when no search results are found.
    /// </summary>
    [Parameter]
    public string? NoSearchDataText { get; set; }

    /// <summary>
    /// Gets or sets the dropdown icon. The default is "fa-solid fa-angle-up".
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the content is a <see cref="MarkupString"/>. The default is <c>false</c>.
    /// </summary>
    [Parameter]
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// Gets or sets the string comparison rule. The default is <see cref="StringComparison.OrdinalIgnoreCase"/>.
    /// </summary>
    [Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// Gets or sets the group item template.
    /// </summary>
    [Parameter]
    public RenderFragment<string>? GroupItemTemplate { get; set; }

    /// <summary>
    /// Gets or sets the scroll behavior. The default is <see cref="ScrollIntoViewBehavior.Smooth"/>.
    /// </summary>
    [Parameter]
    public ScrollIntoViewBehavior ScrollIntoViewBehavior { get; set; } = ScrollIntoViewBehavior.Smooth;

    /// <summary>
    /// Gets or sets the <see cref="IIconTheme"/> service instance.
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// Gets or sets whether virtual scrolling is enabled. Default is false.
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// Gets or sets the row height for virtual scrolling. Default is 33.
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 33f;

    /// <summary>
    /// Gets or sets the overscan count for virtual scrolling. Default is 4.
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 4;

    /// <summary>
    /// Gets or sets the callback method when the clear button is clicked. Default is null.
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    /// <summary>
    /// Gets or sets the right-side clear icon. Default is fa-solid fa-angle-up.
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// Gets or sets whether the select component is clearable. Default is false.
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// Gets the search icon string with default "icon search-icon" class.
    /// </summary>
    protected string? SearchIconString => CssBuilder.Default("icon search-icon")
        .AddClass(SearchIcon)
        .Build();

    /// <summary>
    /// Gets the custom class string.
    /// </summary>
    protected override string? CustomClassString => CssBuilder.Default()
        .AddClass("select", IsPopover)
        .AddClass(base.CustomClassString)
        .Build();

    /// <summary>
    /// Gets the append class string.
    /// </summary>
    protected string? AppendClassString => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// Gets the clear icon class string.
    /// </summary>
    protected string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// Gets the SearchLoadingIcon icon class string.
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
    /// Shows the dropdown.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Show() => InvokeVoidAsync("show", Id);

    /// <summary>
    /// Hides the dropdown.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Hide() => InvokeVoidAsync("hide", Id);

    private bool IsNullable() => !ValueType.IsValueType || NullableUnderlyingType != null;

    /// <summary>
    /// Gets whether show the clear button.
    /// </summary>
    /// <returns></returns>
    protected bool GetClearable() => IsClearable && !IsDisabled && IsNullable();

    /// <summary>
    /// Clears the search text.
    /// </summary>
    public void ClearSearchText() => SearchText = null;

    /// <summary>
    /// Clears the selected value.
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

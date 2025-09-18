// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoComplete component
/// </summary>
public partial class AutoComplete
{
    /// <summary>
    /// Gets or sets the collection of matching data obtained by inputting a string
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<string>? Items { get; set; }

    /// <summary>
    /// Gets or sets custom collection filtering rules, default is null
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<string>>>? OnCustomFilter { get; set; }

    /// <summary>
    /// Gets or sets the icon
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the loading icon
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// Gets or sets the number of items to display when matching data
    /// </summary>
    [Parameter]
    [NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    /// Gets or sets whether to enable fuzzy search, default is false
    /// </summary>
    [Parameter]
    public bool IsLikeMatch { get; set; }

    /// <summary>
    /// Gets or sets whether to ignore case when matching, default is true
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to expand the dropdown candidate menu when focused, default is true
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to show the no matching data option, default is true
    /// </summary>
    [Parameter]
    public bool ShowNoDataTip { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the select component is clearable. Default is false.
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// Gets or sets the right-side clear icon. Default is fa-solid fa-angle-up.
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// IStringLocalizer service instance
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    /// <summary>
    /// Gets the string setting for automatically displaying the dropdown when focused
    /// </summary>
    private string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? "true" : null;

    private List<string>? _filterItems;

    [NotNull]
    private RenderTemplate? _dropdown = null;

    private string? ClassString => CssBuilder.Default("auto-complete")
        .AddClass("is-clearable", IsClearable)
        .Build();

    /// <summary>
    /// Gets the clear icon class string.
    /// </summary>
    private string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        SkipRegisterEnterEscJSInvoke = true;

        Items ??= [];

        if (!string.IsNullOrEmpty(Value))
        {
            _filterItems = GetFilterItemsByValue(Value);
            if (DisplayCount != null)
            {
                _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoCompleteIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectClearIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Value);

    /// <summary>
    /// Gets whether show the clear button.
    /// </summary>
    /// <returns></returns>
    private bool GetClearable() => IsClearable && !IsDisabled;

    /// <summary>
    /// Callback method when a candidate item is clicked
    /// </summary>
    private async Task OnClickItem(string val)
    {
        CurrentValue = val;

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }

        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }

        await TriggerFilter(val);

        // 使用脚本更新 input 值
        await InvokeVoidAsync("setValue", Id, val);
    }

    private List<string> Rows => _filterItems ?? [.. Items];

    /// <summary>
    /// TriggerFilter method
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public async Task TriggerFilter(string val)
    {
        if (OnCustomFilter != null)
        {
            var items = await OnCustomFilter(val);
            _filterItems = [.. items];
        }
        else if (string.IsNullOrEmpty(val))
        {
            _filterItems = [.. Items];
        }
        else
        {
            _filterItems = GetFilterItemsByValue(val);
        }

        if (DisplayCount != null)
        {
            _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
        }

        // only render the dropdown menu
        _dropdown.Render();
    }

    private List<string> GetFilterItemsByValue(string val)
    {
        var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        var items = IsLikeMatch
            ? Items.Where(s => s.Contains(val, comparison))
            : Items.Where(s => s.StartsWith(val, comparison));
        return [.. items];
    }
}

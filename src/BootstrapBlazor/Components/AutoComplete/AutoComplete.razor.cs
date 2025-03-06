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
    /// Gets the component style
    /// </summary>
    private string? ClassString => CssBuilder.Default("auto-complete")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

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

    /// <summary>
    /// Tracks the current user input to prevent it from being overwritten
    /// </summary>
    private string _currentUserInput = string.Empty;

    /// <summary>
    /// Flag to track whether we're handling debounced filtering
    /// </summary>
    private bool _isFiltering = false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        SkipRegisterEnterEscJSInvoke = true;
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

        Items ??= [];

        // Initialize _currentUserInput with current value if it hasn't been set yet
        if (string.IsNullOrEmpty(_currentUserInput) && !string.IsNullOrEmpty(CurrentValueAsString))
        {
            _currentUserInput = CurrentValueAsString;
        }
    }

    /// <summary>
    /// Callback method when a candidate item is clicked
    /// </summary>
    private async Task OnClickItem(string val)
    {
        // Update both the CurrentValue and _currentUserInput when an item is clicked
        _currentUserInput = val;
        CurrentValue = val;

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    private List<string> Rows => _filterItems ?? [.. Items];

    /// <summary>
    /// TriggerFilter method
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public override async Task TriggerFilter(string val)
    {
        try
        {
            _isFiltering = true;
            // Update our tracking variable
            _currentUserInput = val;

            // Filter items as usual
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
                var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                var items = IsLikeMatch
                    ? Items.Where(s => s.Contains(val, comparison))
                    : Items.Where(s => s.StartsWith(val, comparison));
                _filterItems = [.. items];
            }

            if (DisplayCount != null)
            {
                _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
            }

            // Update the bound value to match the user input, triggering proper value change notifications
            // This ensures OnValueChanged is triggered while preventing visual disruption
            CurrentValue = val;

            // Refresh UI
            StateHasChanged();
        }
        finally
        {
            _isFiltering = false;
        }
    }

    /// <summary>
    /// TriggerChange method
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public override Task TriggerChange(string val)
    {
        // Update our tracking variable
        _currentUserInput = val;

        // Update component value and trigger change notifications
        if (CurrentValue != val)
        {
            CurrentValue = val;
            if (!ValueChanged.HasDelegate)
            {
                StateHasChanged();
            }
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Override CurrentValueAsString to return the current user input
    /// </summary>
    protected override string? FormatValueAsString(string? value)
    {
        // During filtering operations, use what the user is actually typing
        if (_isFiltering)
        {
            return _currentUserInput;
        }

        // In non-filtering scenarios, sync our tracked value with the component value
        if (!string.IsNullOrEmpty(value) && _currentUserInput != value)
        {
            _currentUserInput = value;
        }

        return base.FormatValueAsString(value);
    }
}

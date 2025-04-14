// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components; // Added for Parameter/Inject etc.
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging; // Added for ILogger (optional, for handling ex)
using Microsoft.JSInterop; // Added for JSInvokable/IJSRuntime
using System; // Added for Func/StringComparison/Exception
using System.Collections.Generic; // Added for List<>
using System.Diagnostics.CodeAnalysis; // Added for NotNull
using System.Linq; // Added for Linq methods (Ensure this is present for .Take())
using System.Threading.Tasks; // Added for Task

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

    // --- NEW PARAMETER ---
    /// <summary>
    /// Gets or sets whether to populate the dropdown list with initial items
    /// immediately after the component first renders.
    /// Defaults to true. Set to false to only populate when the user interacts (types or focuses, depending on other settings).
    /// </summary>
    [Parameter]
    public bool PopulateListOnFirstRender { get; set; } = true; // Default to the behavior we just added
    // --- END NEW PARAMETER ---

    /// <summary>
    /// IStringLocalizer service instance
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    /// <summary>
    /// ILogger instance (optional, for logging errors)
    /// </summary>
    [Inject]
    [NotNull]
    private ILogger<AutoComplete>? Logger { get; set; }

    /// <summary>
    /// Gets the string setting for automatically displaying the dropdown when focused
    /// </summary>
    private string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? "true" : null;

    private List<string>? _filterItems;

    [NotNull]
    private RenderTemplate? _dropdown = default!; // Use ! assertion

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        SkipRegisterEnterEscJSInvoke = true; // Keep original base class interaction
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // Keep original parameter initialization
        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoCompleteIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);
        Items ??= [];

        // Note: Logic for handling external Value changes might be needed here
        // by comparing previous/current Value and calling JSSetInputValue if changed.
    }

    /// <summary>
    /// OnAfterRenderAsync method
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender); // Call base implementation first

        if (firstRender)
        {
            // Original logic: Set initial visual value of input via JS
            await JSSetInputValue(Value);

            // --- MODIFIED SECTION ---
            // Check the new parameter before populating/rendering the initial list
            if (PopulateListOnFirstRender) // <--- Check the new parameter
            {
                // 1. Populate the _filterItems list based on initial state
                try
                {
                    if (OnCustomFilter != null)
                    {
                        var initialItems = await OnCustomFilter(Value ?? "");
                        _filterItems = [.. initialItems];
                    }
                    else if (!string.IsNullOrEmpty(Value))
                    {
                        var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                        var items = IsLikeMatch
                            ? Items.Where(s => s.Contains(Value, comparison))
                            : Items.Where(s => s.StartsWith(Value, comparison));
                        _filterItems = [.. items];
                    }
                    else
                    {
                        _filterItems = [.. Items];
                    }

                    if (DisplayCount.HasValue && _filterItems != null)
                    {
                        _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
                    }
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "Error during initial population of AutoComplete dropdown for ID {Id}", Id);
                    _filterItems = [];
                }

                // 2. Trigger a re-render of *only* the dropdown fragment
                if (_dropdown != null)
                {
                    _dropdown.Render();
                }
            }
            // --- End of modified section ---
        }
    }

    // Keep original _render flag and ShouldRender override for targeted dropdown updates
    private bool _shouldRender = true;
    protected override bool ShouldRender() => _shouldRender;

    /// <summary>
    /// Callback method when a candidate item is clicked
    /// </summary>
    private async Task OnClickItem(string val)
    {
        // STUTTER FIX: Bypass CurrentValue setter to avoid potential conflicts/double events
        var previousValue = Value; // Use Value backing field
        var valueHasChanged = !EqualityComparer<string>.Default.Equals(val, previousValue);

        if (valueHasChanged)
        {
            Value = val; // Update backing field directly
            // Manually trigger notifications/callbacks same as original setter would
            if (FieldIdentifier != null) ValidateForm?.NotifyFieldChanged(FieldIdentifier.Value, Value);
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            if (OnValueChanged != null) await OnValueChanged.Invoke(Value);
            if (IsNeedValidate && FieldIdentifier != null) EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
        }

        // STUTTER FIX: Update the visual input via JS
        await JSSetInputValue(val);

        // Original logic for selected item changed
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    private List<string> Rows => _filterItems ?? [.. Items];

    /// <summary>
    /// JSInvokable method called by JavaScript after debouncing input.
    /// Updates C# state, performs filtering, and updates dropdown UI.
    /// Renamed from original TriggerFilter to clarify purpose.
    /// </summary>
    [JSInvokable]
    public async Task PerformFilteringAndCommitValue(string val)
    {
        // STUTTER FIX: Bypass CurrentValue setter
        var previousValue = Value;
        var valueHasChanged = !EqualityComparer<string>.Default.Equals(val, previousValue);

        if (valueHasChanged)
        {
            Value = val; // Update backing field directly
            // Manually trigger notifications and callbacks
            if (FieldIdentifier != null) ValidateForm?.NotifyFieldChanged(FieldIdentifier.Value, Value);
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            // Ensures OnValueChanged is called ONLY after debounce and if value changed
            if (OnValueChanged != null) await OnValueChanged.Invoke(Value);
            if (IsNeedValidate && FieldIdentifier != null) EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
        }

        // Original filtering logic
        try
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
                var comparison = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                var items = IsLikeMatch
                    ? Items.Where(s => s.Contains(val, comparison))
                    : Items.Where(s => s.StartsWith(val, comparison));
                _filterItems = [.. items];
            }

            if (DisplayCount != null && _filterItems != null) // Check _filterItems for null after potential filtering
            {
                _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
            }
        }
        catch (Exception ex) // Optional: Add error handling for filtering
        {
             Logger?.LogError(ex, "Error during filtering/committing value for AutoComplete ID {Id}", Id);
             _filterItems = []; // Set to empty list on error
        }


        // Update dropdown UI using targeted render (original approach)
        _shouldRender = false;
        if (_dropdown != null) _dropdown.Render();
        _shouldRender = true;
    }

    /// <summary>
    /// REMOVED: Original TriggerChange method.
    /// Its functionality (updating value, rendering dropdown) is now handled by
    /// PerformFilteringAndCommitValue after debouncing.
    /// </summary>
    // [JSInvokable]
    // public override Task TriggerChange(string val) { ... }

    /// <summary>
    /// Handles the Enter key press. Commits value and updates input.
    /// Hides base implementation because base is not virtual.
    /// Removed [JSInvokable] to prevent conflicts with base JSInvokable.
    /// </summary>
    public new async Task EnterCallback(string val) // Use 'new'
    {
        // STUTTER FIX: Bypass CurrentValue setter
        var previousValue = Value;
        var valueHasChanged = !EqualityComparer<string>.Default.Equals(val, previousValue);

        if (valueHasChanged)
        {
            Value = val; // Update backing field directly
            // Manually trigger notifications/callbacks
            if (FieldIdentifier != null) ValidateForm?.NotifyFieldChanged(FieldIdentifier.Value, Value);
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            if (OnValueChanged != null) await OnValueChanged.Invoke(Value);
            if (IsNeedValidate && FieldIdentifier != null) EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
        }
        // STUTTER FIX: Update the visual input via JS
        await JSSetInputValue(val);
    }

    /// <summary>
    /// Handles the Escape key press. Resets visual input.
    /// Hides base implementation because base is not virtual.
    /// Removed [JSInvokable] to prevent conflicts with base JSInvokable.
    /// </summary>
    public new async Task EscCallback() // Use 'new'
    {
        // STUTTER FIX: Reset visual input to last committed C# value via JS
        await JSSetInputValue(Value);
    }

    /// <summary>
    /// Handles the Delete/Backspace key press trigger from original JS.
    /// Removed [JSInvokable] to prevent conflicts. Body is cleared because
    /// value changes from delete/backspace are now handled by the debounced
    /// PerformFilteringAndCommitValue method triggered by Input.composition.
    /// </summary>
    public Task TriggerDeleteCallback(string val)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// JSInvokable method called by JS blur event handler.
    /// Commits the current visual input value before triggering OnBlurAsync.
    /// </summary>
    [JSInvokable]
    public async Task TriggerBlurWithValue(string currentValueFromInput)
    {
        // STUTTER FIX / BLUR FIX: Commit the value from the input field on blur
        var previousValue = Value;
        var valueHasChanged = !EqualityComparer<string>.Default.Equals(currentValueFromInput, previousValue);

        if (valueHasChanged)
        {
            Value = currentValueFromInput; // Update backing field directly
            // Manually trigger notifications/callbacks
            if (FieldIdentifier != null) ValidateForm?.NotifyFieldChanged(FieldIdentifier.Value, Value);
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            if (OnValueChanged != null) await OnValueChanged.Invoke(Value);
            if (IsNeedValidate && FieldIdentifier != null) EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
        }

        // Now invoke the original OnBlurAsync callback if it exists
        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value); // Pass the committed value
        }
    }

    /// <summary>
    /// Helper method to call the JS function 'setValue' to update the input element's visual value.
    /// Necessary because Blazor no longer controls the 'value' attribute directly.
    /// </summary>
    private async ValueTask JSSetInputValue(string? value)
    {
        try
        {
            // Module is JSObjectReference from BootstrapModuleComponentBase
            if (Module != null) await Module.InvokeVoidAsync("setValue", Id, value);
        }
        catch (JSDisconnectedException) { } // Ignore if circuit is disconnected
        catch (ObjectDisposedException) { } // Ignore if Module is disposed
        catch (Exception ex)
        {
            // Log error or handle otherwise
            Logger?.LogError(ex, "Error calling JS setValue for ID {Id}", Id);
        }
    }

    /// <summary>
    /// Dispose method
    /// </summary>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        // Ensure base disposal runs (which should call JS dispose)
        await base.DisposeAsync(disposing);
    }
}

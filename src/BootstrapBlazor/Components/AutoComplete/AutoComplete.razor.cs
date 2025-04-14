// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop; // Required for JSInvokable
using System; // Required for Func

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoComplete component
/// </summary>
public partial class AutoComplete
{
    // Parameters... (omitted for brevity, same as latest code)
    #region Parameters
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
    #endregion

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
    private RenderTemplate? _dropdown = default!; // Use ! assertion

    // REMOVED: _currentInputValue field is no longer needed

    // private bool _isFirstRender = true; // Flag for initial value setting - Handled in OnAfterRenderAsync

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        SkipRegisterEnterEscJSInvoke = true; // Keep this if base class needs it
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
    }

    /// <summary>
    /// OnAfterRenderAsync method
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // Ensure JS interop module is loaded (likely handled by base class)
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            // _isFirstRender = false; // Not needed
            await JSSetInputValue(Value); // Set initial value using the backing field
        }
        // Handle external parameter changes if necessary
        // This might require comparing the current Value parameter against a stored previous value
        // For simplicity, we assume external changes require user interaction or parent component logic
    }

    // REMOVED: _render flag and ShouldRender override (may not be needed)

    /// <summary>
    /// Callback method when a candidate item is clicked
    /// </summary>
    private async Task OnClickItem(string val)
    {
        // Update C# state first, bypassing CurrentValue setter
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

        // Update the visual input via JS
        await JSSetInputValue(val);

        // Invoke selection changed callback separately
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }
    }

    private List<string> Rows => _filterItems ?? [.. Items];

    // REMOVED: UpdateInputValue JSInvokable method

    /// <summary>
    /// JSInvokable method called by JavaScript after debouncing.
    /// Receives the debounced value from the input.
    /// Renamed from TriggerFilter.
    /// </summary>
    /// <param name="val">The debounced input value.</param>
    [JSInvokable] // This method is new/renamed, keep JSInvokable
    public async Task PerformFilteringAndCommitValue(string val)
    {
        // --- Bypass CurrentValue Setter ---
        var previousValue = Value;
        var valueHasChanged = !EqualityComparer<string>.Default.Equals(val, previousValue);

        if (valueHasChanged)
        {
            Value = val; // Update backing field directly

            // Manually trigger notifications and callbacks
            if (FieldIdentifier != null) ValidateForm?.NotifyFieldChanged(FieldIdentifier.Value, Value);
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            if (OnValueChanged != null) await OnValueChanged.Invoke(Value);
            if (IsNeedValidate && FieldIdentifier != null) EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
        }
        // --- End Bypass ---

        // Perform filtering logic (using the new 'val')...
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

        // Update dropdown UI
        if (_dropdown != null)
        {
            StateHasChanged(); // Trigger re-render of dropdown via main component render
        }
    }

    // REMOVED: TriggerChange method from latest code

    /// <summary>
    /// Handles the Enter key press, potentially committing the current input value.
    /// Hides base implementation.
    /// </summary>
    /// <param name="val">The current value in the input field when Enter was pressed.</param>
    // Removed [JSInvokable]
    public new async Task EnterCallback(string val) // Use 'new'
    {
        // Update C# state first, bypassing setter
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

        // Update the visual input via JS
        await JSSetInputValue(val);
    }


    /// <summary>
    /// Handles the Escape key press. Hides base implementation.
    /// </summary>
    // Removed [JSInvokable]
    public new async Task EscCallback() // Use 'new'
    {
        // Reset visual input to last committed C# value
        await JSSetInputValue(Value);
    }

    /// <summary>
    /// Handles deletion - No longer directly called by JS in this version.
    /// </summary>
    // Removed [JSInvokable]
    public Task TriggerDeleteCallback(string val)
    {
        // Value update is handled by PerformFilteringAndCommitValue after debounce.
        return Task.CompletedTask;
    }

    /// <summary>
    /// Helper method to call the JS function to set the input value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    private async ValueTask JSSetInputValue(string? value)
    {
        try
        {
            // Module is JSObjectReference from BootstrapModuleComponentBase
            if (Module != null)
            {
                await Module.InvokeVoidAsync("setValue", Id, value);
            }
        }
        catch (JSDisconnectedException) { } // Ignore if circuit is disconnected
        catch (ObjectDisposedException) { } // Ignore if Module is disposed
        catch (Exception ex)
        {
            //Console.WriteLine($"Error calling JS setValue for ID {Id}: {ex.Message}"); // Log other errors
        }
    }

    /// <summary>
    /// Dispose method
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        // Ensure base disposal runs, which should call JS dispose
        await base.DisposeAsync(disposing);
    }
}

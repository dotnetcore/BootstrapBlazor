// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoFill component
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public partial class AutoFill<TValue>
{
    /// <summary>
    /// Gets or sets the collection of items for the component.
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// Gets or sets the number of items to display when matching data. Default is null.
    /// </summary>
    [Parameter]
    [NotNull]
    public int? DisplayCount { get; set; }

    /// <summary>
    /// Gets or sets whether to enable fuzzy search. Default is false.
    /// </summary>
    [Parameter]
    public bool IsLikeMatch { get; set; }

    /// <summary>
    /// Gets or sets whether to ignore case when matching. Default is true.
    /// </summary>
    [Parameter]
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to expand the dropdown candidate menu when focused. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowDropdownListOnFocus { get; set; } = true;

    /// <summary>
    /// Gets or sets the method to get the display text from the model. Default is to use the ToString override method.
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<TValue?, string?>? OnGetDisplayText { get; set; }

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the loading icon.
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// Gets or sets the custom collection filtering rules.
    /// </summary>
    [Parameter]
    public Func<string, Task<IEnumerable<TValue>>>? OnCustomFilter { get; set; }

    /// <summary>
    /// Gets or sets whether to show the no matching data option. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowNoDataTip { get; set; } = true;

    /// <summary>
    /// Gets or sets the candidate item template. Default is null.
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ItemTemplate 代替；Deprecated please use ItemTemplate parameter")]
    [ExcludeFromCodeCoverage]
    public RenderFragment<TValue>? Template { get => ItemTemplate; set => ItemTemplate = value; }

    /// <summary>
    /// Gets or sets whether virtual scrolling is enabled. Default is false.
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// Gets or sets the row height for virtual scrolling. Default is 50f.
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 50f;

    /// <summary>
    /// 失去焦点时判定输入框内容无效则清空内容
    /// </summary>
    [Parameter]
    public bool IsClearOnInvalid { get; set; } = false;

    /// <summary>
    /// Gets or sets the overscan count for virtual scrolling. Default is 3.
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 3;

    /// <summary>
    /// Gets or sets the callback method for loading virtualized items.
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<TValue>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the clear button is clicked. Default is null.
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<AutoComplete>? Localizer { get; set; }

    private string? ShowDropdownListOnFocusString => ShowDropdownListOnFocus ? "true" : null;

    private string? _displayText;

    private List<TValue>? _filterItems;

    [NotNull]
    private Virtualize<TValue>? _virtualizeElement = null;

    [NotNull]
    private RenderTemplate? _dropdown = null;

    /// <summary>
    /// Gets the component style.
    /// </summary>
    private string? ClassString => CssBuilder.Default("auto-complete auto-fill")
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

    private string? PlaceHolderStyleString => Math.Abs(RowHeight - 50f) > 0.1f
        ? CssBuilder.Default().AddClass($"height: {RowHeight.ToString(CultureInfo.InvariantCulture)}px;").Build()
        : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NoDataTip ??= Localizer[nameof(NoDataTip)];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AutoFillIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.LoadingIcon);

        _displayText = GetDisplayText(Value);
        Items ??= [];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, _displayText);

    private bool IsNullable() => !ValueType.IsValueType || NullableUnderlyingType != null;

    /// <summary>
    /// Gets whether show the clear button.
    /// </summary>
    /// <returns></returns>
    private bool GetClearable() => IsClearable && !IsDisabled && IsNullable();

    /// <summary>
    /// Callback method when a candidate item is clicked.
    /// </summary>
    /// <param name="val">The value of the clicked item.</param>
    private async Task OnClickItem(TValue val)
    {
        CurrentValue = val;
        _displayText = GetDisplayText(val);

        // 使用脚本更新 input 值
        await InvokeVoidAsync("setValue", Id, _displayText);

        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(val);
        }

        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }

        await TriggerFilter(_displayText!);
    }

    private string? GetDisplayText(TValue item) => OnGetDisplayText?.Invoke(item) ?? item?.ToString();

    private List<TValue> Rows => _filterItems ?? [.. Items];

    private async ValueTask<ItemsProviderResult<TValue>> LoadItems(ItemsProviderRequest request)
    {
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = request.Count, SearchText = _searchText });
        var _totalCount = data.TotalCount;
        var items = data.Items ?? [];
        return new ItemsProviderResult<TValue>(items, _totalCount);
    }

    private string? _searchText;

    /// <summary>
    /// Triggers the filter method.
    /// </summary>
    /// <param name="val">The value to filter by.</param>
    [JSInvokable]
    public async Task TriggerFilter(string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            CurrentValue = default;
            _filterItems = null;
            _displayText = null;

            if (OnClearAsync != null)
            {
                await OnClearAsync();
            }
        }

        if (OnQueryAsync != null)
        {
            _searchText = val;
            await _virtualizeElement.RefreshDataAsync();
            _dropdown.Render();
            return;
        }

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
                ? Items.Where(i => OnGetDisplayText?.Invoke(i)?.Contains(val, comparison) ?? false)
                : Items.Where(i => OnGetDisplayText?.Invoke(i)?.StartsWith(val, comparison) ?? false);
            _filterItems = [.. items];
        }

        if (!IsVirtualize && DisplayCount != null)
        {
            _filterItems = [.. _filterItems.Take(DisplayCount.Value)];
        }
        _dropdown.Render();
    }


    protected virtual async Task OnBlur()
    {
        if (IsClearOnInvalid)
        {
            // 获取input的实际值
            var inputValue = await GetInputValue();

            if (!string.IsNullOrEmpty(inputValue))
            {
                if (GetDisplayText(Value) != inputValue)
                {
                    // 如果没有匹配项，清空输入和Model
                    CurrentValue = default;
                    await InvokeVoidAsync("setValue", Id, "");
                }
            }
        }


        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }
    [JSInvokable]
    public async Task<string> GetInputValue()
    {
        return await InvokeAsync<string>("getInputValue", Id);
    }
}

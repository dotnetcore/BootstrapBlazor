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

    private string? _clientValue;

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
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            if (Value != _clientValue)
            {
                _clientValue = Value;
                await InvokeVoidAsync("setValue", Id, _clientValue);
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Value, GetChangedEventCallbackName());

    private string? GetChangedEventCallbackName() => (OnValueChanged != null || ValueChanged.HasDelegate) ? nameof(TriggerChange) : null;

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
    /// 点击清空按钮时调用此方法 由 Javascript 触发
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerClear()
    {
        await TriggerFilter("");

        _clientValue = null;
        CurrentValueAsString = string.Empty;
    }

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

    /// <summary>
    /// 支持双向绑定 由客户端 JavaScript 触发
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    [JSInvokable]
    public void TriggerChange(string v)
    {
        _clientValue = v;
        CurrentValueAsString = v;
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

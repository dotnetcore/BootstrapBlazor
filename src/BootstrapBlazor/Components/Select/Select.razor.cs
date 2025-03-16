// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Select component
/// </summary>
/// <typeparam name="TValue"></typeparam>
public partial class Select<TValue> : ISelect, ILookup
{
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    private string? ClassString => CssBuilder.Default("select dropdown")
        .AddClass("cls", IsClearable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? InputClassString => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    private string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private bool GetClearable() => IsClearable && !IsDisabled && IsNullable();

    private string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", item.Value == CurrentValueAsString)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private string? SearchLoadingIconString => CssBuilder.Default("icon searching-icon")
        .AddClass(SearchLoadingIcon)
        .Build();

    private string? DropdownMenuClassString => CssBuilder.Default("dropdown-menu")
        .AddClass("is-fixed-search", ShowSearch && IsFixedSearch)
        .Build();

    private readonly List<SelectedItem> _children = [];

    private string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollIntoViewBehavior.ToDescriptionString();

    /// <summary>
    /// Gets or sets the right-side clear icon. Default is fa-solid fa-angle-up.
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the search text changes.
    /// </summary>
    [Parameter]
    public Func<string, IEnumerable<SelectedItem>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the search bar in the dropdown is fixed. Default is false.
    /// </summary>
    [Parameter]
    public bool IsFixedSearch { get; set; }

    /// <summary>
    /// Gets or sets whether the select component is editable. Default is false.
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the input value changes. Default is null.
    /// </summary>
    /// <remarks>Effective when <see cref="IsEditable"/> is set.</remarks>
    [Parameter]
    public Func<string, Task>? OnInputChangedCallback { get; set; }

    /// <summary>
    /// Gets or sets whether the select component is clearable. Default is false.
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// Gets or sets the options template for static data.
    /// </summary>
    [Parameter]
    public RenderFragment? Options { get; set; }

    /// <summary>
    /// Gets or sets the display template. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem?>? DisplayTemplate { get; set; }

    /// <summary>
    /// Gets or sets whether virtual scrolling is enabled. Default is false. Note: When virtual scrolling is enabled, <see cref="SelectBase{TValue}.ShowSearch"/>, <see cref="PopoverSelectBase{TValue}.IsPopover"/>, and <seealso cref="IsFixedSearch"/> are not supported. Set <see cref="DefaultVirtualizeItemText"/> when setting initial values.
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
    /// Gets or sets the default text for virtualized items. Default is null.
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is enabled and data source is provided via <see cref="OnQueryAsync"/>. If the data set does not contain the <see cref="DisplayBase{TValue}.Value"/> option value during rendering, the DefaultText value is used.</remarks>
    [Parameter]
    public string? DefaultVirtualizeItemText { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the clear button is clicked. Default is null.
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    /// <summary>
    /// Gets or sets whether to disable the OnSelectedItemChanged callback method on first render. Default is false.
    /// </summary>
    [Parameter]
    public bool DisableItemChangedWhenFirstRender { get; set; }

    /// <summary>
    /// Gets or sets the bound data set.
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// Gets or sets the item template.
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

    /// <summary>
    /// Gets or sets the callback method before the selected item changes. Returns true to change the selected item value; otherwise, the selected item value does not change.
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task<bool>>? OnBeforeSelectedItemChange { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the selected item changes.
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// Gets or sets the Swal category. Default is Question.
    /// </summary>
    [Parameter]
    public SwalCategory SwalCategory { get; set; } = SwalCategory.Question;

    /// <summary>
    /// Gets or sets the Swal title. Default is null.
    /// </summary>
    [Parameter]
    public string? SwalTitle { get; set; }

    /// <summary>
    /// Gets or sets the Swal content. Default is null.
    /// </summary>
    [Parameter]
    public string? SwalContent { get; set; }

    /// <summary>
    /// Gets or sets the Swal footer. Default is null.
    /// </summary>
    [Parameter]
    public string? SwalFooter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// Gets or sets the callback method for loading virtualized items.
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<SelectedItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    IEnumerable<SelectedItem>? ILookup.Lookup { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    StringComparison ILookup.LookupStringComparison { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TValue>>? Localizer { get; set; }

    /// <summary>
    /// Gets or sets the injected lookup service instance.
    /// </summary>
    [Inject]
    [NotNull]
    private ILookupService? InjectLookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? RetrieveId() => InputId;

    [NotNull]
    private Virtualize<SelectedItem>? _virtualizeElement = default;

    private string? InputId => $"{Id}_input";

    private string _lastSelectedValueString = string.Empty;

    private bool _init = true;

    private List<SelectedItem>? _itemsCache;

    private ItemsProviderResult<SelectedItem> _result;

    private SelectedItem? SelectedItem { get; set; }

    private List<SelectedItem> Rows
    {
        get
        {
            _itemsCache ??= string.IsNullOrEmpty(SearchText) ? GetRowsByItems() : GetRowsBySearch();
            return _itemsCache;
        }
    }

    private SelectedItem? SelectedRow
    {
        get
        {
            SelectedItem ??= GetSelectedRow();
            return SelectedItem;
        }
    }

    private SelectedItem? GetSelectedRow()
    {
        if (Value is null)
        {
            return null;
        }

        var item = GetItemWithEnumValue()
            ?? Rows.Find(i => i.Value == CurrentValueAsString)
            ?? Rows.Find(i => i.Active)
            ?? Rows.FirstOrDefault(i => !i.IsDisabled)
            ?? GetVirtualizeItem(CurrentValueAsString);

        if (item != null)
        {
            if (_init && DisableItemChangedWhenFirstRender)
            {

            }
            else
            {
                _ = SelectedItemChanged(item);
                _init = false;
            }
        }
        return item;
    }

    private SelectedItem? GetItemWithEnumValue() => ValueType.IsEnum
        ? Rows.Find(i => i.Value == Convert.ToInt32(Value).ToString())
        : null;

    private List<SelectedItem> GetRowsByItems()
    {
        var items = new List<SelectedItem>();
        if (Items != null)
        {
            items.AddRange(Items);
        }
        items.AddRange(_children);
        return items;
    }

    private List<SelectedItem> GetRowsBySearch()
    {
        var items = OnSearchTextChanged?.Invoke(SearchText) ?? FilterBySearchText(GetRowsByItems());
        return [.. items];
    }

    private IEnumerable<SelectedItem> FilterBySearchText(IEnumerable<SelectedItem> source) => string.IsNullOrEmpty(SearchText)
        ? source
        : source.Where(i => i.Text.Contains(SearchText, StringComparison));

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        NoSearchDataText ??= Localizer[nameof(NoSearchDataText)];
        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectDropdownIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectClearIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Items ??= await this.GetItemsAsync(InjectLookupService, LookupServiceKey, LookupServiceData) ?? [];

        // 内置对枚举类型的支持
        if (!Items.Any() && ValueType.IsEnum())
        {
            var item = NullableUnderlyingType == null ? "" : PlaceHolder;
            Items = ValueType.ToSelectList(string.IsNullOrEmpty(item) ? null : new SelectedItem("", item));
        }

        _itemsCache = null;
        SelectedItem = null;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await RefreshVirtualizeElement();
            StateHasChanged();
        }
    }

    private int TotalCount { get; set; }

    private List<SelectedItem> GetVirtualItems() => [.. FilterBySearchText(GetRowsByItems())];

    private async ValueTask<ItemsProviderResult<SelectedItem>> LoadItems(ItemsProviderRequest request)
    {
        // 有搜索条件时使用原生请求数量
        // 有总数时请求剩余数量
        var count = !string.IsNullOrEmpty(SearchText) ? request.Count : GetCountByTotal();
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = count, SearchText = SearchText });

        TotalCount = data.TotalCount;
        var items = data.Items ?? [];
        _result = new ItemsProviderResult<SelectedItem>(items, TotalCount);
        return _result;

        int GetCountByTotal() => TotalCount == 0 ? request.Count : Math.Min(request.Count, TotalCount - request.StartIndex);
    }

    private async Task SearchTextChanged(string val)
    {
        _itemsCache = null;
        SearchText = val;
        await RefreshVirtualizeElement();
    }

    private async Task RefreshVirtualizeElement()
    {
        if (IsVirtualize && OnQueryAsync != null)
        {
            // 通过 ItemProvider 提供数据
            await _virtualizeElement.RefreshDataAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage) => ValueType == typeof(SelectedItem)
        ? TryParseSelectItem(value, out result, out validationErrorMessage)
        : base.TryParseValueFromString(value, out result, out validationErrorMessage);

    private bool TryParseSelectItem(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        SelectedItem = Rows.FirstOrDefault(i => i.Value == value)
            ?? GetVirtualizeItem(value);

        // support SelectedItem? type
        result = SelectedItem != null ? (TValue)(object)SelectedItem : default;
        validationErrorMessage = "";
        return SelectedItem != null;
    }

    private SelectedItem? GetVirtualizeItem(string value)
    {
        SelectedItem? item = null;
        if (_result.Items != null)
        {
            item = _result.Items.FirstOrDefault(i => i.Value == value) ?? new SelectedItem(value, DefaultVirtualizeItemText ?? value);
        }
        return item;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new { ConfirmMethodCallback = nameof(ConfirmSelectedItem), SearchMethodCallback = nameof(TriggerOnSearch) });

    /// <summary>
    /// Confirms the selected item.
    /// </summary>
    /// <param name="index">The index of the selected item.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [JSInvokable]
    public async Task ConfirmSelectedItem(int index)
    {
        if (index < Rows.Count)
        {
            await OnClickItem(Rows[index]);
            StateHasChanged();
        }
    }

    /// <summary>
    /// Triggers the search callback method.
    /// </summary>
    /// <param name="searchText">The search text.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [JSInvokable]
    public async Task TriggerOnSearch(string searchText)
    {
        await SearchTextChanged(searchText);
        StateHasChanged();
    }

    /// <summary>
    /// Handles the click event for a dropdown item.
    /// </summary>
    /// <param name="item">The selected item.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task OnClickItem(SelectedItem item)
    {
        var ret = true;
        if (OnBeforeSelectedItemChange != null)
        {
            ret = await OnBeforeSelectedItemChange(item);
            if (ret)
            {
                // Return true to show modal
                var option = new SwalOption()
                {
                    Category = SwalCategory,
                    Title = SwalTitle,
                    Content = SwalContent
                };
                if (!string.IsNullOrEmpty(SwalFooter))
                {
                    option.ShowFooter = true;
                    option.FooterTemplate = builder => builder.AddContent(0, SwalFooter);
                }
                ret = await SwalService.ShowModal(option);
            }
            else
            {
                // Return false to proceed
                ret = true;
            }
        }
        if (ret)
        {
            await SelectedItemChanged(item);
        }
    }

    private async Task SelectedItemChanged(SelectedItem item)
    {
        if (_lastSelectedValueString != item.Value)
        {
            item.Active = true;
            SelectedItem = item;

            _lastSelectedValueString = item.Value;
            CurrentValueAsString = _lastSelectedValueString;

            if (OnSelectedItemChanged != null)
            {
                await OnSelectedItemChanged(SelectedItem);
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Add(SelectedItem item) => _children.Add(item);

    /// <summary>
    /// Clears the search text.
    /// </summary>
    public void ClearSearchText() => SearchText = null;

    private async Task OnClearValue()
    {
        if (ShowSearch)
        {
            ClearSearchText();
        }
        if (OnClearAsync != null)
        {
            await OnClearAsync();
        }

        if (OnQueryAsync != null)
        {
            await _virtualizeElement.RefreshDataAsync();
        }

        _lastSelectedValueString = string.Empty;
        CurrentValue = default;
        SelectedItem = null;
    }

    private bool IsNullable() => !ValueType.IsValueType || NullableUnderlyingType != null;

    private string? ReadonlyString => IsEditable ? null : "readonly";

    private async Task OnChange(ChangeEventArgs args)
    {
        if (args.Value is string v)
        {
            // Items 中没有时插入一个 SelectedItem
            var item = Items.FirstOrDefault(i => i.Text == v);

            if (item == null)
            {
                item = new SelectedItem(v, v);

                var items = new List<SelectedItem>() { item };
                items.AddRange(Items);
                Items = items;
            }
            CurrentValueAsString = v;

            if (OnInputChangedCallback != null)
            {
                await OnInputChangedCallback(v);
            }
        }
    }
}

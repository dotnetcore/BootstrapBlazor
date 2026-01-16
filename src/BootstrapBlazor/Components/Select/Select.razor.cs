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

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TValue>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ILookupService? InjectLookupService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 值为 null 时是否使用第一个选项或者标记为 active 的候选项作为默认值</para>
    /// <para lang="en">Gets or sets a value indicating Whether to use the first option or the candidate marked as active as the default value when the value is null</para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 IsUseDefaultItemWhenValueIsNull 参数代替；Deprecated, use the IsUseDefaultItemWhenValueIsNull parameter instead")]
    [ExcludeFromCodeCoverage]
    public bool IsUseActiveWhenValueIsNull
    {
        get => IsUseDefaultItemWhenValueIsNull;
        set => IsUseDefaultItemWhenValueIsNull = value;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 值为 null 时是否使用第一个选项或者标记为 active 的候选项作为默认值</para>
    /// <para lang="en">Gets or sets a value indicating Whether to use the first option or the candidate marked as active as the default value when the value is null</para>
    /// </summary>
    [Parameter]
    public bool IsUseDefaultItemWhenValueIsNull { get; set; }

    /// <summary>
    /// Gets or sets the display template. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem?>? DisplayTemplate { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the input value changes. Default is null.
    /// </summary>
    /// <remarks>Effective when <see cref="SimpleSelectBase{TValue}.IsEditable"/> is set.</remarks>
    [Parameter]
    public Func<string, Task>? OnInputChangedCallback { get; set; }

    /// <summary>
    /// Gets or sets the options template for static data.
    /// </summary>
    [Parameter]
    public RenderFragment? Options { get; set; }

    /// <summary>
    /// Gets or sets whether to disable the OnSelectedItemChanged callback method on first render. Default is false.
    /// </summary>
    [Parameter]
    public bool DisableItemChangedWhenFirstRender { get; set; }

    /// <summary>
    /// <para lang="zh">获取/设置 选中项改变前的回调方法。返回 true 则改变选中项的值；否则选中项的值不变。</para>
    /// <para lang="en">Gets or sets the callback method before the selected item changes. Returns true to change the selected item value; otherwise, the selected item value does not change.</para>
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task<bool>>? OnBeforeSelectedItemChange { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Swal 确认弹窗 默认值 为 false</para>
    /// <para lang="en">Gets or sets whether to show the Swal confirmation popup. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool ShowSwal { get; set; }

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
    /// Gets or sets the default text for virtualized items. Default is null.
    /// </summary>
    [Parameter]
    public string? DefaultVirtualizeItemText { get; set; }

    /// <summary>
    /// Gets or sets whether auto clear the search text when dropdown closed.
    /// </summary>
    [Parameter]
    public bool IsAutoClearSearchTextWhenCollapsed { get; set; }

    /// <summary>
    /// Gets or sets the dropdown collapsed callback method.
    /// </summary>
    [Parameter]
    public Func<Task>? OnCollapsed { get; set; }

    IEnumerable<SelectedItem>? ILookup.Lookup { get => Items; set => Items = value; }

    StringComparison ILookup.LookupStringComparison { get => StringComparison; set => StringComparison = value; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? RetrieveId() => InputId;

    private string? ClassString => CssBuilder.Default("select dropdown")
        .AddClass("is-clearable", IsClearable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? InputClassString => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    private string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", item.Value == CurrentValueAsString)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private readonly List<SelectedItem> _children = [];

    private string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollIntoViewBehavior.ToDescriptionString();

    private string? InputId => $"{Id}_input";

    private bool _init = true;

    private ItemsProviderResult<SelectedItem> _result;

    private string _defaultVirtualizedItemText = "";

    private SelectedItem? SelectedItem { get; set; }

    private SelectedItem? SelectedRow
    {
        get
        {
            SelectedItem ??= GetSelectedRow();
            return SelectedItem;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _defaultVirtualizedItemText = DefaultVirtualizeItemText ?? CurrentValueAsString;
    }

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

        // <para lang="zh">内置对枚举类型的支持</para>
        // <para lang="en">Built-in support for enum types</para>
        if (!Items.Any() && ValueType.IsEnum())
        {
            var item = NullableUnderlyingType == null ? "" : PlaceHolder;
            Items = ValueType.ToSelectList(string.IsNullOrEmpty(item) ? null : new SelectedItem("", item));
        }

        _itemsCache = null;
        SelectedItem = null;
    }

    private int _totalCount;

    private List<SelectedItem> GetVirtualItems() => [.. FilterBySearchText(GetRowsByItems())];

    private async ValueTask<ItemsProviderResult<SelectedItem>> LoadItems(ItemsProviderRequest request)
    {
        // <para lang="zh">有搜索条件时使用原生请求数量</para>
        // <para lang="en">Use original request count when there is search condition</para>
        // <para lang="zh">有总数时请求剩余数量</para>
        // <para lang="en">Request remaining count when there is total count</para>
        var count = !string.IsNullOrEmpty(SearchText) ? request.Count : GetCountByTotal();
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = count, SearchText = SearchText });

        _itemsCache = null;
        _totalCount = data.TotalCount;
        var items = data.Items ?? [];
        _result = new ItemsProviderResult<SelectedItem>(items, _totalCount);
        return _result;

        int GetCountByTotal() => _totalCount == 0 ? request.Count : Math.Min(request.Count, _totalCount - request.StartIndex);
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
        validationErrorMessage = null;
        return SelectedItem != null;
    }

    private SelectedItem? GetVirtualizeItem(string value)
    {
        SelectedItem? item = null;
        if (_result.Items != null)
        {
            item = _result.Items.FirstOrDefault(i => i.Value == value);
        }
        return item;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new
    {
        ConfirmMethodCallback = nameof(ConfirmSelectedItem),
        SearchMethodCallback = nameof(TriggerOnSearch),
        TriggerCollapsed = (OnCollapsed != null || IsAutoClearSearchTextWhenCollapsed) ? nameof(TriggerCollapsed) : null
    });

    /// <summary>
    /// Trigger <see cref="OnCollapsed"/> event callback method. called by JavaScript.
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerCollapsed()
    {
        if (OnCollapsed != null)
        {
            await OnCollapsed();
        }

        if (IsAutoClearSearchTextWhenCollapsed)
        {
            SearchText = string.Empty;
            _itemsCache = null;
            StateHasChanged();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override List<SelectedItem> GetRowsByItems()
    {
        var items = new List<SelectedItem>();
        if (Items != null)
        {
            items.AddRange(Items);
        }
        items.AddRange(_children);
        return items;
    }

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
    /// Handles the click event for a dropdown item.
    /// </summary>
    /// <param name="item">The selected item.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task OnClickItem(SelectedItem item)
    {
        var ret = true;

        // <para lang="zh">自定义回调方法 OnBeforeSelectedItemChange 返回 false 时不修改选中项</para>
        // <para lang="en">Do not modify the selected item when the custom callback method OnBeforeSelectedItemChange returns false</para>
        if (OnBeforeSelectedItemChange != null)
        {
            ret = await OnBeforeSelectedItemChange(item);
        }

        // <para lang="zh">如果 ShowSwal 为 true 且 则显示 Swal 确认弹窗，通过确认弹窗返回值决定是否修改选中项</para>
        // <para lang="en">If ShowSwal is true, show the Swal confirmation popup and decide whether to modify the selected item based on the confirmation popup return value</para>
        if (ret && ShowSwal)
        {
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

        // <para lang="zh">如果 ret 为 true 则修改选中项</para>
        // <para lang="en">If ret is true, modify the selected item</para>
        if (ret)
        {
            _defaultVirtualizedItemText = item.Text;
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
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnClearValue()
    {
        await base.OnClearValue();

        SelectedItem = null;
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(new SelectedItem("", ""));
        }
    }

    private string? ReadonlyString => IsEditable ? null : "readonly";

    private async Task OnChange(ChangeEventArgs args)
    {
        if (args.Value is string v)
        {
            // <para lang="zh">Items 中没有时插入一个 SelectedItem</para>
            // <para lang="en">Insert a SelectedItem when it is not in Items</para>
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

    private SelectedItem? GetSelectedRow()
    {
        if (Value is null)
        {
            _lastSelectedValueString = "";
            _init = false;

            return IsUseDefaultItemWhenValueIsNull && !IsVirtualize
                ? SetSelectedItemState(GetItemByRows())
                : null;
        }

        var item = IsVirtualize ? GetItemByVirtualized() : GetItemByRows();
        return SetSelectedItemState(item);
    }

    private SelectedItem? SetSelectedItemState(SelectedItem? item)
    {
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

    private SelectedItem? GetItemWithEnumValue() => ValueType.IsEnum ? Rows.Find(i => i.Value == Convert.ToInt32(Value).ToString()) : null;

    private SelectedItem GetItemByVirtualized() => new(CurrentValueAsString, _defaultVirtualizedItemText);

    private SelectedItem? GetItemByRows()
    {
        var item = GetItemWithEnumValue()
            ?? Rows.Find(i => i.Value == CurrentValueAsString)
            ?? Rows.Find(i => i.Active)
            ?? Rows.Find(i => !i.IsDisabled);
        return item;
    }
}

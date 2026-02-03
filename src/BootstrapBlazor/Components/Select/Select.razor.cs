// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Select 组件</para>
/// <para lang="en">Select component</para>
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
    /// <para lang="en">Gets or sets whether to use the first option or the candidate marked as active as the default value when the value is null</para>
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
    /// <para lang="en">Gets or sets whether to use the first option or the candidate marked as active as the default value when the value is null</para>
    /// </summary>
    [Parameter]
    public bool IsUseDefaultItemWhenValueIsNull { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示模板，默认为 null</para>
    /// <para lang="en">Gets or sets the display template. Default is null</para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem?>? DisplayTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 输入值改变时的回调方法，默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when the input value changes. Default is null</para>
    /// </summary>
    /// <remarks>Effective when <see cref="SimpleSelectBase{TValue}.IsEditable"/> is set.</remarks>
    [Parameter]
    public Func<string, Task>? OnInputChangedCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 静态数据的选项模板</para>
    /// <para lang="en">Gets or sets the options template for static data</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Options { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否在首次渲染时禁用 OnSelectedItemChanged 回调方法，默认为 false</para>
    /// <para lang="en">Gets or sets whether to disable the OnSelectedItemChanged callback method on first render. Default is false</para>
    /// </summary>
    [Parameter]
    public bool DisableItemChangedWhenFirstRender { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中项改变前的回调方法。返回 true 则改变选中项的值；否则选中项的值不变</para>
    /// <para lang="en">Gets or sets the callback method before the selected item changes. Returns true to change the selected item value; otherwise, the selected item value does not change</para>
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task<bool>>? OnBeforeSelectedItemChange { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Swal 确认弹窗，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show the Swal confirmation popup. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowSwal { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中项改变时的回调方法</para>
    /// <para lang="en">Gets or sets the callback method when the selected item changes</para>
    /// </summary>
    [Parameter]
    public Func<SelectedItem, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Swal 类别，默认为 Question</para>
    /// <para lang="en">Gets or sets the Swal category. Default is Question</para>
    /// </summary>
    [Parameter]
    public SwalCategory SwalCategory { get; set; } = SwalCategory.Question;

    /// <summary>
    /// <para lang="zh">获得/设置 Swal 标题，默认为 null</para>
    /// <para lang="en">Gets or sets the Swal title. Default is null</para>
    /// </summary>
    [Parameter]
    public string? SwalTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Swal 内容，默认为 null</para>
    /// <para lang="en">Gets or sets the Swal content. Default is null</para>
    /// </summary>
    [Parameter]
    public string? SwalContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Swal 底部内容，默认为 null</para>
    /// <para lang="en">Gets or sets the Swal footer. Default is null</para>
    /// </summary>
    [Parameter]
    public string? SwalFooter { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupService"/>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupServiceKey"/>
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupServiceData"/>
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 虚拟化项目的默认文本，默认为 null</para>
    /// <para lang="en">Gets or sets the default text for virtualized items. Default is null</para>
    /// </summary>
    [Parameter]
    public string? DefaultVirtualizeItemText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉框关闭时是否自动清空搜索文本</para>
    /// <para lang="en">Gets or sets whether auto clear the search text when dropdown closed</para>
    /// </summary>
    [Parameter]
    public bool IsAutoClearSearchTextWhenCollapsed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉框关闭时的回调方法</para>
    /// <para lang="en">Gets or sets the dropdown collapsed callback method</para>
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
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = request.Count, SearchText = SearchText });

        _itemsCache = null;
        _totalCount = data.TotalCount;
        var items = data.Items ?? [];
        _result = new ItemsProviderResult<SelectedItem>(items, _totalCount);
        return _result;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
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
    /// <para lang="zh">Trigger <see cref="OnCollapsed"/> event 回调方法. called by JavaScript</para>
    /// <para lang="en">Trigger <see cref="OnCollapsed"/> event callback method. called by JavaScript</para>
    /// </summary>
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
    /// <para lang="zh">Confirms the selected item</para>
    /// <para lang="en">Confirms the selected item</para>
    /// </summary>
    /// <param name="index">The index of the selected item.</param>
    [JSInvokable]
    public async Task ConfirmSelectedItem(int index)
    {
        if (index < Rows.Count)
        {
            await OnClickItem(Rows[index]);
            StateHasChanged();
        }
    }

    private async Task OnClickItem(SelectedItem item)
    {
        var ret = true;
        if (OnBeforeSelectedItemChange != null)
        {
            ret = await OnBeforeSelectedItemChange(item);
        }

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
        // 修复：使用完整的未过滤列表来查找当前选中项
        // 避免在用户搜索时被外部 StateHasChanged 影响导致值被错误修改
        var allItems = GetRowsByItems();
        
        var item = GetItemWithEnumValue()
            ?? allItems.Find(i => i.Value == CurrentValueAsString)
            ?? allItems.Find(i => i.Active)
            ?? allItems.Find(i => !i.IsDisabled);
        return item;
    }
}

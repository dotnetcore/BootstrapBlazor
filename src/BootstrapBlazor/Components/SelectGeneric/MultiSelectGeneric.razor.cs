// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">MultiSelectGeneric component</para>
///  <para lang="en">MultiSelectGeneric component</para>
/// </summary>
[ExcludeFromCodeCoverage]
public partial class MultiSelectGeneric<TValue> : IModelEqualityComparer<TValue>
{
    private List<SelectedItem<TValue>> SelectedItems { get; } = [];

    private string? ClassString => CssBuilder.Default("select dropdown multi-select")
        .AddClass("is-clearable", IsClearable)
        .Build();

    private string? DropdownMenuClassString => CssBuilder.Default("dropdown-menu")
        .AddClass("is-fixed-toolbar", ShowToolbar)
        .Build();

    private string? ToggleClassString => CssBuilder.Default("dropdown-toggle scroll")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
        .AddClass("is-fixed", IsFixedHeight)
        .AddClass("is-single-line", IsSingleLine)
        .AddClass("disabled", IsDisabled)
        .AddClass("show", ValidateForm != null && _isToggle)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    private string? GetItemClassString(SelectedItem<TValue> item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", GetCheckedState(item))
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private string? PlaceHolderClassString => CssBuilder.Default("multi-select-ph")
        .AddClass("d-none", SelectedItems.Count != 0)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得/设置 显示部分模板 默认 null</para>
    ///  <para lang="en">Get/Set Display Template. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<List<SelectedItem<TValue>>>? DisplayTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示关闭按钮 默认为 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show close button. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 关闭按钮图标 默认为 null</para>
    ///  <para lang="en">Get/Set Close button icon. Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示功能按钮 默认为 false 不显示</para>
    ///  <para lang="en">Get/Set Whether to show toolbar. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示默认功能按钮 默认为 true 显示</para>
    ///  <para lang="en">Get/Set Whether to show default buttons. Default true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowDefaultButtons { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否固定高度 默认 false</para>
    ///  <para lang="en">Get/Set Whether fixed height. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsFixedHeight { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否为单行模式 默认 false</para>
    ///  <para lang="en">Get/Set Whether single line mode. Default false</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsSingleLine { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑模式下输入选项更新后回调方法 默认 null</para>
    ///  <para lang="en">Get/Set Callback method when input option is updated in edit mode. Default null</para>
    ///  <para lang="zh">返回 <see cref="SelectedItem"/> 实例时输入选项生效，返回 null 时选项不生效进行舍弃操作，建议在回调方法中自行提示</para>
    ///  <para lang="en">Return <see cref="SelectedItem"/> instance to take effect, return null to discard, it is recommended to prompt in the callback method</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>Effective when <see cref="SimpleSelectBase{TValue}.IsEditable"/> is set.</remarks>
    [Parameter]
    public Func<string, Task<SelectedItem>>? OnEditCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 编辑提交按键 默认 Enter</para>
    ///  <para lang="en">Get/Set Edit Submit Key. Default Enter</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EditSubmitKey EditSubmitKey { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 扩展按钮模板</para>
    ///  <para lang="en">Get/Set Button Template</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 选中项集合发生改变时回调委托方法</para>
    ///  <para lang="en">Callback method when selected items collection changes</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<SelectedItem<TValue>>, Task>? OnSelectedItemsChanged { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the default virtualize items text.</para>
    ///  <para lang="en">Gets or sets the default virtualize items text.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DefaultVirtualizeItemText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 全选按钮显示文本</para>
    ///  <para lang="en">Get/Set Select All Text</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SelectAllText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 全选按钮显示文本</para>
    ///  <para lang="en">Get/Set Reverse Select Text</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ReverseSelectText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 全选按钮显示文本</para>
    ///  <para lang="en">Get/Set Clear Text</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 选项最大数 默认为 0 不限制</para>
    ///  <para lang="en">Get/Set Max items. Default 0 (no limit)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Max { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 设置最大值时错误消息文字</para>
    ///  <para lang="en">Get/Set Error message when max value is set</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MaxErrorMessage { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 选项最小数 默认为 0 不限制</para>
    ///  <para lang="en">Get/Set Min items. Default 0 (no limit)</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Min { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 设置最小值时错误消息文字</para>
    ///  <para lang="en">Get/Set Error message when min value is set</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MinErrorMessage { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the items.</para>
    ///  <para lang="en">Gets or sets the items.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem<TValue>>? Items { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the 回调方法 for loading virtualized items.</para>
    ///  <para lang="en">Gets or sets the callback method for loading virtualized items.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<SelectedItem<TValue>>>>? OnQueryAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the 回调方法 when the search text changes.</para>
    ///  <para lang="en">Gets or sets the callback method when the search text changes.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string, IEnumerable<SelectedItem<TValue>>>? OnSearchTextChanged { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the item 模板.</para>
    ///  <para lang="en">Gets or sets the item template.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem<TValue>>? ItemTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 比较数据是否相同回调方法 默认为 null</para>
    ///  <para lang="en">Get/Set Value Equality Comparer. Default null</para>
    ///  <para lang="zh">提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</para>
    ///  <para lang="en">Ignore <see cref="CustomKeyAttribute"/> when providing this callback</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TValue, TValue, bool>? ValueEqualityComparer { get; set; }

    Func<TValue, TValue, bool>? IModelEqualityComparer<TValue>.ModelEqualityComparer
    {
        get => ValueEqualityComparer;
        set => ValueEqualityComparer = value;
    }

    /// <summary>
    ///  <para lang="zh">获得/设置 数据主键标识标签 默认为 <see cref="KeyAttribute"/>用于判断数据主键标签，如果模型未设置主键时可使用 <see cref="ValueEqualityComparer"/> 参数自定义判断数据模型支持联合主键</para>
    ///  <para lang="en">Get/Set Identifier tag for data primary key. Default is <see cref="KeyAttribute"/>. Used to determine date primary key tag. If the model does not set a primary key, you can use the <see cref="ValueEqualityComparer"/> parameter to customize the judgment of the data model supporting joint primary keys</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    [Inject]
    [NotNull]
    private IStringLocalizer<MultiSelect<TValue>>? Localizer { get; set; }

    private string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth
        ? null
        : ScrollIntoViewBehavior.ToDescriptionString();

    [NotNull]
    private Virtualize<SelectedItem<TValue>>? _virtualizeElement = default;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        SelectAllText ??= Localizer[nameof(SelectAllText)];
        ReverseSelectText ??= Localizer[nameof(ReverseSelectText)];
        ClearText ??= Localizer[nameof(ClearText)];
        MinErrorMessage ??= Localizer[nameof(MinErrorMessage)];
        MaxErrorMessage ??= Localizer[nameof(MaxErrorMessage)];
        NoSearchDataText ??= Localizer[nameof(NoSearchDataText)];

        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.MultiSelectDropdownIcon);
        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.MultiSelectCloseIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.MultiSelectClearIcon);

        ResetRules();

        _itemsCache = null;

        if (IsVirtualize == false)
        {
            ResetSelectedItems();
        }
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        _isToggle = false;
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new
    {
        ConfirmMethodCallback = nameof(ConfirmSelectedItem),
        SearchMethodCallback = nameof(TriggerOnSearch),
        ToggleRow = nameof(ToggleRow)
    });

    /// <summary>
    ///  <para lang="zh">Triggers the search 回调方法.</para>
    ///  <para lang="en">Triggers the search callback method.</para>
    /// </summary>
    /// <param name="searchText">The search text.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [JSInvokable]
    public async Task TriggerOnSearch(string searchText)
    {
        _itemsCache = null;
        SearchText = searchText;
        await RefreshVirtualizeElement();
        StateHasChanged();
    }

    /// <summary>
    ///  <para lang="zh">Refreshes the virtualize component.</para>
    ///  <para lang="en">Refreshes the virtualize component.</para>
    /// </summary>
    /// <returns></returns>
    private async Task RefreshVirtualizeElement()
    {
        if (IsVirtualize && OnQueryAsync != null)
        {
            // <para lang="zh">通过 ItemProvider 提供数据</para>
            // <para lang="en">Data provided by ItemProvider</para>
            await _virtualizeElement.RefreshDataAsync();
        }
    }

    private List<SelectedItem<TValue>>? _itemsCache;
    /// <summary>
    ///  <para lang="zh"><para lang="zh">获得 the dropdown menu rows.</para>
    ///</para>
    ///  <para lang="en"><para lang="zh">Gets the dropdown menu rows.</para>
    ///</para>
    /// </summary>
    private List<SelectedItem<TValue>> Rows
    {
        get
        {
            _itemsCache ??= string.IsNullOrEmpty(SearchText) ? GetRowsByItems() : GetRowsBySearch();
            return _itemsCache;
        }
    }

    private List<SelectedItem<TValue>> GetRowsBySearch()
    {
        var items = OnSearchTextChanged?.Invoke(SearchText) ?? FilterBySearchText(GetRowsByItems());
        return [.. items];
    }

    private IEnumerable<SelectedItem<TValue>> FilterBySearchText(IEnumerable<SelectedItem<TValue>> source) => string.IsNullOrEmpty(SearchText)
        ? source
        : source.Where(i => i.Text.Contains(SearchText, StringComparison));

    private int _totalCount;
    private ItemsProviderResult<SelectedItem<TValue>> _result;

    private List<SelectedItem<TValue>> GetVirtualItems() => [.. FilterBySearchText(GetRowsByItems())];

    private async ValueTask<ItemsProviderResult<SelectedItem<TValue>>> LoadItems(ItemsProviderRequest request)
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
        _result = new ItemsProviderResult<SelectedItem<TValue>>(items, _totalCount);
        return _result;

        int GetCountByTotal() => _totalCount == 0 ? request.Count : Math.Min(request.Count, _totalCount - request.StartIndex);
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnClearValue()
    {
        await base.OnClearValue();

        SelectedItems.Clear();
        await SetValue();
    }

    private bool _isToggle;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    private List<SelectedItem<TValue>> GetRowsByItems()
    {
        var items = new List<SelectedItem<TValue>>();
        if (_result.Items != null)
        {
            items.AddRange(_result.Items);
        }
        else if (Items != null)
        {
            items.AddRange(Items);
        }
        return items;
    }

    /// <summary>
    ///  <para lang="zh">客户端回车回调方法</para>
    ///  <para lang="en">Client Enter Callback Method</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task ConfirmSelectedItem(int index)
    {
        var rows = Rows;
        if (index < rows.Count)
        {
            await ToggleItem(rows[index]);
            StateHasChanged();
        }
    }

    /// <summary>
    ///  <para lang="zh">切换当前选项方法</para>
    ///  <para lang="en">Toggle Current Option Method</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task ToggleRow(string val)
    {
        if (int.TryParse(val, out var index) && index >= 0 && index < SelectedItems.Count)
        {
            var item = SelectedItems[index];
            await ToggleRow(item);
        }
    }

    private async Task ToggleRow(SelectedItem<TValue> item)
    {
        SelectedItems.Remove(item);

        _isToggle = true;
        // <para lang="zh">更新选中值</para>
        // <para lang="en">Update selected value</para>
        await SetValue();
    }

    private string? GetValueString(SelectedItem<TValue> item) => IsPopover ? SelectedItems.IndexOf(item).ToString() : null;

    private async Task ToggleItem(SelectedItem<TValue> val)
    {
        var item = SelectedItems.FirstOrDefault(i => Equals(i.Value, val.Value));
        if (item != null)
        {
            SelectedItems.Remove(item);
        }
        else
        {
            var d = Rows.FirstOrDefault(i => Equals(i.Value, val.Value));
            if (d != null)
            {
                SelectedItems.Add(d);
            }
        }

        _isToggle = true;
        // <para lang="zh">更新选中值</para>
        // <para lang="en">Update selected value</para>
        await SetValue();
    }

    private int _min;
    private int _max;
    private void ResetRules()
    {
        if (Max != _max)
        {
            _max = Max;
            Rules.RemoveAll(v => v is MaxValidator);

            if (Max > 0)
            {
                Rules.Add(new MaxValidator() { Value = Max, ErrorMessage = MaxErrorMessage });
            }
        }

        if (Min != _min)
        {
            _min = Min;
            Rules.RemoveAll(v => v is MinValidator);

            if (Min > 0)
            {
                Rules.Add(new MinValidator() { Value = Min, ErrorMessage = MinErrorMessage });
            }
        }
    }

    private async Task SetValue()
    {
        if (ValidateForm == null && (Min > 0 || Max > 0))
        {
            var validationContext = new ValidationContext(Value!) { MemberName = FieldIdentifier?.FieldName };
            var validationResults = new List<ValidationResult>();

            await ValidatePropertyAsync(CurrentValue, validationContext, validationResults);
            await ToggleMessage(validationResults);
        }

        if (OnSelectedItemsChanged != null)
        {
            await OnSelectedItemsChanged(SelectedItems);
        }

        CurrentValue = [.. SelectedItems.Select(i => i.Value)];
        if (!ValueChanged.HasDelegate)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    ///  <para lang="zh">清除选择项方法</para>
    ///  <para lang="en">Clear Selected Items Method</para>
    /// </summary>
    /// <returns></returns>
    public async Task Clear()
    {
        SelectedItems.Clear();
        await SetValue();
    }

    /// <summary>
    ///  <para lang="zh">全选选择项方法</para>
    ///  <para lang="en">Select All Items Method</para>
    /// </summary>
    /// <returns></returns>
    public async Task SelectAll()
    {
        SelectedItems.Clear();
        SelectedItems.AddRange(Rows);
        await SetValue();
    }

    /// <summary>
    ///  <para lang="zh">翻转选择项方法</para>
    ///  <para lang="en">Invert Selection Method</para>
    /// </summary>
    /// <returns></returns>
    public async Task InvertSelect()
    {
        var items = Rows.Where(item => !SelectedItems.Any(i => Equals(i.Value, item.Value))).ToList();
        SelectedItems.Clear();
        SelectedItems.AddRange(items);
        await SetValue();
    }

    private bool GetCheckedState(SelectedItem<TValue> item) => SelectedItems.Any(i => Equals(i.Value, item.Value));

    private string? GetCheckedString(SelectedItem<TValue> item) => GetCheckedState(item) ? "checked" : null;

    private bool CheckCanTrigger(SelectedItem<TValue> item)
    {
        var ret = true;
        if (Max > 0)
        {
            ret = SelectedItems.Count < Max || GetCheckedState(item);
        }
        return ret;
    }

    private bool CheckCanSelect(SelectedItem<TValue> item)
    {
        var ret = GetCheckedState(item);
        if (!ret)
        {
            ret = CheckCanTrigger(item);
        }
        return !ret;
    }

    /// <summary>
    ///  <para lang="zh">客户端检查完成时调用此方法</para>
    ///  <para lang="en">Client Validation Completed Callback Method</para>
    /// </summary>
    /// <param name="valid"></param>
    protected override void OnValidate(bool? valid)
    {
        if (valid != null)
        {
            Color = valid.Value ? Color.Success : Color.Danger;
        }
    }

    private void ResetSelectedItems()
    {
        SelectedItems.Clear();
        if (Value != null)
        {
            foreach (var v in Value)
            {
                var item = Rows.Find(i => Equals(i.Value, v));
                if (item != null)
                {
                    SelectedItems.Add(item);
                }
            }
        }
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(TValue? x, TValue? y) => this.Equals<TValue>(x, y);
}

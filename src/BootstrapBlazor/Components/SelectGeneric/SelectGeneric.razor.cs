// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Select 泛型组件实现类</para>
/// <para lang="en">Select Generic Component Implementation Class</para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
[CascadingTypeParameter(nameof(TValue))]
public partial class SelectGeneric<TValue> : ISelectGeneric<TValue>, IModelEqualityComparer<TValue>
{
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Class Name</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("select dropdown")
        .AddClass("is-clearable", IsClearable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Input Class Name</para>
    /// </summary>
    private string? InputClassString => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <para lang="zh">设置当前项是否 Active 方法</para>
    /// <para lang="en">Set Current Item Active Method</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? ActiveItem(SelectedItem<TValue> item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", Equals(item.Value, Value))
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private readonly List<SelectedItem<TValue>> _children = [];

    /// <summary>
    /// <para lang="zh">获得/设置 搜索文本发生变化时回调此方法</para>
    /// <para lang="en">Callback method when search text changes</para>
    /// </summary>
    [Parameter]
    public Func<string, IEnumerable<SelectedItem<TValue>>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可编辑 默认 false</para>
    /// <para lang="en">Get/Set Whether editable. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项输入更新后回调方法 默认 null</para>
    /// <para lang="en">Callback method after option input update. Default null</para>
    /// </summary>
    /// <remarks>设置 <see cref="IsEditable"/> 后生效</remarks>
    [Parameter]
    public Func<string, Task>? OnInputChangedCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项输入更新后转换为 Value 回调方法 默认 null</para>
    /// <para lang="en">Callback method to convert option input update to Value. Default null</para>
    /// <para lang="zh">返回值为 null 时放弃操作</para>
    /// <para lang="en">Discard operation when return value is null</para>
    /// </summary>
    /// <remarks>设置 <see cref="IsEditable"/> 后生效</remarks>
    [Parameter]
    public Func<string, Task<TValue?>>? TextConvertToValueCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项模板支持静态数据</para>
    /// <para lang="en">Get/Set Option template supports static data</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Options { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示部分模板 默认 null</para>
    /// <para lang="en">Get/Set Display Template. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem<TValue>?>? DisplayTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 禁止首次加载时触发 OnSelectedItemChanged 回调方法 默认 false</para>
    /// <para lang="en">Get/Set Disable triggering OnSelectedItemChanged callback on first load. Default false</para>
    /// </summary>
    [Parameter]
    public bool DisableItemChangedWhenFirstRender { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 比较数据是否相同回调方法 默认为 null</para>
    /// <para lang="en">Get/Set Value Equality Comparer. Default null</para>
    /// <para lang="zh">提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</para>
    /// <para lang="en">Ignore <see cref="CustomKeyAttribute"/> when providing this callback</para>
    /// </summary>
    [Parameter]
    public Func<TValue, TValue, bool>? ValueEqualityComparer { get; set; }

    Func<TValue, TValue, bool>? IModelEqualityComparer<TValue>.ModelEqualityComparer
    {
        get => ValueEqualityComparer;
        set => ValueEqualityComparer = value;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 数据主键标识标签 默认为 <see cref="KeyAttribute"/>用于判断数据主键标签，如果模型未设置主键时可使用 <see cref="ValueEqualityComparer"/> 参数自定义判断数据模型支持联合主键</para>
    /// <para lang="en">Get/Set Identifier tag for data primary key. Default is <see cref="KeyAttribute"/>. Used to determine date primary key tag. If the model does not set a primary key, you can use the <see cref="ValueEqualityComparer"/> parameter to customize the judgment of the data model supporting joint primary keys</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    [NotNull]
    private Virtualize<SelectedItem<TValue>>? VirtualizeElement { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 绑定数据集</para>
    /// <para lang="en">Get/Set Bound Dataset</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem<TValue>>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项模板</para>
    /// <para lang="en">Get/Set Item Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem<TValue>>? ItemTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉框项目改变前回调委托方法 返回 true 时选项值改变，否则选项值不变</para>
    /// <para lang="en">Get/Set Callback delegate before dropdown item changes. Return true to change option value, otherwise value remains unchanged</para>
    /// </summary>
    [Parameter]
    public Func<SelectedItem<TValue>, Task<bool>>? OnBeforeSelectedItemChange { get; set; }

    /// <summary>
    /// <para lang="zh">SelectedItemChanged 回调方法</para>
    /// <para lang="en">SelectedItemChanged Callback Method</para>
    /// </summary>
    [Parameter]
    public Func<SelectedItem<TValue>, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Swal 图标 默认 Question</para>
    /// <para lang="en">Get/Set Swal Icon. Default Question</para>
    /// </summary>
    [Parameter]
    public SwalCategory SwalCategory { get; set; } = SwalCategory.Question;

    /// <summary>
    /// <para lang="zh">获得/设置 Swal 标题 默认 null</para>
    /// <para lang="en">Get/Set Swal Title. Default null</para>
    /// </summary>
    [Parameter]
    public string? SwalTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Swal 内容 默认 null</para>
    /// <para lang="en">Get/Set Swal Content. Default null</para>
    /// </summary>
    [Parameter]
    public string? SwalContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 默认 null</para>
    /// <para lang="en">Get/Set Footer. Default null</para>
    /// </summary>
    [Parameter]
    public string? SwalFooter { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TValue>>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得 input 组件 Id 方法</para>
    /// <para lang="en">Get input Component Id Method</para>
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// <para lang="zh">获得/设置 Select 内部 Input 组件 Id</para>
    /// <para lang="en">Get/Set Select Internal Input Component Id</para>
    /// </summary>
    private string? InputId => $"{Id}_input";

    private TValue? _lastSelectedValue;

    private bool _init = true;

    private List<SelectedItem<TValue>>? _itemsCache;

    private ItemsProviderResult<SelectedItem<TValue>> _result;

    private string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollIntoViewBehavior.ToDescriptionString();

    /// <summary>
    /// <para lang="zh">当前选择项实例</para>
    /// <para lang="en">Current Selected Item Instance</para>
    /// </summary>
    private SelectedItem<TValue>? SelectedItem { get; set; }

    private List<SelectedItem<TValue>> Rows
    {
        get
        {
            _itemsCache ??= string.IsNullOrEmpty(SearchText) ? GetRowsByItems() : GetRowsBySearch();
            return _itemsCache;
        }
    }

    private SelectedItem<TValue>? SelectedRow
    {
        get
        {
            SelectedItem ??= GetSelectedRow();
            return SelectedItem;
        }
    }

    private SelectedItem<TValue>? GetSelectedRow()
    {
        if (Value is null)
        {
            _init = false;
            return null;
        }

        if (IsVirtualize)
        {
            _init = false;
            return new SelectedItem<TValue>(default!, CurrentValueAsString);
        }

        var item = Rows.Find(i => Equals(i.Value, Value))
            ?? Rows.Find(i => i.Active)
            ?? Rows.Where(i => !i.IsDisabled).FirstOrDefault();

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

    private List<SelectedItem<TValue>> GetRowsByItems()
    {
        var items = new List<SelectedItem<TValue>>();
        items.AddRange(Items);
        items.AddRange(_children);
        return items;
    }

    private List<SelectedItem<TValue>> GetRowsBySearch()
    {
        var items = OnSearchTextChanged?.Invoke(SearchText) ?? FilterBySearchText(GetRowsByItems());
        return [.. items];
    }

    private IEnumerable<SelectedItem<TValue>> FilterBySearchText(IEnumerable<SelectedItem<TValue>> source) => string.IsNullOrEmpty(SearchText)
        ? source
        : source.Where(i => i.Text.Contains(SearchText, StringComparison));

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        NoSearchDataText ??= Localizer[nameof(NoSearchDataText)];
        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectDropdownIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectClearIcon);

        // <para lang="zh">内置对枚举类型的支持</para>
        // <para lang="en">Built-in support for enum types</para>
        if (!Items.Any() && ValueType.IsEnum())
        {
            var item = NullableUnderlyingType == null ? "" : PlaceHolder;
            Items = ValueType.ToSelectList<TValue>(string.IsNullOrEmpty(item) ? null : new SelectedItem<TValue>(default!, item));
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

    /// <summary>
    /// <para lang="zh">获得/设置 数据总条目</para>
    /// <para lang="en">Get/Set Total Count</para>
    /// </summary>
    private int TotalCount { get; set; }

    private List<SelectedItem<TValue>> GetVirtualItems() => [.. FilterBySearchText(GetRowsByItems())];

    /// <summary>
    /// <para lang="zh">虚拟滚动数据加载回调方法</para>
    /// <para lang="en">Virtual Scroll Data Load Callback Method</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<SelectedItem<TValue>>>>? OnQueryAsync { get; set; }

    private async ValueTask<ItemsProviderResult<SelectedItem<TValue>>> LoadItems(ItemsProviderRequest request)
    {
        // <para lang="zh">有搜索条件时使用原生请求数量</para>
        // <para lang="en">Use original request count when there is search condition</para>
        // <para lang="zh">有总数时请求剩余数量</para>
        // <para lang="en">Request remaining count when there is total count</para>
        var count = !string.IsNullOrEmpty(SearchText) ? request.Count : GetCountByTotal();
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = count, SearchText = SearchText });

        TotalCount = data.TotalCount;
        var items = data.Items ?? [];
        _result = new ItemsProviderResult<SelectedItem<TValue>>(items, TotalCount);
        return _result;

        int GetCountByTotal() => TotalCount == 0 ? request.Count : Math.Min(request.Count, TotalCount - request.StartIndex);
    }

    private async Task RefreshVirtualizeElement()
    {
        if (IsVirtualize && OnQueryAsync != null)
        {
            // <para lang="zh">通过 ItemProvider 提供数据</para>
            // <para lang="en">Data provided by ItemProvider</para>
            await VirtualizeElement.RefreshDataAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new { ConfirmMethodCallback = nameof(ConfirmSelectedItem), SearchMethodCallback = nameof(TriggerOnSearch) });

    /// <summary>
    /// <para lang="zh">客户端回车回调方法</para>
    /// <para lang="en">Client Enter Callback Method</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
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
    /// <para lang="zh">客户端搜索栏回调方法</para>
    /// <para lang="en">Client Search Bar Callback Method</para>
    /// </summary>
    /// <param name="searchText"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerOnSearch(string searchText)
    {
        _itemsCache = null;
        SearchText = searchText;
        await RefreshVirtualizeElement();
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">下拉框选项点击时调用此方法</para>
    /// <para lang="en">Called when dropdown option is clicked</para>
    /// </summary>
    private async Task OnClickItem(SelectedItem<TValue> item)
    {
        var ret = true;
        if (OnBeforeSelectedItemChange != null)
        {
            ret = await OnBeforeSelectedItemChange(item);
            if (ret)
            {
                // <para lang="zh">返回 True 弹窗提示</para>
                // <para lang="en">Return True to show popup</para>
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
                // <para lang="zh">返回 False 直接运行</para>
                // <para lang="en">Return False to run directly</para>
                ret = true;
            }
        }
        if (ret)
        {
            await SelectedItemChanged(item);
        }
    }

    private async Task SelectedItemChanged(SelectedItem<TValue> item)
    {
        if (!Equals(item.Value, Value))
        {
            item.Active = true;
            SelectedItem = item;

            CurrentValue = item.Value;

            // <para lang="zh">触发 SelectedItemChanged 事件</para>
            // <para lang="en">Trigger SelectedItemChanged Event</para>
            if (OnSelectedItemChanged != null)
            {
                await OnSelectedItemChanged(SelectedItem);
            }
        }
        else
        {
            await ValueTypeChanged(item);
        }
    }

    private async Task ValueTypeChanged(SelectedItem<TValue> item)
    {
        if (!Equals(_lastSelectedValue, item.Value))
        {
            _lastSelectedValue = item.Value;

            item.Active = true;
            SelectedItem = item;

            // <para lang="zh">触发 StateHasChanged</para>
            // <para lang="en">Trigger StateHasChanged</para>
            CurrentValue = item.Value;
            // <para lang="zh">触发 SelectedItemChanged 事件</para>
            // <para lang="en">Trigger SelectedItemChanged Event</para>
            if (OnSelectedItemChanged != null)
            {
                await OnSelectedItemChanged(SelectedItem);
            }
        }
    }

    /// <summary>
    /// <para lang="zh">添加静态下拉项方法</para>
    /// <para lang="en">Add Static Dropdown Item Method</para>
    /// </summary>
    /// <param name="item"></param>
    public void Add(SelectedItem<TValue> item) => _children.Add(item);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnClearValue()
    {
        await base.OnClearValue();

        if (OnQueryAsync != null)
        {
            await VirtualizeElement.RefreshDataAsync();
        }
        SelectedItem = new SelectedItem<TValue>(default!, "");
        if (OnSelectedItemChanged != null)
        {
            await OnSelectedItemChanged(SelectedItem);
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
                TValue? val = default;
                if (TextConvertToValueCallback != null)
                {
                    val = await TextConvertToValueCallback(v);
                }

                if (val is not null)
                {
                    // <para lang="zh">返回值时，由外部更新 Items 参数</para>
                    // <para lang="en">Return value, update Items parameter externally</para>
                    CurrentValue = val;
                }
                else
                {
                    // <para lang="zh">返回空值时恢复上次选中值</para>
                    // <para lang="en">Return empty value, restore last selected value</para>
                    await InvokeVoidAsync("resetValue", InputId, SelectedRow?.Text);
                }
            }
            else
            {
                CurrentValue = item.Value;
            }

            if (OnInputChangedCallback != null)
            {
                await OnInputChangedCallback(v);
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(TValue? x, TValue? y) => this.Equals<TValue>(x, y);
}

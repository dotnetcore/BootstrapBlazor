// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件实现类
/// </summary>
/// <typeparam name="TValue"></typeparam>
[CascadingTypeParameter(nameof(TValue))]
public partial class SelectGeneric<TValue> : ISelectGeneric<TValue>, IModelEqualityComparer<TValue>
{
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassString => CssBuilder.Default("select dropdown")
        .AddClass("is-clearable", IsClearable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? InputClassString => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 设置当前项是否 Active 方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? ActiveItem(SelectedItem<TValue> item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", Equals(item.Value, Value))
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private readonly List<SelectedItem<TValue>> _children = [];

    /// <summary>
    /// 获得/设置 搜索文本发生变化时回调此方法
    /// </summary>
    [Parameter]
    public Func<string, IEnumerable<SelectedItem<TValue>>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否可编辑 默认 false
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// 获得/设置 选项输入更新后回调方法 默认 null
    /// </summary>
    /// <remarks>设置 <see cref="IsEditable"/> 后生效</remarks>
    [Parameter]
    public Func<string, Task>? OnInputChangedCallback { get; set; }

    /// <summary>
    /// 获得/设置 选项输入更新后转换为 Value 回调方法 默认 null
    /// </summary>
    /// <remarks>设置 <see cref="IsEditable"/> 后生效</remarks>
    [Parameter]
    public Func<string, Task<TValue>>? TextConvertToValueCallback { get; set; }

    /// <summary>
    /// 获得/设置 选项模板支持静态数据
    /// </summary>
    [Parameter]
    public RenderFragment? Options { get; set; }

    /// <summary>
    /// 获得/设置 显示部分模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem<TValue>?>? DisplayTemplate { get; set; }

    /// <summary>
    /// 获得/设置 禁止首次加载时触发 OnSelectedItemChanged 回调方法 默认 false
    /// </summary>
    [Parameter]
    public bool DisableItemChangedWhenFirstRender { get; set; }

    /// <summary>
    /// 获得/设置 比较数据是否相同回调方法 默认为 null
    /// <para>提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</para>
    /// </summary>
    [Parameter]
    public Func<TValue, TValue, bool>? ValueEqualityComparer { get; set; }

    Func<TValue, TValue, bool>? IModelEqualityComparer<TValue>.ModelEqualityComparer
    {
        get => ValueEqualityComparer;
        set => ValueEqualityComparer = value;
    }

    /// <summary>
    /// 获得/设置 数据主键标识标签 默认为 <see cref="KeyAttribute"/>用于判断数据主键标签，如果模型未设置主键时可使用 <see cref="ValueEqualityComparer"/> 参数自定义判断数据模型支持联合主键
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    [NotNull]
    private Virtualize<SelectedItem<TValue>>? VirtualizeElement { get; set; }

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem<TValue>>? Items { get; set; }

    /// <summary>
    /// 获得/设置 选项模板
    /// </summary>
    [Parameter]
    public RenderFragment<SelectedItem<TValue>>? ItemTemplate { get; set; }

    /// <summary>
    /// 获得/设置 下拉框项目改变前回调委托方法 返回 true 时选项值改变，否则选项值不变
    /// </summary>
    [Parameter]
    public Func<SelectedItem<TValue>, Task<bool>>? OnBeforeSelectedItemChange { get; set; }

    /// <summary>
    /// SelectedItemChanged 回调方法
    /// </summary>
    [Parameter]
    public Func<SelectedItem<TValue>, Task>? OnSelectedItemChanged { get; set; }

    /// <summary>
    /// 获得/设置 Swal 图标 默认 Question
    /// </summary>
    [Parameter]
    public SwalCategory SwalCategory { get; set; } = SwalCategory.Question;

    /// <summary>
    /// 获得/设置 Swal 标题 默认 null
    /// </summary>
    [Parameter]
    public string? SwalTitle { get; set; }

    /// <summary>
    /// 获得/设置 Swal 内容 默认 null
    /// </summary>
    [Parameter]
    public string? SwalContent { get; set; }

    /// <summary>
    /// 获得/设置 Footer 默认 null
    /// </summary>
    [Parameter]
    public string? SwalFooter { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TValue>>? Localizer { get; set; }

    /// <summary>
    /// 获得 input 组件 Id 方法
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// 获得/设置 Select 内部 Input 组件 Id
    /// </summary>
    private string? InputId => $"{Id}_input";

    private TValue? _lastSelectedValue;

    private bool _init = true;

    private List<SelectedItem<TValue>>? _itemsCache;

    private ItemsProviderResult<SelectedItem<TValue>> _result;

    private string? ScrollIntoViewBehaviorString => ScrollIntoViewBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollIntoViewBehavior.ToDescriptionString();

    /// <summary>
    /// 当前选择项实例
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

        // 内置对枚举类型的支持
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
    /// 获得/设置 数据总条目
    /// </summary>
    private int TotalCount { get; set; }

    private List<SelectedItem<TValue>> GetVirtualItems() => [.. FilterBySearchText(GetRowsByItems())];

    /// <summary>
    /// 虚拟滚动数据加载回调方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<SelectedItem<TValue>>>>? OnQueryAsync { get; set; }

    private async ValueTask<ItemsProviderResult<SelectedItem<TValue>>> LoadItems(ItemsProviderRequest request)
    {
        // 有搜索条件时使用原生请求数量
        // 有总数时请求剩余数量
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
            // 通过 ItemProvider 提供数据
            await VirtualizeElement.RefreshDataAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new { ConfirmMethodCallback = nameof(ConfirmSelectedItem), SearchMethodCallback = nameof(TriggerOnSearch) });

    /// <summary>
    /// 客户端回车回调方法
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
    /// 客户端搜索栏回调方法
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
    /// 下拉框选项点击时调用此方法
    /// </summary>
    private async Task OnClickItem(SelectedItem<TValue> item)
    {
        var ret = true;
        if (OnBeforeSelectedItemChange != null)
        {
            ret = await OnBeforeSelectedItemChange(item);
            if (ret)
            {
                // 返回 True 弹窗提示
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
                // 返回 False 直接运行
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

            // 触发 SelectedItemChanged 事件
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

            // 触发 StateHasChanged
            CurrentValue = item.Value;

            // 触发 SelectedItemChanged 事件
            if (OnSelectedItemChanged != null)
            {
                await OnSelectedItemChanged(SelectedItem);
            }
        }
    }

    /// <summary>
    /// 添加静态下拉项方法
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
    }

    private string? ReadonlyString => IsEditable ? null : "readonly";

    private async Task OnChange(ChangeEventArgs args)
    {
        if (args.Value is string v)
        {
            // Items 中没有时插入一个 SelectedItem
            var item = Items.FirstOrDefault(i => i.Text == v);

            if (item == null)
            {
                TValue val = default!;
                if (TextConvertToValueCallback != null)
                {
                    val = await TextConvertToValueCallback(v);
                }
                item = new SelectedItem<TValue>(val, v);

                var items = new List<SelectedItem<TValue>>() { item };
                items.AddRange(Items);
                Items = items;
                CurrentValue = val;
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

﻿// Licensed to the .NET Foundation under one or more agreements.
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
        .AddClass("cls", IsClearable)
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

    private string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private bool GetClearable() => IsClearable && !IsDisabled;

    /// <summary>
    /// 设置当前项是否 Active 方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? ActiveItem(SelectedItem<TValue> item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", Equals(item.Value, Value))
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private string? SearchClassString => CssBuilder.Default("search")
        .AddClass("is-fixed", IsFixedSearch)
        .Build();

    private readonly List<SelectedItem<TValue>> _children = [];

    /// <summary>
    /// 获得/设置 右侧清除图标 默认 fa-solid fa-angle-up
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// 获得/设置 搜索文本发生变化时回调此方法
    /// </summary>
    [Parameter]
    public Func<string, IEnumerable<SelectedItem<TValue>>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否固定下拉框中的搜索栏 默认 false
    /// </summary>
    [Parameter]
    public bool IsFixedSearch { get; set; }

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
    /// 获得/设置 无搜索结果时显示文字
    /// </summary>
    [Parameter]
    public string? NoSearchDataText { get; set; }

    /// <summary>
    /// 获得 PlaceHolder 属性
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 是否可清除 默认 false
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

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
    /// 获得/设置 是否开启虚拟滚动 默认 false 未开启 注意：开启虚拟滚动后不支持 <see cref="SelectBase{TValue}.ShowSearch"/> <see cref="PopoverSelectBase{TValue}.IsPopover"/> <seealso cref="IsFixedSearch"/> 参数设置，设置初始值时请设置 <see cref="DefaultVirtualizeItemText"/>
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// 获得/设置 虚拟滚动行高 默认为 33
    /// </summary>
    /// <remarks>需要设置 <see cref="IsVirtualize"/> 值为 true 时生效</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 33f;

    /// <summary>
    /// 获得/设置 过载阈值数 默认为 4
    /// </summary>
    /// <remarks>需要设置 <see cref="IsVirtualize"/> 值为 true 时生效</remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 4;

    /// <summary>
    /// 获得/设置 默认文本 <see cref="IsVirtualize"/> 时生效 默认 null
    /// </summary>
    /// <remarks>开启 <see cref="IsVirtualize"/> 并且通过 <see cref="OnQueryAsync"/> 提供数据源时，由于渲染时还未调用或者调用后数据集未包含 <see cref="DisplayBase{TValue}.Value"/> 选项值，此时使用 DefaultText 值渲染</remarks>
    [Parameter]
    public string? DefaultVirtualizeItemText { get; set; }

    /// <summary>
    /// 获得/设置 清除文本内容 OnClear 回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

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

    private SelectedItem<TValue> SelectedRow
    {
        get
        {
            SelectedItem ??= GetSelectedRow();
            return SelectedItem;
        }
    }

    private SelectedItem<TValue> GetSelectedRow()
    {
        var item = Rows.Find(i => Equals(i.Value, Value))
            ?? Rows.Find(i => i.Active)
            ?? Rows.Where(i => !i.IsDisabled).FirstOrDefault()
            ?? new SelectedItem<TValue>(Value, DefaultVirtualizeItemText!);

        if (!_init || !DisableItemChangedWhenFirstRender)
        {
            _ = SelectedItemChanged(item);
            _init = false;
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
        return items.ToList();
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
    /// 获得/设置 数据总条目
    /// </summary>
    private int TotalCount { get; set; }

    private List<SelectedItem<TValue>> GetVirtualItems() => FilterBySearchText(GetRowsByItems()).ToList();

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

    private async Task SearchTextChanged(string val)
    {
        SearchText = val;
        _itemsCache = null;

        if (OnQueryAsync != null)
        {
            // 通过 ItemProvider 提供数据
            await VirtualizeElement.RefreshDataAsync();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(ConfirmSelectedItem));

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
    /// 清空搜索栏文本内容
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

        SelectedItem<TValue>? item;
        if (OnQueryAsync != null)
        {
            await VirtualizeElement.RefreshDataAsync();
            item = _result.Items.FirstOrDefault();
        }
        else
        {
            item = Items.FirstOrDefault();
        }
        if (item != null)
        {
            await SelectedItemChanged(item);
        }
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
                TValue? val = default;
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

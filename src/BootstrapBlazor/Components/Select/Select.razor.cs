// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件实现类
/// </summary>
/// <typeparam name="TValue"></typeparam>
public partial class Select<TValue> : ISelect
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

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? AppendClassString => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
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
    private string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", () => item.Value == CurrentValueAsString)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private string? SearchClassString => CssBuilder.Default("search")
        .AddClass("is-fixed", IsFixedSearch)
        .Build();

    /// <summary>
    /// Razor 文件中 Options 模板子项
    /// </summary>
    private List<SelectedItem> Children { get; } = [];

    [NotNull]
    private List<SelectedItem> DataSource { get; } = [];

    /// <summary>
    /// 获得/设置 右侧下拉箭头图标 默认 fa-solid fa-angle-up
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

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
    [NotNull]
    public Func<string, IEnumerable<SelectedItem>>? OnSearchTextChanged { get; set; }

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
    public RenderFragment<SelectedItem?>? DisplayTemplate { get; set; }

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
    /// 获得/设置 禁止首次加载时触发 OnSelectedItemChanged 回调方法 默认 false
    /// </summary>
    [Parameter]
    public bool DisableItemChangedWhenFirstRender { get; set; }

    [NotNull]
    private Virtualize<SelectedItem>? VirtualizeElement { get; set; }

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

    private string _lastSelectedValueString = string.Empty;

    private bool _init;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
        OnSearchTextChanged ??= text => Items.Where(i => i.Text.Contains(text, StringComparison));
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        NoSearchDataText ??= Localizer[nameof(NoSearchDataText)];
        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectDropdownIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectClearIcon);

        // 内置对枚举类型的支持
        if (!Items.Any() && ValueType.IsEnum())
        {
            var item = NullableUnderlyingType == null ? "" : PlaceHolder;
            Items = ValueType.ToSelectList(string.IsNullOrEmpty(item) ? null : new SelectedItem("", item));
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            _init = true;
        }
    }

    /// <summary>
    /// 获得/设置 数据总条目
    /// </summary>
    private int TotalCount { get; set; }

    private IEnumerable<SelectedItem>? VirtualItems { get; set; }

    private ICollection<SelectedItem> GetVirtualItems() => (VirtualItems ?? Items).ToList();

    /// <summary>
    /// 虚拟滚动数据加载回调方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<VirtualizeQueryOption, Task<QueryData<SelectedItem>>>? OnQueryAsync { get; set; }

    private async ValueTask<ItemsProviderResult<SelectedItem>> LoadItems(ItemsProviderRequest request)
    {
        // 有搜索条件时使用原生请求数量
        // 有总数时请求剩余数量
        var count = !string.IsNullOrEmpty(SearchText) ? request.Count : GetCountByTotal();
        var data = await OnQueryAsync(new() { StartIndex = request.StartIndex, Count = count, SearchText = SearchText });

        TotalCount = data.TotalCount;
        VirtualItems = data.Items ?? [];
        return new ItemsProviderResult<SelectedItem>(VirtualItems, TotalCount);

        int GetCountByTotal() => TotalCount == 0 ? request.Count : Math.Min(request.Count, TotalCount - request.StartIndex);
    }

    private async Task SearchTextChanged(string val)
    {
        SearchText = val;
        if (OnQueryAsync == null)
        {
            // 通过 Items 提供数据
            VirtualItems = OnSearchTextChanged(SearchText);
        }
        else
        {
            // 通过 ItemProvider 提供数据
            await VirtualizeElement.RefreshDataAsync();
        }
        StateHasChanged();
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
        SelectedItem = (VirtualItems ?? DataSource).FirstOrDefault(i => i.Value == value) ?? GetVirtualizeItem();

        // support SelectedItem? type
        result = SelectedItem != null ? (TValue)(object)SelectedItem : default;
        validationErrorMessage = "";
        return SelectedItem != null;
    }

    private SelectedItem? GetVirtualizeItem()
    {
        return OnQueryAsync == null ? null : GetSelectedItem();

        SelectedItem? GetSelectedItem() => ValueType == typeof(SelectedItem)
            ? (SelectedItem)(object)Value
            : new SelectedItem(CurrentValueAsString, DefaultVirtualizeItemText ?? CurrentValueAsString);
    }

    private void ResetSelectedItem()
    {
        DataSource.Clear();

        if (string.IsNullOrEmpty(SearchText))
        {
            DataSource.AddRange(Items);
            DataSource.AddRange(Children);

            if (VirtualItems != null)
            {
                DataSource.AddRange(VirtualItems);
            }

            SelectedItem = DataSource.Find(i => i.Value.Equals(CurrentValueAsString, StringComparison))
                ?? DataSource.Find(i => i.Active)
                ?? DataSource.Where(i => !i.IsDisabled).FirstOrDefault()
                ?? GetVirtualizeItem();

            if (SelectedItem != null && ((_init || !DisableItemChangedWhenFirstRender)))
            {
                _ = SelectedItemChanged(SelectedItem);
            }
        }
        else if (IsVirtualize)
        {
            if (Items.Any())
            {
                VirtualItems = OnSearchTextChanged(SearchText);
            }
        }
        else
        {
            DataSource.AddRange(OnSearchTextChanged(SearchText));
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
        var ds = string.IsNullOrEmpty(SearchText)
            ? DataSource
            : OnSearchTextChanged(SearchText);
        var item = ds.ElementAt(index);
        await OnClickItem(item);
        StateHasChanged();
    }

    /// <summary>
    /// 下拉框选项点击时调用此方法
    /// </summary>
    private async Task OnClickItem(SelectedItem item)
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

    private async Task SelectedItemChanged(SelectedItem item)
    {
        if (_lastSelectedValueString != item.Value)
        {
            _lastSelectedValueString = item.Value;

            item.Active = true;
            SelectedItem = item;

            // 触发 StateHasChanged
            CurrentValueAsString = item.Value;

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
    public void Add(SelectedItem item) => Children.Add(item);

    /// <summary>
    /// 清空搜索栏文本内容
    /// </summary>
    public void ClearSearchText() => SearchText = null;

    private void OnClearValue()
    {
        CurrentValue = default;
    }

    private async Task OnChange(ChangeEventArgs args)
    {
        if (args.Value is string v)
        {
            // Items 中没有时插入一个 SelectedItem
            if (Items.FirstOrDefault(i => i.Text == v) == null)
            {
                var items = new List<SelectedItem>
                {
                    new(v, v)
                };
                items.AddRange(Items);
                Items = items;
            }
            if (OnInputChangedCallback != null)
            {
                await OnInputChangedCallback(v);
            }
            CurrentValueAsString = v;
        }
    }
}

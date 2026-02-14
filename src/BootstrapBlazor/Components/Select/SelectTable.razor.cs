// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">下拉表格组件实现类</para>
/// <para lang="en">SelectTable Component Implementation Class</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
[CascadingTypeParameter(nameof(TItem))]
public partial class SelectTable<TItem> : IColumnCollection where TItem : class, new()
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否为多选模式，默认值为 false</para>
    /// <para lang="en">Gets or sets Multiple Selection Mode. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsMultipleSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 多选模式下组件最大高度，默认值为 null 使用样式默认值 65px</para>
    /// <para lang="en">Gets or sets Maximum height in multiple selection mode. Default null</para>
    /// </summary>
    [Parameter]
    public string? MultiSelectedItemsMaxHeight { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 多选模式下组件显示最大数量，超过数量用数字显示，默认值为 8 </para>
    /// <para lang="en">Gets or sets the maximum number of components is displayed in multiple selection mode; any quantity exceeding this is displayed as a number. Default value is 8</para>
    /// </summary>
    [Parameter]
    public int MultiSelectedItemsMaxDisplayCount { get; set; } = 8;

    /// <summary>
    /// <para lang="zh">获得/设置 多选模式下组件显示最大数量标签颜色，默认值 <see cref="Color.None"/> 未设置</para>
    /// <para lang="en">Gets or sets the color of maximum number of components is displayed in multiple selection mode. Default value is <see cref="Color.None"/></para>
    /// </summary>
    [Parameter]
    public Color MultiSelectedItemsMaxDisplayCountColor { get; set; } = Color.None;

    /// <summary>
    /// <para lang="zh">获得/设置 多选模式下已选择项集合 默认 null</para>
    /// <para lang="en">Gets or sets the selected items collection in multiple selection mode. Default null</para>
    /// </summary>
    [Parameter]
    public List<TItem> SelectedItems { get; set; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 多选模式下已选择项集合变化回调方法</para>
    /// <para lang="en">Gets or sets the callback method when selected items collection changes in multiple selection mode</para>
    /// </summary>
    [Parameter]
    public EventCallback<List<TItem>> SelectedItemsChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 TableHeader 实例</para>
    /// <para lang="en">Gets or sets TableHeader Instance</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? TableColumns { get; set; }

    /// <summary>
    /// <para lang="zh">异步查询回调方法</para>
    /// <para lang="en">Async Query Callback Method</para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 颜色 默认 <see cref="Color.None"/> 无设置</para>
    /// <para lang="en">Gets or sets Color. Default <see cref="Color.None"/></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示组件右侧扩展箭头 默认 true 显示</para>
    /// <para lang="en">Gets or sets Whether to show the component right extension arrow. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowAppendArrow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗表格最小宽度 默认为 null 未设置使用样式中的默认值 602px</para>
    /// <para lang="en">Gets or sets Dropdown Table Min Width. Default null (use style default 602px)</para>
    /// </summary>
    [Parameter]
    public int? TableMinWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得 显示文字回调方法 默认 null</para>
    /// <para lang="en">Get Display Text Callback Method. Default null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public Func<TItem, string?>? GetTextCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧下拉箭头图标 默认 fa-solid fa-angle-up</para>
    /// <para lang="en">Gets or sets Dropdown Icon. Default fa-solid fa-angle-up</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可清除 默认 false</para>
    /// <para lang="en">Gets or sets Whether clearable. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示无数据空记录 默认 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show empty record when no data. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowEmpty { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 无数据时显示模板 默认 null</para>
    /// <para lang="en">Gets or sets Empty Template. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 多选模式下选中项最大宽度 默认 null 未设置使用样式内置默认值 6 个汉字</para>
    /// <para lang="en">Gets or sets the maximum width of selected item in multiple selection mode. Default null</para>
    /// </summary>
    [Parameter]
    public string? MultiSelectedItemMaxWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示工具栏 默认 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show toolbar. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表格 Toolbar 工具栏模板</para>
    /// <para lang="en">Gets or sets the table toolbar template, content appears center of toolbar</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ToolbarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表格 Toolbar 工具栏右侧按钮模板，模板中内容出现在默认按钮后面</para>
    /// <para lang="en">Gets or sets the table toolbar right-side button template, content appears after the default buttons</para>
    /// </summary>
    [Parameter]
    public RenderFragment? TableExtensionToolbarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 IIconTheme 服务实例</para>
    /// <para lang="en">Gets or sets IIconTheme Service Instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">获得表格列集合</para>
    /// <para lang="en">Get Table Column Collection</para>
    /// </summary>
    public List<ITableColumn> Columns { get; } = [];

    private string? ClassName => CssBuilder.Default("select select-table dropdown")
        .AddClass("disabled", IsDisabled)
        .AddClass("is-clearable", IsClearable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? InputClassName => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(FieldClass, IsNeedValidate)
        .AddClass(ValidCss)
        .Build();

    private string? MultiItemsClassString => CssBuilder.Default("multi-select-items")
        .AddClass(InputClassName)
        .Build();

    private string? MultiItemsStyleString => CssBuilder.Default()
        .AddClass($"--bb-select-table-item-width: {MultiSelectedItemMaxWidth};", !string.IsNullOrEmpty(MultiSelectedItemMaxWidth))
        .AddClass($"--bb-select-max-height: {MultiSelectedItemsMaxHeight};", !string.IsNullOrEmpty(MultiSelectedItemsMaxHeight))
        .Build();

    private string? MultiSelectedItemCountClassString => CssBuilder.Default("multi-select-item-group-count")
        .AddClass($"bg-{MultiSelectedItemsMaxDisplayCountColor.ToDescriptionString()}", MultiSelectedItemsMaxDisplayCountColor != Color.None)
        .Build();

    private string? AppendClassString => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private bool GetClearable() => IsClearable && !IsDisabled;

    /// <summary>
    /// <para lang="zh">获得/设置 右侧清除图标 默认 null</para>
    /// <para lang="en">Gets or sets Clear Icon. Default null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得 PlaceHolder 属性</para>
    /// <para lang="en">Get PlaceHolder Attribute</para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表格高度 默认 486px</para>
    /// <para lang="en">Gets or sets Table Height. Default 486px</para>
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 486;

    /// <summary>
    /// <para lang="zh">获得/设置 Value 显示模板 默认 null</para>
    /// <para lang="en">Gets or sets Value Display Template. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索框 默认为 false 不显示搜索框</para>
    /// <para lang="en">Gets or sets Whether to show search box. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 SearchTemplate 实例</para>
    /// <para lang="en">Gets or sets SearchTemplate Instance</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? SearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否收缩顶部搜索框 默认为 false 不收缩搜索框 是否显示搜索框请设置 <see cref="SearchMode"/> 值 Top</para>
    /// <para lang="en">Gets or sets Whether to collapse top search box. Default false. Please set <see cref="SearchMode"/> to Top if show search box</para>
    /// </summary>
    [Parameter]
    public bool CollapsedTopSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 SearchModel 实例</para>
    /// <para lang="en">Gets or sets SearchModel Instance</para>
    /// </summary>
    [Parameter]
    public TItem SearchModel { get; set; } = new TItem();

    /// <summary>
    /// <para lang="zh">获得/设置 自定义搜索模型 <see cref="CustomerSearchTemplate"/></para>
    /// <para lang="en">Gets or sets Custom Search Model <see cref="CustomerSearchTemplate"/></para>
    /// </summary>
    [Parameter]
    public ITableSearchModel? CustomerSearchModel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义搜索模型模板 <see cref="CustomerSearchModel"/></para>
    /// <para lang="en">Gets or sets Custom Search Model Template <see cref="CustomerSearchModel"/></para>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableSearchModel>? CustomerSearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否分页 默认为 false</para>
    /// <para lang="en">Gets or sets Whether pagination. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsPagination { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 每页显示数据数量的外部数据源</para>
    /// <para lang="en">Gets or sets PageItems Source</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<int>? PageItemsSource { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动生成列信息 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to auto generate columns. Default false</para>
    /// </summary>
    [Parameter]
    public bool AutoGenerateColumns { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清除文本内容 OnClear 回调方法 默认 null</para>
    /// <para lang="en">Gets or sets OnClear Callback Method. Default null</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TItem>>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? RetrieveId() => InputId;

    private string InputId => $"{Id}_input";

    private string GetStyleString => $"height: {Height}px;";

    private string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private Table<TItem> _table = default!;
    private string? _closeButtonIcon;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AddRequiredValidator();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (OnQueryAsync == null)
        {
            throw new InvalidOperationException("Please set OnQueryAsync value");
        }

        if (GetTextCallback == null)
        {
            throw new InvalidOperationException("Please set GetTextCallback value");
        }

        SelectedItems ??= [];

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectDropdownIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectClearIcon);
        _closeButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.MultiSelectCloseIcon);
    }

    private string? GetText(TItem item) => item == default ? null : GetTextCallback(item);

    private string GetIndexString(TItem item) => SelectedItems.IndexOf(item).ToString();

    private string GetCountText() => $"+ {SelectedItems.Count - MultiSelectedItemsMaxDisplayCount}";

    private async Task OnClickRowCallback(TItem item)
    {
        CurrentValue = item;
        await InvokeVoidAsync("close", Id);
    }

    private async Task OnClearValue()
    {
        SelectedItems.Clear();
        await TriggerUpdateSelectedItems();

        if (OnClearAsync != null)
        {
            await OnClearAsync();
        }

        await OnClickRowCallback(default!);
    }

    /// <summary>
    /// <para lang="zh">查询方法</para>
    /// <para lang="en">Query Method</para>
    /// </summary>
    public Task QueryAsync() => _table.QueryAsync();

    private IEnumerable<TItem> GetDisplayItems()
    {
        IEnumerable<TItem> items = SelectedItems;

        if (MultiSelectedItemsMaxDisplayCount > 0)
        {
            items = SelectedItems.Take(MultiSelectedItemsMaxDisplayCount);
        }
        return items;
    }

    /// <summary>
    /// <para lang="zh">触发删除选项方法 由 Javascript 调用</para>
    /// <para lang="en">Trigger remove item method, called by Javascript</para>
    /// </summary>
    /// <param name="index">
    /// <para lang="zh">要删除的选项索引</para>
    /// <para lang="en">The index of the item to remove</para>
    /// </param>
    [JSInvokable]
    public async Task TriggerRemoveItem(int index)
    {
        if (index >= 0 && index < SelectedItems.Count)
        {
            var item = SelectedItems[index];
            SelectedItems.Remove(item);

            await TriggerUpdateSelectedItems();
        }
    }

    /// <summary>
    /// <para lang="zh">更新 <see cref="SelectedItems"/> 参数方法 由 Javascript 调用</para>
    /// <para lang="en">Update <see cref="SelectedItems"/> parameter method, called by Javascript</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerUpdateSelectedItems()
    {
        if (IsMultipleSelect)
        {
            if (SelectedItemsChanged.HasDelegate)
            {
                await SelectedItemsChanged.InvokeAsync(SelectedItems);
            }
        }
    }
}

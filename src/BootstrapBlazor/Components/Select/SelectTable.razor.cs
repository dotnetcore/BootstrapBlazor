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
    /// <para lang="zh">获得/设置 TableHeader 实例</para>
    /// <para lang="en">Get/Set TableHeader Instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? TableColumns { get; set; }

    /// <summary>
    /// <para lang="zh">异步查询回调方法</para>
    /// <para lang="en">Async Query Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 颜色 默认 Color.None 无设置</para>
    /// <para lang="en">Get/Set Color. Default Color.None</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示组件右侧扩展箭头 默认 true 显示</para>
    /// <para lang="en">Get/Set Whether to show the component right extension arrow. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowAppendArrow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗表格最小宽度 默认为 null 未设置使用样式中的默认值</para>
    /// <para lang="en">Get/Set Dropdown Table Min Width. Default null (use style default)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? TableMinWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得 显示文字回调方法 默认 null</para>
    /// <para lang="en">Get Display Text Callback Method. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public Func<TItem, string?>? GetTextCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧下拉箭头图标 默认 fa-solid fa-angle-up</para>
    /// <para lang="en">Get/Set Dropdown Icon. Default fa-solid fa-angle-up</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可清除 默认 false</para>
    /// <para lang="en">Get/Set Whether clearable. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示无数据空记录 默认 false 不显示</para>
    /// <para lang="en">Get/Set Whether to show empty record when no data. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowEmpty { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 无数据时显示模板 默认 null</para>
    /// <para lang="en">Get/Set Empty Template. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 IIconTheme 服务实例</para>
    /// <para lang="en">Get/Set IIconTheme Service Instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">获得表格列集合</para>
    /// <para lang="en">Get Table Column Collection</para>
    /// </summary>
    public List<ITableColumn> Columns { get; } = [];

    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Class Name</para>
    /// </summary>
    private string? ClassName => CssBuilder.Default("select select-table dropdown")
        .AddClass("disabled", IsDisabled)
        .AddClass("is-clearable", IsClearable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Input Class Name</para>
    /// </summary>
    private string? InputClassName => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(FieldClass, IsNeedValidate)
        .AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 样式集合</para>
    /// <para lang="en">Get Append Class Name</para>
    /// </summary>
    private string? AppendClassString => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private bool GetClearable() => IsClearable && !IsDisabled;

    /// <summary>
    /// <para lang="zh">获得/设置 右侧清除图标 默认 fa-solid fa-angle-up</para>
    /// <para lang="en">Get/Set Clear Icon. Default fa-solid fa-angle-up</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得 PlaceHolder 属性</para>
    /// <para lang="en">Get PlaceHolder Attribute</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表格高度 默认 486px</para>
    /// <para lang="en">Get/Set Table Height. Default 486px</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 486;

    /// <summary>
    /// <para lang="zh">获得/设置 Value 显示模板 默认 null</para>
    /// <para lang="en">Get/Set Value Display Template. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索框 默认为 false 不显示搜索框</para>
    /// <para lang="en">Get/Set Whether to show search box. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 SearchTemplate 实例</para>
    /// <para lang="en">Get/Set SearchTemplate Instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? SearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否收缩顶部搜索框 默认为 false 不收缩搜索框 是否显示搜索框请设置 <see cref="SearchMode"/> 值 Top</para>
    /// <para lang="en">Get/Set Whether to collapse top search box. Default false. Please set <see cref="SearchMode"/> to Top if show search box</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool CollapsedTopSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 SearchModel 实例</para>
    /// <para lang="en">Get/Set SearchModel Instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public TItem SearchModel { get; set; } = new TItem();

    /// <summary>
    /// <para lang="zh">获得/设置 自定义搜索模型 <see cref="CustomerSearchTemplate"/></para>
    /// <para lang="en">Get/Set Custom Search Model <see cref="CustomerSearchTemplate"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public ITableSearchModel? CustomerSearchModel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义搜索模型模板 <see cref="CustomerSearchModel"/></para>
    /// <para lang="en">Get/Set Custom Search Model Template <see cref="CustomerSearchModel"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableSearchModel>? CustomerSearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否分页 默认为 false</para>
    /// <para lang="en">Get/Set Whether pagination. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsPagination { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 每页显示数据数量的外部数据源</para>
    /// <para lang="en">Get/Set PageItems Source</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<int>? PageItemsSource { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动生成列信息 默认为 false</para>
    /// <para lang="en">Get/Set Whether to auto generate columns. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoGenerateColumns { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清除文本内容 OnClear 回调方法 默认 null</para>
    /// <para lang="en">Get/Set OnClear Callback Method. Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TItem>>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得 input 组件 Id 方法</para>
    /// <para lang="en">Get input Component Id Method</para>
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// <para lang="zh">获得/设置 内部 Input 组件 Id</para>
    /// <para lang="en">Get/Set Internal Input Component Id</para>
    /// </summary>
    private string InputId => $"{Id}_input";

    private string GetStyleString => $"height: {Height}px;";

    private string? ClearClassString => CssBuilder.Default("clear-icon")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private Table<TItem> _table = default!;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AddRequiredValidator();
    }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet Method</para>
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

        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectDropdownIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.SelectClearIcon);
    }

    /// <summary>
    /// <para lang="zh">获得 Text 显示文字</para>
    /// <para lang="en">Get Display Text</para>
    /// </summary>
    /// <returns></returns>
    private string? GetText() => Value == default ? null : GetTextCallback(Value);

    private async Task OnClickRowCallback(TItem item)
    {
        CurrentValue = item;
        await InvokeVoidAsync("close", Id);
    }

    private async Task OnClearValue()
    {
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
    /// <returns></returns>
    public Task QueryAsync() => _table.QueryAsync();
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 下拉表格组件实现类
/// </summary>
/// <typeparam name="TItem"></typeparam>
[CascadingTypeParameter(nameof(TItem))]
public partial class SelectTable<TItem> : IColumnCollection where TItem : class, new()
{
    /// <summary>
    /// 获得/设置 TableHeader 实例
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? TableColumns { get; set; }

    /// <summary>
    /// 异步查询回调方法
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 是否显示组件右侧扩展箭头 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowAppendArrow { get; set; } = true;

    /// <summary>
    /// 获得/设置 弹窗表格最小宽度 默认为 null 未设置使用样式中的默认值
    /// </summary>
    [Parameter]
    public int? TableMinWidth { get; set; }

    /// <summary>
    /// 获得 显示文字回调方法 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public Func<TItem, string?>? GetTextCallback { get; set; }

    /// <summary>
    /// 获得/设置 右侧下拉箭头图标 默认 fa-solid fa-angle-up
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否可清除 默认 false
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// 获得/设置 是否显示无数据空记录 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowEmpty { get; set; }

    /// <summary>
    /// 获得/设置 无数据时显示模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? EmptyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 IIconTheme 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// 获得表格列集合
    /// </summary>
    public List<ITableColumn> Columns { get; } = [];

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("select select-table dropdown")
        .AddClass("disabled", IsDisabled)
        .AddClass("cls", IsClearable)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? InputClassName => CssBuilder.Default("form-select form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(FieldClass, IsNeedValidate)
        .AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? AppendClassString => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    private bool GetClearable() => IsClearable && !IsDisabled;

    /// <summary>
    /// 获得/设置 右侧清除图标 默认 fa-solid fa-angle-up
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// 获得 PlaceHolder 属性
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 表格高度 默认 486px
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 486;

    /// <summary>
    /// 获得/设置 Value 显示模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? Template { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索框 默认为 false 不显示搜索框
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// 获得/设置 SearchTemplate 实例
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? SearchTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否收缩顶部搜索框 默认为 false 不收缩搜索框 是否显示搜索框请设置 <see cref="SearchMode"/> 值 Top
    /// </summary>
    [Parameter]
    public bool CollapsedTopSearch { get; set; }

    /// <summary>
    /// 获得/设置 SearchModel 实例
    /// </summary>
    [Parameter]
    public TItem SearchModel { get; set; } = new TItem();

    /// <summary>
    /// 获得/设置 自定义搜索模型 <see cref="CustomerSearchTemplate"/>
    /// </summary>
    [Parameter]
    public ITableSearchModel? CustomerSearchModel { get; set; }

    /// <summary>
    /// 获得/设置 自定义搜索模型模板 <see cref="CustomerSearchModel"/>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableSearchModel>? CustomerSearchTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否分页 默认为 false
    /// </summary>
    [Parameter]
    public bool IsPagination { get; set; }

    /// <summary>
    /// 获得/设置 每页显示数据数量的外部数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<int>? PageItemsSource { get; set; }

    /// <summary>
    /// 获得/设置 是否自动生成列信息 默认为 false
    /// </summary>
    [Parameter]
    public bool AutoGenerateColumns { get; set; }

    /// <summary>
    /// 获得/设置 清除文本内容 OnClear 回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TItem>>? Localizer { get; set; }

    /// <summary>
    /// 获得 input 组件 Id 方法
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// 获得/设置 内部 Input 组件 Id
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
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AddRequiredValidator();
    }

    /// <summary>
    /// OnParametersSet 方法
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
    /// 获得 Text 显示文字
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
    /// 查询方法
    /// </summary>
    /// <returns></returns>
    public Task QueryAsync() => _table.QueryAsync();
}

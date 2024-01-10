// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件实现类
/// </summary>
/// <typeparam name="TItem"></typeparam>
[CascadingTypeParameter(nameof(TItem))]
public partial class SelectTable<TItem> : ITable where TItem : class, new()
{
    /// <summary>
    /// 获得/设置 TableHeader 实例
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? TableColumns { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<ITableColumn> Columns { get; } = [];

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, IFilterAction> Filters { get; } = [];

    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    public Func<Task>? OnFilterAsync { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ITableColumn> GetVisibleColumns() => Columns;

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("select select-table dropdown")
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? InputClassName => CssBuilder.Default("form-select")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"border-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"border-danger", IsValid.HasValue && !IsValid.Value)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? AppendClassName => CssBuilder.Default("form-select-append")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass($"text-success", IsValid.HasValue && IsValid.Value)
        .AddClass($"text-danger", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// 设置当前项是否 Active 方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
        .AddClass("active", () => item.Value == CurrentValueAsString)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    /// <summary>
    /// 获得/设置 搜索文本发生变化时回调此方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<string, IEnumerable<TItem>>? OnSearchTextChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否显示搜索框 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// 获得/设置 选中候选项后是否自动清空搜索框内容 默认 false 不清空
    /// </summary>
    [Parameter]
    public bool AutoClearSearchText { get; set; }

    /// <summary>
    /// 获得 PlaceHolder 属性
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感
    /// </summary>
    [Parameter]
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TItem>>? Localizer { get; set; }

    [NotNull]
    private List<TItem>? DataSource { get; set; }

    /// <summary>
    /// 获得 input 组件 Id 方法
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => InputId;

    /// <summary>
    /// 获得/设置 Select 内部 Input 组件 Id
    /// </summary>
    private string? InputId => $"{Id}_input";

    /// <summary>
    /// 获得/设置 搜索文字
    /// </summary>
    private string SearchText { get; set; } = "";

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        //if (OnSearchTextChanged == null)
        //{
        //    OnSearchTextChanged = text => Items.Where(i => GetText(i).Contains(text, StringComparison));
        //}
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<TItem>();
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
    }

    /// <summary>
    /// 获得 Text 属性名称
    /// </summary>
    [NotNull]
    protected Func<TItem, string>? TextInvoke { get; set; }

    /// <summary>
    /// 获得 Text 显示文字
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string GetText(TItem item) => item is null ? "" : TextInvoke(item);
}

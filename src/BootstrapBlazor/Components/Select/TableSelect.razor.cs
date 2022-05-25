// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件实现类
/// </summary>
/// <typeparam name="TValue"></typeparam>
public partial class TableSelect<TValue> where TValue : class, new()
{
    private ElementReference SelectElement { get; set; }

    private JSInterop<TableSelect<TValue>>? Interop { get; set; }

    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    /// <summary>
    /// 获得/设置 绑定数据集
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// 获得/设置 颜色 默认 Color.None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("select dropdown")
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
    public Func<string, IEnumerable<TValue>>? OnSearchTextChanged { get; set; }

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

    /// <summary>
    /// 获得/设置 Text 表达式
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public Expression<Func<TValue, string>>? TextExpresion { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Select<TValue>>? Localizer { get; set; }

    [NotNull]
    private List<TValue>? DataSource { get; set; }

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

        if (OnSearchTextChanged == null)
        {
            OnSearchTextChanged = text => Items.Where(i => GetText(i).Contains(text, StringComparison));
        }
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<TValue>();
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
        TextInvoke = TextExpresion.Compile();
    }

    /// <summary>
    /// 获得 Text 属性名称
    /// </summary>
    [NotNull]
    protected Func<TValue, string>? TextInvoke { get; set; }

    /// <summary>
    /// 获得 Text 显示文字
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string GetText(TValue item) => item is null ? "" : TextInvoke(item);
}

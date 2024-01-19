// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

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
    /// 获得/设置 是否显示组件右侧扩展箭头 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowAppendArrow { get; set; } = true;

    /// <summary>
    /// 获得 显示文字回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<TItem, string?>? GetTextCallback { get; set; }

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
    private string? InputClassName => CssBuilder.Default("form-select form-control")
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
    /// 获得 PlaceHolder 属性
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 表格高度
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 486;

    /// <summary>
    /// 获得/设置 Value 显示模板 默认 null
    /// </summary>
    /// <remarks>默认通过 <code></code></remarks>
    [Parameter]
    public RenderFragment<TItem>? Template { get; set; }

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

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
        PlaceHolder ??= Localizer[nameof(PlaceHolder)];
    }

    /// <summary>
    /// 获得 Text 显示文字
    /// </summary>
    /// <returns></returns>
    private string? GetText() => Value == default ? "" : GetTextCallback?.Invoke(Value) ?? Value?.ToString();

    private async Task OnClickRowCallback(TItem item)
    {
        CurrentValue = item;
        await InvokeVoidAsync("close", Id);
    }
}

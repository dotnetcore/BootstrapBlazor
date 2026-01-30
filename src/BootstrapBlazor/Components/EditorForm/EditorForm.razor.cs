// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">编辑表单基类</para>
/// <para lang="en">Editor Form Base Component</para>
/// </summary>
[CascadingTypeParameter(nameof(TModel))]
public partial class EditorForm<TModel> : IShowLabel, IDisposable
{
    private string? ClassString => CssBuilder.Default("bb-editor")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">支持每行多少个控件功能</para>
    /// <para lang="en">Support items per row function</para>
    /// </summary>
    /// <param name="item"></param>
    private string? GetCssString(IEditorItem item)
    {
        int cols = Math.Max(0, Math.Min(12, item.Cols));
        double mdCols = 6;
        if (ItemsPerRow.HasValue)
        {
            mdCols = Math.Max(0, Math.Min(12, Math.Ceiling(12d / ItemsPerRow.Value)));
        }
        return CssBuilder.Default("col-12")
            .AddClass($"col-sm-{cols}", cols > 0) // <para lang="zh">指定 Cols</para><para lang="en">Specify Cols</para>
            .AddClass($"col-sm-6 col-md-{mdCols}", mdCols > 0 && cols == 0 && item.Rows == 0 && !Utility.IsCheckboxList(item.PropertyType, item.ComponentType)) // <para lang="zh">指定 ItemsPerRow</para><para lang="en">Specify ItemsPerRow</para>
            .Build();
    }

    private string? FormClassString => CssBuilder.Default("row g-3")
        .AddClass("form-inline", RowType == RowType.Inline)
        .AddClass("form-inline-end", RowType == RowType.Inline && LabelAlign == Alignment.Right)
        .AddClass("form-inline-center", RowType == RowType.Inline && LabelAlign == Alignment.Center)
        .Build();

    private string? FormStyleString => CssBuilder.Default()
        .AddClass($"--bb-row-label-width: {LabelWidth}px;", LabelWidth.HasValue)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 每行显示组件数量 默认为 null</para>
    /// <para lang="en">Gets or sets Items Per Row. Default is null</para>
    /// </summary>
    [Parameter]
    public int? ItemsPerRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 实体类编辑模式 Add 还是 Update</para>
    /// <para lang="en">Gets or sets Item Changed Type. Add or Update</para>
    /// </summary>
    [Parameter]
    public ItemChangedType ItemChangedType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置行格式 默认 Row 布局</para>
    /// <para lang="en">Gets or sets Row Type. Default is Row</para>
    /// </summary>
    [Parameter]
    public RowType RowType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐</para>
    /// <para lang="en">Gets or sets Label Alignment in <see cref="RowType" /> Inline mode. Default is None, equivalent to Left</para>
    /// </summary>
    [Parameter]
    public Alignment LabelAlign { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标签宽度 默认 null 未设置使用全局设置 <code>--bb-row-label-width</code> 值</para>
    /// <para lang="en">Gets or sets Label Width. Default is null, use global setting <code>--bb-row-label-width</code> if not set</para>
    /// </summary>
    [Parameter]
    public int? LabelWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列模板 设置 <see cref="Items"/> 时本参数不生效</para>
    /// <para lang="en">Gets or sets Field Items Template. Not effective when <see cref="Items"/> is set</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? FieldItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮模板</para>
    /// <para lang="en">Gets or sets Buttons Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Buttons { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 绑定模型</para>
    /// <para lang="en">Gets or sets Model</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public TModel? Model { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示前置标签 默认为 null 未设置时默认显示标签</para>
    /// <para lang="en">Gets or sets Whether to Show Label. Default is null, show label if not set</para>
    /// </summary>
    [Parameter]
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null</para>
    /// <para lang="en">Gets or sets Whether to Show Label Tooltip. Default is null</para>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示为 Display 组件 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to Show as Display Component. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsDisplay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Display 组件的 Tooltip 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to Show Display Component Tooltip. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsShowDisplayTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否使用 SearchTemplate 默认 false 使用 EditTemplate 模板</para>
    /// <para lang="en">Gets or sets Whether to use SearchTemplate. Default is false, use EditTemplate</para>
    /// </summary>
    /// <remarks>多用于表格组件传递 <see cref="ITableColumn"/> 集合给参数 <see cref="Items"/> 时; Mostly used when passing <see cref="ITableColumn"/> collection to <see cref="Items"/> parameter in Table component</remarks>
    [CascadingParameter(Name = "IsSearch")]
    [NotNull]
    private bool? IsSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动生成模型的所有属性 默认为 true 生成所有属性</para>
    /// <para lang="en">Gets or sets Whether to Auto Generate All Items. Default is true</para>
    /// </summary>
    [Parameter]
    public bool AutoGenerateAllItem { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 忽略项目集合 默认 null 未设置</para>
    /// <para lang="en">Gets or sets the ignore items collection. Default is null</para>
    /// </summary>
    [Parameter]
    public List<string>? IgnoreItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 级联上下文绑定字段信息集合 设置此参数后 <see cref="FieldItems"/> 模板不生效</para>
    /// <para lang="en">Gets or sets Context Field Items Collection. <see cref="FieldItems"/> template will not be effective if set</para>
    /// </summary>
    [Parameter]
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义列排序规则 默认 null 未设置 使用内部排序机制 1 2 3 0 -3 -2 -1 顺序</para>
    /// <para lang="en">Gets or sets Custom Column Sort Rule. Default is null, use internal sort mechanism</para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<ITableColumn>, IEnumerable<ITableColumn>>? ColumnOrderCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 未设置 GroupName 编辑项是否放置在顶部 默认 false</para>
    /// <para lang="en">Gets or sets Whether to show unset GroupName items on top. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 默认占位符文本 默认 null</para>
    /// <para lang="en">Gets or sets Default Placeholder Text. Default is null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PlaceHolderText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当值变化时是否重新渲染组件 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Re-render Component when Value Changed. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsRenderWhenValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 级联上下文 EditContext 实例 内置于 EditForm 或者 ValidateForm 时有值</para>
    /// <para lang="en">Gets or sets Cascading EditContext Instance. Available when inside EditForm or ValidateForm</para>
    /// </summary>
    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    /// <summary>
    /// <para lang="zh">获得 ValidateForm 实例</para>
    /// <para lang="en">Get ValidateForm Instance</para>
    /// </summary>
    [CascadingParameter]
    private ValidateForm? ValidateForm { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<EditorForm<TModel>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ILookupService? LookupService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 配置编辑项目集合</para>
    /// <para lang="en">Gets or sets Editor Items Cache</para>
    /// </summary>
    private readonly List<IEditorItem> _editorItems = [];

    private IEnumerable<IEditorItem> UnsetGroupItems => RenderItems.Where(i => string.IsNullOrEmpty(i.GroupName) && i.IsVisible(ItemChangedType, IsSearch.Value));

    private IEnumerable<KeyValuePair<string, IOrderedEnumerable<IEditorItem>>> GroupItems => RenderItems
        .Where(i => !string.IsNullOrEmpty(i.GroupName) && i.IsVisible(ItemChangedType, IsSearch.Value))
        .GroupBy(i => i.GroupName).OrderBy(i => i.Key)
        .Select(i => new KeyValuePair<string, IOrderedEnumerable<IEditorItem>>(i.First().GroupName!, i.OrderBy(x => x.Order)));

    private List<IEditorItem>? _itemsCache;

    private List<IEditorItem> RenderItems
    {
        get
        {
            _itemsCache ??= GetRenderItems();
            return _itemsCache;
        }
    }

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized Method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (CascadedEditContext != null)
        {
            CascadedEditContext.OnFieldChanged += NotifyValueChanged;
        }

        if (CascadedEditContext != null && IsSearch is not true)
        {
            var message = Localizer["ModelInvalidOperationExceptionMessage", nameof(EditorForm<TModel>)];
            if (!CascadedEditContext.Model.GetType().IsAssignableTo(typeof(TModel)))
            {
                throw new InvalidCastException(message);
            }

            Model = (TModel)CascadedEditContext.Model;
        }

        // 统一设置所有 IEditorItem 的 PlaceHolder
        // Set PlaceHolder for all IEditorItem
        PlaceHolderText ??= Localizer[nameof(PlaceHolderText)];
        IsSearch ??= false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 为空时使用级联参数 ValidateForm 的 ShowLabel
        // Use cascading parameter ValidateForm's ShowLabel when null
        ShowLabel ??= ValidateForm?.ShowLabel;
        _itemsCache = null;
    }

    private List<IEditorItem> GetRenderItems()
    {
        var items = new List<IEditorItem>();
        if (Items != null)
        {
            items.AddRange(Items.Where(i => !i.GetIgnore() && !string.IsNullOrEmpty(i.GetFieldName())));
            return items;
        }

        // 如果 EditorItems 有值表示 用户自定义列
        // If EditorItems has value, it means user defined columns
        var columns = AutoGenerateAllItem
            ? AutoGenerateColumns()
            : _editorItems.Where(i => !i.GetIgnore()
                && !string.IsNullOrEmpty(i.GetFieldName())
                && i.IsVisible(ItemChangedType, IsSearch.Value));
        items.AddRange(columns);
        return items;
    }

    private List<ITableColumn> AutoGenerateColumns()
    {
        // 获取绑定模型所有属性
        // Get all properties of binding model
        var columns = Utility.GetTableColumns<TModel>(defaultOrderCallback: ColumnOrderCallback).ToList();

        // 通过设定的 FieldItems 模板获取项进行渲染
        // Render items by setting FieldItems template
        foreach (var el in _editorItems)
        {
            var item = columns.FirstOrDefault(i => i.GetFieldName() == el.GetFieldName());
            if (item != null)
            {
                // 过滤掉不编辑与不可见的列
                // Filter out non-editable and invisible columns
                if (el.GetIgnore() || !el.IsVisible(ItemChangedType, IsSearch.Value) || string.IsNullOrEmpty(el.GetFieldName()))
                {
                    columns.Remove(item);
                }
                else
                {
                    // 设置只读属性与列模板
                    // Set Readonly property and column template
                    item.CopyValue(el);
                }
            }
        }

        return columns;
    }

    private RenderFragment AutoGenerateTemplate(IEditorItem item) => builder =>
    {
        if (IsDisplay || !item.CanWrite(typeof(TModel), ItemChangedType, IsSearch.Value))
        {
            builder.CreateDisplayByFieldType(item, Model, IsShowDisplayTooltip);
        }
        else
        {
            item.PlaceHolder ??= PlaceHolderText;
            builder.CreateComponentByFieldType(this, item, Model, ItemChangedType, IsSearch.Value, item.GetLookupService(LookupService));
        }
    };

    private RenderFragment<object>? GetRenderTemplate(IEditorItem item) => IsSearch.Value && item is ITableColumn col
        ? col.SearchTemplate
        : item.EditTemplate;

    private void NotifyValueChanged(object? sender, FieldChangedEventArgs args)
    {
        // perf: 判断是否在编辑状态避免不必要的重绘
        // perf: Check if in edit state to avoid unnecessary redraw
        if (IsRenderWhenValueChanged)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        if (CascadedEditContext != null)
        {
            CascadedEditContext.OnFieldChanged -= NotifyValueChanged;
        }
        GC.SuppressFinalize(this);
    }
}

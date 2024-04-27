// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 编辑表单基类
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TModel))]
#endif
public partial class EditorForm<TModel> : IShowLabel
{
    private string? ClassString => CssBuilder.Default("bb-editor form-body")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 支持每行多少个控件功能
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? GetCssString(IEditorItem item)
    {
        int cols = 0;
        double mdCols = 6;
        if (item is AutoGenerateColumnAttribute a && a.Cols > 0 && a.Cols < 13)
        {
            cols = a.Cols;
        }
        if (ItemsPerRow.HasValue)
        {
            mdCols = Math.Min(12, Math.Ceiling(12d / ItemsPerRow.Value));
        }
        return CssBuilder.Default("col-12")
            .AddClass($"col-sm-{cols}", cols > 0) // 指定 Cols
            .AddClass($"col-sm-6 col-md-{mdCols}", cols == 0 && item.Items == null && item.Rows == 0) // 指定 ItemsPerRow
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
    /// 获得/设置 每行显示组件数量 默认为 null
    /// </summary>
    [Parameter]
    public int? ItemsPerRow { get; set; }

    /// <summary>
    /// 获得/设置 实体类编辑模式 Add 还是 Update
    /// </summary>
    [Parameter]
    public ItemChangedType ItemChangedType { get; set; }

    /// <summary>
    /// 获得/设置 设置行格式 默认 Row 布局
    /// </summary>
    [Parameter]
    public RowType RowType { get; set; }

    /// <summary>
    /// 获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐
    /// </summary>
    [Parameter]
    public Alignment LabelAlign { get; set; }

    /// <summary>
    /// 获得/设置 标签宽度 默认 null 未设置使用全局设置 <code>--bb-row-label-width</code> 值
    /// </summary>
    [Parameter]
    public int? LabelWidth { get; set; }

    /// <summary>
    /// 获得/设置 列模板 设置 <see cref="Items"/> 时本参数不生效
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? FieldItems { get; set; }

    /// <summary>
    /// 获得/设置 按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? Buttons { get; set; }

    /// <summary>
    /// 获得/设置 绑定模型
    /// </summary>
    [Parameter]
    [NotNull]
    public TModel? Model { get; set; }

    /// <summary>
    /// 获得/设置 是否显示前置标签 默认为 null 未设置时默认显示标签
    /// </summary>
    [Parameter]
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 是否显示为 Display 组件 默认为 false
    /// </summary>
    [Parameter]
    public bool IsDisplay { get; set; }

    /// <summary>
    /// 获得/设置 是否使用 SearchTemplate 默认 false 使用 EditTemplate 模板
    /// </summary>
    /// <remarks>多用于表格组件传递 <see cref="ITableColumn"/> 集合给参数 <see cref="Items"/> 时</remarks>
    [CascadingParameter(Name = "IsSearch")]
    [NotNull]
    private bool? IsSearch { get; set; }

    /// <summary>
    /// 获得/设置 是否自动生成模型的所有属性 默认为 true 生成所有属性
    /// </summary>
    [Parameter]
    public bool AutoGenerateAllItem { get; set; } = true;

    /// <summary>
    /// 获得/设置 级联上下文绑定字段信息集合 设置此参数后 <see cref="FieldItems"/> 模板不生效
    /// </summary>
    [Parameter]
    public IEnumerable<IEditorItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 自定义列排序规则 默认 null 未设置 使用内部排序机制 1 2 3 0 -3 -2 -1 顺序
    /// </summary>
    [Parameter]
    public Func<IEnumerable<ITableColumn>, IEnumerable<ITableColumn>>? ColumnOrderCallback { get; set; }

    /// <summary>
    /// 获得/设置 未设置 GroupName 编辑项是否放置在顶部 默认 false
    /// </summary>
    [Parameter]
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    /// 获得/设置 默认占位符文本 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PlaceHolderText { get; set; }

    /// <summary>
    /// 获得/设置 级联上下文 EditContext 实例 内置于 EditForm 或者 ValidateForm 时有值
    /// </summary>
    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    /// <summary>
    /// 获得 ValidateForm 实例
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
    /// 获得/设置 配置编辑项目集合
    /// </summary>
    private readonly List<IEditorItem> _editorItems = [];

    /// <summary>
    /// 获得/设置 渲染的编辑项集合
    /// </summary>
    [NotNull]
    private List<IEditorItem>? _formItems = null;

    private IEnumerable<IEditorItem> UnsetGroupItems => _formItems.Where(i => string.IsNullOrEmpty(i.GroupName) && i.IsVisible(ItemChangedType, IsSearch.Value));

    private IEnumerable<KeyValuePair<string, IOrderedEnumerable<IEditorItem>>> GroupItems => _formItems
        .Where(i => !string.IsNullOrEmpty(i.GroupName) && i.IsVisible(ItemChangedType, IsSearch.Value))
        .GroupBy(i => i.GroupOrder).OrderBy(i => i.Key)
        .Select(i => new KeyValuePair<string, IOrderedEnumerable<IEditorItem>>(i.First().GroupName!, i.OrderBy(x => x.Order)));

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

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
        ShowLabel ??= ValidateForm?.ShowLabel;
        _formItems = null;
    }

    private bool _inited;

    private Task OnRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _inited = true;
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private void ResetItems()
    {
        if (_formItems == null)
        {
            _formItems = [];
            if (Items != null)
            {
                _formItems.AddRange(Items.Where(i => !i.Ignore));
            }
            else
            {
                // 如果 EditorItems 有值表示 用户自定义列
                if (AutoGenerateAllItem)
                {
                    // 获取绑定模型所有属性
                    var items = Utility.GetTableColumns<TModel>(defaultOrderCallback: ColumnOrderCallback).ToList();

                    // 通过设定的 FieldItems 模板获取项进行渲染
                    foreach (var el in _editorItems)
                    {
                        var item = items.FirstOrDefault(i => i.GetFieldName() == el.GetFieldName());
                        if (item != null)
                        {
                            // 过滤掉不编辑与不可见的列
                            if (el.Ignore || !el.IsVisible(ItemChangedType, IsSearch.Value))
                            {
                                items.Remove(item);
                            }
                            else
                            {
                                // 设置只读属性与列模板
                                item.CopyValue(el);
                            }
                        }
                    }
                    _formItems.AddRange(items);
                }
                else
                {
                    _formItems.AddRange(_editorItems.Where(i => !i.Ignore && i.IsVisible(ItemChangedType, IsSearch.Value)));
                }
            }
        }
    }

    private RenderFragment AutoGenerateTemplate(IEditorItem item) => builder =>
    {
        if (IsDisplay || !item.CanWrite(typeof(TModel), ItemChangedType, IsSearch.Value))
        {
            builder.CreateDisplayByFieldType(item, Model);
        }
        else
        {
            item.PlaceHolder ??= PlaceHolderText;
            builder.CreateComponentByFieldType(this, item, Model, ItemChangedType, IsSearch.Value, LookupService);
        }
    };

    private RenderFragment<object>? GetRenderTemplate(IEditorItem item) => IsSearch.Value && item is ITableColumn col
        ? col.SearchTemplate
        : item.EditTemplate;
}

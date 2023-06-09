// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;
#if NET5_0
/// <summary>
/// 表头组件
/// </summary>
/// <typeparam name="TType">绑定字段值类型</typeparam>
public class TableColumn<TType> : BootstrapComponentBase, ITableColumn
#elif NET6_0_OR_GREATER
/// <summary>
/// 表头组件
/// </summary>
/// <typeparam name="TItem">模型泛型</typeparam>
/// <typeparam name="TType">绑定字段值类型</typeparam>
public class TableColumn<TItem, TType> : BootstrapComponentBase, ITableColumn
#endif
{
    /// <summary>
    /// 获得/设置 相关过滤器
    /// </summary>
    public IFilter? Filter { get; set; }

    /// <summary>
    /// 获得/设置 组件类型 默认为 null
    /// </summary>
    [Parameter]
    public Type? ComponentType { get; set; }

    /// <summary>
    /// 获得/设置 组件自定义类型参数集合 默认为 null
    /// </summary>
    [Parameter]
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// 获得/设置 绑定列类型
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; set; }

    /// <summary>
    /// 获得/设置 数据绑定字段值
    /// </summary>
    [Parameter]
    [MaybeNull]
    public TType Field { get; set; }

    /// <summary>
    /// 获得/设置 ValueExpression 表达式
    /// </summary>
    [Parameter]
    public Expression<Func<TType>>? FieldExpression { get; set; }

    /// <summary>
    /// 获得/设置 是否排序 默认 false
    /// </summary>
    [Parameter]
    public bool Sortable { get; set; }

    /// <summary>
    /// 获得/设置 是否为默认排序列 默认为 false
    /// </summary>
    [Parameter]
    public bool DefaultSort { get; set; }

    /// <summary>
    /// 获得/设置 本列是否允许换行 默认为 false
    /// </summary>
    [Parameter]
    public bool TextWrap { get; set; }

    /// <summary>
    /// 获得/设置 本列文本超出省略 默认为 false
    /// </summary>
    [Parameter]
    public bool TextEllipsis { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 是否表头允许折行 默认 false 不折行
    /// </summary>
    [Parameter]
    public bool HeaderTextWrap { get; set; }

    /// <summary>
    /// 获得/设置 是否表头显示 Tooltip 默认 false 不显示 可配合 <see cref="HeaderTextEllipsis"/> 使用 设置 <see cref="HeaderTextWrap"/> 为 true 时本参数不生效
    /// </summary>
    [Parameter]
    public bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// 获得/设置 是否表头 Tooltip 内容
    /// </summary>
    [Parameter]
    public string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// 获得/设置 是否表头溢出时截断 默认 false 不截断 可配合 <see cref="HeaderTextTooltip"/> 使用 设置 <see cref="HeaderTextWrap"/> 为 true 时本参数不生效
    /// </summary>
    [Parameter]
    public bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// 获得/设置 步长 默认为 null
    /// </summary>
    [Parameter]
    public object? Step { get; set; }

    /// <summary>
    /// 获得/设置 Textarea 行数 默认为 0
    /// </summary>
    [Parameter]
    public int Rows { get; set; }

    /// <summary>
    /// 获得/设置 是否为默认排序规则 默认为 SortOrder.Unset
    /// </summary>
    [Parameter]
    public SortOrder DefaultSortOrder { get; set; }

    /// <summary>
    /// 获得/设置 是否可过滤数据 默认 false
    /// </summary>
    [Parameter]
    public bool Filterable { get; set; }

    /// <summary>
    /// 获得/设置 是否参与搜索自动生成 默认 false
    /// </summary>
    [Parameter]
    public bool Searchable { get; set; }

    /// <summary>
    /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
    /// </summary>
    [Parameter]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// 获得/设置 当前列编辑时是否为只读模式 默认为 false
    /// </summary>
    /// <remarks>此属性覆盖 <see cref="IsReadonlyWhenAdd"/> 与 <see cref="IsReadonlyWhenEdit"/> 即新建与编辑时均只读</remarks>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// 获得/设置 新建时此列只读 默认为 false
    /// </summary>
    [Parameter]
    public bool IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// 获得/设置 编辑时此列只读 默认为 false
    /// </summary>
    [Parameter]
    public bool IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    [Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// 获得/设置 表头显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 placeholder 文本 默认为 null
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 列宽 默认为 auto
    /// </summary>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// 获得/设置 是否固定本列 默认 false 不固定
    /// </summary>
    [Parameter]
    public bool Fixed { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool ShowCopyColumn { get; set; }

    /// <summary>
    /// 获得/设置 是否显示本列 默认 true 显示
    /// </summary>
    [Parameter]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// 获得/设置 字段鼠标悬停提示
    /// </summary>
    [Parameter]
    public bool ShowTips { get; set; }

    /// <summary>
    /// 获得/设置 列 td 自定义样式 默认为 null 未设置
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// 获得/设置 文字对齐方式 默认为 Alignment.None
    /// </summary>
    [Parameter]
    public Alignment Align { get; set; }

    /// <summary>
    /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// 获得/设置 列格式化回调委托
    /// </summary>
    [Parameter]
    public Func<object?, Task<string>>? Formatter { get; set; }

    /// <summary>
    /// 获得/设置 显示模板
    /// </summary>
    [Parameter]
#if NET5_0
    public RenderFragment<TableColumnContext<object, TType>>? Template { get; set; }

    /// <summary>
    /// 内部使用负责把 object 类型的绑定数据值转化为泛型数据传递给前端
    /// </summary>
    RenderFragment<object>? ITableColumn.Template
    {
        get => Template == null ? null : new RenderFragment<object>(context => builder =>
        {
            // 此处 context 为行数据
            var fieldName = GetFieldName();
            var value = Utility.GetPropertyValue<object, TType>(context, fieldName);
            builder.AddContent(0, Template.Invoke(new TableColumnContext<object, TType>(context, value)));
        });
        set
        {

        }
    }
#elif NET6_0_OR_GREATER
    public RenderFragment<TableColumnContext<TItem, TType>>? Template { get; set; }

    /// <summary>
    /// 内部使用负责把 object 类型的绑定数据值转化为泛型数据传递给前端
    /// </summary>
    RenderFragment<object>? ITableColumn.Template
    {
        get => Template == null ? null : new RenderFragment<object>(context => builder =>
        {
            // 此处 context 为行数据
            var fieldName = GetFieldName();
            var value = Utility.GetPropertyValue<object, TType>(context, fieldName);
            builder.AddContent(0, Template.Invoke(new TableColumnContext<TItem, TType>((TItem)context, value)));
        });
        set
        {

        }
    }
#endif

    /// <summary>
    /// 获得/设置 编辑模板
    /// </summary>
    [Parameter]
#if NET5_0
    public RenderFragment<object>? EditTemplate { get; set; }
#elif NET6_0_OR_GREATER
    public RenderFragment<TItem>? EditTemplate { get; set; }

    RenderFragment<object>? IEditorItem.EditTemplate
    {
        get
        {
            return EditTemplate == null ? null : new RenderFragment<object>(item => builder =>
            {
                builder.AddContent(0, EditTemplate((TItem)item));
            });
        }
        set
        {
        }
    }
#endif

    /// <summary>
    /// 获得/设置 搜索模板
    /// </summary>
    /// <value></value>
    [Parameter]
#if NET5_0
    public RenderFragment<object>? SearchTemplate { get; set; }
#elif NET6_0_OR_GREATER
    public RenderFragment<TItem>? SearchTemplate { get; set; }

    RenderFragment<object>? ITableColumn.SearchTemplate
    {
        get
        {
            return SearchTemplate == null ? null : new RenderFragment<object>(item => builder =>
            {
                builder.AddContent(0, SearchTemplate((TItem)item));
            });
        }
        set
        {
        }
    }
#endif

    /// <summary>
    /// 获得/设置 过滤模板
    /// </summary>
    [Parameter]
    public RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 表头模板
    /// </summary>
    [Parameter]
    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 显示节点阈值 默认值 BreakPoint.None 未设置
    /// </summary>
    [Parameter]
    public BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// 获得/设置 额外数据源一般用于下拉框或者 CheckboxList 这种需要额外配置数据源组件使用
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 显示顺序
    /// </summary>
    [Parameter]
    public int Order { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源 常用于外键自动转换为名称操作
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 字段数据源下拉框是否显示搜索栏 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// 获得/设置 字典数据源服务的类别 常用于外键自动转换为名称操作
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// 获得/设置 单元格回调方法
    /// </summary>
    [Parameter]
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// 获得/设置 自定义验证集合
    /// </summary>
    [Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// 获得/设置 Table 实例
    /// </summary>
    [CascadingParameter]
    protected ITable? Table { get; set; }

    /// <summary>
    /// 组件初始化方法
    /// </summary>
    protected override void OnInitialized()
    {
        Table?.Columns.Add(this);
        if (FieldExpression != null)
        {
            _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
        }

        // 获取模型属性定义类型
        PropertyType = typeof(TType);
    }

    private FieldIdentifier? _fieldIdentifier;
    /// <summary>
    /// 获取绑定字段显示名称方法
    /// </summary>
    public virtual string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? FieldName ?? "";

    /// <summary>
    /// 获得/设置 绑定类字段名称
    /// </summary>
    [Parameter]
    public string? FieldName { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组 默认 null
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组排序 默认 0
    /// </summary>
    [Parameter]
    public int GroupOrder { get; set; }

    /// <summary>
    /// 获取绑定字段信息方法
    /// </summary>
    public string GetFieldName()
    {
        if (string.IsNullOrEmpty(FieldName))
        {
            var fields = new List<string>();
            Expression? express = FieldExpression;

            while (express is LambdaExpression lambda)
            {
                express = lambda.Body;
            }

            while (express is MemberExpression member)
            {
                if (member.Expression is MemberExpression)
                {
                    fields.Add(member.Member.Name);
                }
                express = member.Expression;
            }

            if (fields.Any())
            {
                fields.Reverse();
                FieldName = string.Join(".", fields);
            }
            else
            {
                FieldName = _fieldIdentifier?.FieldName;
            }
        }
        return FieldName ?? "";
    }
}

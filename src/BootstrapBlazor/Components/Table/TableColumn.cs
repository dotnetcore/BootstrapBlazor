// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">表头组件</para>
/// <para lang="en">表头component</para>
/// </summary>
/// <typeparam name="TItem"><para lang="zh">模型泛型</para><para lang="en">model generic</para></typeparam>
/// <typeparam name="TType"><para lang="zh">绑定字段值类型</para><para lang="en">bind value type</para></typeparam>
public class TableColumn<TItem, TType> : BootstrapComponentBase, ITableColumn
{
    /// <summary>
    /// <para lang="zh">获得/设置 相关过滤器</para>
    /// <para lang="en">Gets or sets 相关过滤器</para>
    /// </summary>
    public IFilter? Filter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件类型 默认为 null</para>
    /// <para lang="en">Gets or sets component type Default is为 null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Type? ComponentType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件自定义类型参数集合 默认为 null</para>
    /// <para lang="en">Gets or sets component自定义type参数collection Default is为 null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 绑定列类型</para>
    /// <para lang="en">Gets or sets 绑定列type</para>
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据绑定字段值</para>
    /// <para lang="en">Gets or sets data绑定字段值</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [MaybeNull]
    public TType Field { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ValueExpression 表达式</para>
    /// <para lang="en">Gets or sets ValueExpression 表达式</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Expression<Func<TType>>? FieldExpression { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IgnoreWhenExport { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? Sortable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为默认排序列 默认为 false</para>
    /// <para lang="en">Gets or sets whether为Default is排序列 Default is为 false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool DefaultSort { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? TextWrap { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? TextEllipsis { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null</para>
    /// <para lang="en">Gets or sets whether  label tooltip Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否表头允许折行 默认 false 不折行</para>
    /// <para lang="en">Gets or sets whether表头允许折行 Default is false 不折行</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool HeaderTextWrap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否表头显示 Tooltip 默认 false 不显示 可配合 <see cref="HeaderTextEllipsis"/> 使用 设置 <see cref="HeaderTextWrap"/> 为 true 时本参数不生效</para>
    /// <para lang="en">Gets or sets whether表头display Tooltip Default is false 不display 可配合 <see cref="HeaderTextEllipsis"/> 使用 Sets <see cref="HeaderTextWrap"/> 为 true 时本参数不生效</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否表头 Tooltip 内容</para>
    /// <para lang="en">Gets or sets whether表头 Tooltip content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否表头溢出时截断 默认 false 不截断 可配合 <see cref="HeaderTextTooltip"/> 使用 设置 <see cref="HeaderTextWrap"/> 为 true 时本参数不生效</para>
    /// <para lang="en">Gets or sets whether表头溢出时截断 Default is false 不截断 可配合 <see cref="HeaderTextTooltip"/> 使用 Sets <see cref="HeaderTextWrap"/> 为 true 时本参数不生效</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Step { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Rows { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Cols { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public SortOrder DefaultSortOrder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? Filterable { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? Searchable { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? Ignore { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，是否可编辑改用 Readonly 参数，是否可见改用 Ignore 参数; Deprecated If it is editable, use the Readonly parameter. If it is visible, use the Ignore parameter.")]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? Readonly { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? Visible { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IsVisibleWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IsVisibleWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? Required { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IsRequiredWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否不进行验证 默认为 false</para>
    /// <para lang="en">Gets or sets whether不进行验证 Default is为 false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表头显示文字</para>
    /// <para lang="en">Gets or sets 表头display文字</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 placeholder 文本 默认为 null</para>
    /// <para lang="en">Gets or sets placeholder 文本 Default is为 null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列宽 默认为 auto</para>
    /// <para lang="en">Gets or sets 列宽 Default is为 auto</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否固定本列 默认 false 不固定</para>
    /// <para lang="en">Gets or sets whether固定本列 Default is false 不固定</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Fixed { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? ShowCopyColumn { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 字段鼠标悬停提示</para>
    /// <para lang="en">Gets or sets 字段鼠标悬停提示</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? ShowTips { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列 td 自定义样式 默认为 null 未设置</para>
    /// <para lang="en">Gets or sets 列 td 自定义style Default is为 null 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Alignment? Align { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd</para>
    /// <para lang="en">Gets or sets 格式化字符串 如时间typeSets yyyy-MM-dd</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TableColumnContext<TItem, TType?>>? Template { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    RenderFragment<object>? ITableColumn.Template
    {
        get => Template == null ? null : new RenderFragment<object>(context => builder =>
        {
            // 此处 context 为行数据
            if (this is TableTemplateColumn<TItem>)
            {
                builder.AddContent(0, Template.Invoke(new TableColumnContext<TItem, TType?>((TItem)context, default)));
            }
            else
            {
                var fieldName = GetFieldName();
                var value = Utility.GetPropertyValue<object, TType?>(context, fieldName);
                builder.AddContent(0, Template.Invoke(new TableColumnContext<TItem, TType?>((TItem)context, value)));
            }
        });
        set
        {

        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
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

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
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

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义验证集合</para>
    /// <para lang="en">Gets or sets 自定义验证collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? FieldName { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int GroupOrder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [CascadingParameter]
    protected IColumnCollection? Columns { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Columns?.Columns.Add(this);

        if (FieldExpression != null)
        {
            _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
        }

        // 获取模型属性定义类型
        PropertyType = typeof(TType);
    }

    private FieldIdentifier? _fieldIdentifier;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? FieldName ?? "";

    /// <summary>
    /// <inheritdoc/>
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

            if (fields.Count != 0)
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

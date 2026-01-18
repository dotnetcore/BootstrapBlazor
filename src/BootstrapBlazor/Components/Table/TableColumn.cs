// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">表格 Column 列组件</para>
/// <para lang="en">Table column header Component</para>
/// </summary>
/// <typeparam name="TItem"><para lang="zh">模型泛型</para><para lang="en">model generic type</para></typeparam>
/// <typeparam name="TType"><para lang="zh">绑定字段值类型</para><para lang="en">binding field value type</para></typeparam>
public class TableColumn<TItem, TType> : BootstrapComponentBase, ITableColumn
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IFilter? Filter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Type? ComponentType { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据绑定字段值</para>
    /// <para lang="en">Gets or sets data binding field value</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [MaybeNull]
    public TType Field { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ValueExpression 表达式</para>
    /// <para lang="en">Gets or sets ValueExpression expression</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Expression<Func<TType>>? FieldExpression { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? IgnoreWhenExport { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Sortable { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool DefaultSort { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? TextWrap { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? TextEllipsis { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool HeaderTextWrap { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Step { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public int Rows { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public int Cols { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public SortOrder DefaultSortOrder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Filterable { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Searchable { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Ignore { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，是否可编辑改用 Readonly 参数，是否可见改用 Ignore 参数; Deprecated If it is editable, use the Readonly parameter. If it is visible, use the Ignore parameter.")]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Readonly { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Visible { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? IsVisibleWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? IsVisibleWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Required { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? IsRequiredWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool Fixed { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? ShowCopyColumn { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? ShowTips { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Alignment? Align { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <inheritdoc/>
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
    /// </summary>
    [Parameter]
    public RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>    /// </summary>
    [Parameter]
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? FieldName { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// <inheritdoc/>
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

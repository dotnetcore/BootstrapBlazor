// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">EditorItem 组件</para>
/// <para lang="en">EditorItem component</para>
/// </summary>
public class EditorItem<TModel, TValue> : ComponentBase, IEditorItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 绑定字段值</para>
    /// <para lang="en">Gets or sets the bound field value</para>
    /// </summary>
    [Parameter]
    public TValue? Field { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 绑定字段值变化回调委托</para>
    /// <para lang="en">Gets or sets the bound field value changed callback</para>
    /// </summary>
    [Parameter]
    public EventCallback<TValue> FieldChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 属性类型</para>
    /// <para lang="en">Gets or sets the property type</para>
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ValueExpression 表达式</para>
    /// <para lang="en">Gets or sets the value expression</para>
    /// </summary>
    [Parameter]
    public Expression<Func<TValue>>? FieldExpression { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Editable"/>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，是否可编辑改用 Readonly 参数，是否可见改用 Ignore 参数; Deprecated If it is editable, use the Readonly parameter. If it is visible, use the Ignore parameter.")]
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否忽略显示</para>
    /// <para lang="en">Gets or sets whether the field is ignored</para>
    /// </summary>
    [Parameter]
    public bool? Ignore { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Readonly { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否必填</para>
    /// <para lang="en">Gets or sets whether the field is required</para>
    /// </summary>
    [Parameter]
    public bool? Required { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 必填错误信息</para>
    /// <para lang="en">Gets or sets the required error message</para>
    /// </summary>
    [Parameter]
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否跳过校验</para>
    /// <para lang="en">Gets or sets whether to skip validation</para>
    /// </summary>
    [Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签提示</para>
    /// <para lang="en">Gets or sets whether to show the label tooltip</para>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示文本</para>
    /// <para lang="en">Gets or sets the display text</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 步长</para>
    /// <para lang="en">Gets or sets the step value</para>
    /// </summary>
    [Parameter]
    public string? Step { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 行数</para>
    /// <para lang="en">Gets or sets the row count</para>
    /// </summary>
    [Parameter]
    public int Rows { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列数</para>
    /// <para lang="en">Gets or sets the column count</para>
    /// </summary>
    [Parameter]
    public int Cols { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑模板</para>
    /// <para lang="en">Gets or sets the edit template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? EditTemplate { get; set; }

    RenderFragment<object>? IEditorItem.EditTemplate
    {
        get
        {
            return EditTemplate == null ? null : new RenderFragment<object>(item => builder =>
            {
                builder.AddContent(0, EditTemplate((TModel)item));
            });
        }
        set
        {
        }
    }

    /// <summary>
    /// <para lang="zh">获得/设置 组件类型</para>
    /// <para lang="en">Gets or sets the component type</para>
    /// </summary>
    [Parameter]
    public Type? ComponentType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件参数集合</para>
    /// <para lang="en">Gets or sets the component parameters</para>
    /// </summary>
    [Parameter]
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 占位符</para>
    /// <para lang="en">Gets or sets the placeholder text</para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示顺序</para>
    /// <para lang="en">Gets or sets the display order</para>
    /// </summary>
    [Parameter]
    public int Order { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 绑定数据集合</para>
    /// <para lang="en">Gets or sets the bound data items</para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Lookup 数据集合</para>
    /// <para lang="en">Gets or sets the lookup data items</para>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择时是否显示搜索</para>
    /// <para lang="en">Gets or sets whether to show search when selecting</para>
    /// </summary>
    [Parameter]
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择时是否固定搜索</para>
    /// <para lang="en">Gets or sets whether the search is fixed when selecting</para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示为气泡</para>
    /// <para lang="en">Gets or sets whether to show as a popover</para>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Lookup 比较方式</para>
    /// <para lang="en">Gets or sets the lookup string comparison</para>
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <para lang="zh">获得/设置 Lookup 服务键</para>
    /// <para lang="en">Gets or sets the lookup service key</para>
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Lookup 服务数据</para>
    /// <para lang="en">Gets or sets the lookup service data</para>
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Lookup 服务实例</para>
    /// <para lang="en">Gets or sets the lookup service instance</para>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 校验规则集合</para>
    /// <para lang="en">Gets or sets the validation rules</para>
    /// </summary>
    [Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    [CascadingParameter]
    private List<IEditorItem>? EditorItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组名称</para>
    /// <para lang="en">Gets or sets the group name</para>
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组顺序</para>
    /// <para lang="en">Gets or sets the group order</para>
    /// </summary>
    [Parameter]
    public int GroupOrder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditorItems?.Add(this);
        if (FieldExpression != null)
        {
            _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
        }

        PropertyType = typeof(TValue);
    }

    private FieldIdentifier? _fieldIdentifier;

    /// <summary>
    /// <para lang="zh">获得显示名称</para>
    /// <para lang="en">Gets the display name</para>
    /// </summary>
    public virtual string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? string.Empty;

    /// <summary>
    /// <para lang="zh">获得字段名称</para>
    /// <para lang="en">Gets the field name</para>
    /// </summary>
    public string GetFieldName() => _fieldIdentifier?.FieldName ?? string.Empty;
}

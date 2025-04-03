// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// EditorItem component
/// </summary>
public class EditorItem<TModel, TValue> : ComponentBase, IEditorItem
{
    /// <summary>
    /// 获得/设置 绑定字段值
    /// </summary>
    [Parameter]
    public TValue? Field { get; set; }

    /// <summary>
    /// 获得/设置 绑定字段值变化回调委托
    /// </summary>
    [Parameter]
    public EventCallback<TValue> FieldChanged { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; set; }

    /// <summary>
    /// 获得/设置 ValueExpression 表达式
    /// </summary>
    [Parameter]
    public Expression<Func<TValue>>? FieldExpression { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，是否可编辑改用 Readonly 参数，是否可见改用 Ignore 参数; Deprecated If it is editable, use the Readonly parameter. If it is visible, use the Ignore parameter.")]
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    [Parameter]
    public bool? Ignore { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    [Parameter]
    public bool? Readonly { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? Required { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

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
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

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
    /// <inheritdoc/>>
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    [CascadingParameter]
    private List<IEditorItem>? EditorItems { get; set; }

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
    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditorItems?.Add(this);
        if (FieldExpression != null)
        {
            _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
        }

        // 获取模型属性定义类型
        PropertyType = typeof(TValue);
    }

    private FieldIdentifier? _fieldIdentifier;

    /// <summary>
    /// Gets the display name for the field.
    /// </summary>
    public virtual string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? string.Empty;

    /// <summary>
    /// Gets the field name for the field.
    /// </summary>
    public string GetFieldName() => _fieldIdentifier?.FieldName ?? string.Empty;
}

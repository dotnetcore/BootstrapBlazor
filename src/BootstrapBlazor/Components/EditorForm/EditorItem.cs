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
    /// <para lang="zh">获得/设置 ValueExpression 表达式</para>
    /// <para lang="en">Gets or sets the value expression</para>
    /// </summary>
    [Parameter]
    public Expression<Func<TValue>>? FieldExpression { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.PropertyType"/>
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Editable"/>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，是否可编辑改用 Readonly 参数，是否可见改用 Ignore 参数; Deprecated If it is editable, use the Readonly parameter. If it is visible, use the Ignore parameter.")]
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Ignore"/>
    /// </summary>
    [Parameter]
    public bool? Ignore { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Readonly"/>
    /// </summary>
    [Parameter]
    public bool? Readonly { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Required"/>
    /// </summary>
    [Parameter]
    public bool? Required { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.RequiredErrorMessage"/>
    /// </summary>
    [Parameter]
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.SkipValidate"/>
    /// </summary>
    [Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ShowLabelTooltip"/>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Text"/>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Step"/>
    /// </summary>
    [Parameter]
    public string? Step { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Rows"/>
    /// </summary>
    [Parameter]
    public int Rows { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Cols"/>
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
    /// <inheritdoc cref="IEditorItem.ComponentType"/>
    /// </summary>
    [Parameter]
    public Type? ComponentType { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ComponentParameters"/>
    /// </summary>
    [Parameter]
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.PlaceHolder"/>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Order"/>
    /// </summary>
    [Parameter]
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Items"/>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.Lookup"/>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ShowSearchWhenSelect"/>
    /// </summary>
    [Parameter]
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.IsFixedSearchWhenSelect"/>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.IsPopover"/>
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupStringComparison"/>
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupServiceKey"/>
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupServiceData"/>
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupService"/>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ValidateRules"/>
    /// </summary>
    [Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GroupName"/>
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GroupOrder"/>
    /// </summary>
    [Parameter]
    public int GroupOrder { get; set; }

    [CascadingParameter]
    private List<IEditorItem>? EditorItems { get; set; }

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
    /// <inheritdoc cref="IEditorItem.GetDisplayName"/>
    /// </summary>
    public virtual string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? string.Empty;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GetFieldName"/>
    /// </summary>
    public string GetFieldName() => _fieldIdentifier?.FieldName ?? string.Empty;
}

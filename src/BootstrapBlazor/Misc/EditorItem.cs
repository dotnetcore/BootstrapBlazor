// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">EditorItem 表单渲染项实体类</para>
/// <para lang="en">EditorItem form field class</para>
/// </summary>
/// <param name="fieldName"></param>
/// <param name="fieldType"></param>
/// <param name="fieldText"></param>
public class EditorItem<TModel>(string fieldName, Type fieldType, string? fieldText = null) : IEditorItem
{
    private string FieldName { get; } = fieldName;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.SkipValidate"/>
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Ignore"/>
    /// </summary>
    public bool? Ignore { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Readonly"/>
    /// </summary>
    public bool? Readonly { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Required"/>
    /// </summary>
    public bool? Required { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.RequiredErrorMessage"/>
    /// </summary>
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ShowLabelTooltip"/>
    /// </summary>
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.PlaceHolder"/>
    /// </summary>
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.PropertyType"/>
    /// </summary>
    public Type PropertyType { get; } = fieldType;

    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    bool IEditorItem.Editable { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Step"/>
    /// </summary>
    public string? Step { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Rows"/>
    /// </summary>
    public int Rows { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Cols"/>
    /// </summary>
    public int Cols { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Text"/>
    /// </summary>
    [NotNull]
    public string? Text { get; set; } = fieldText;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.EditTemplate"/>
    /// </summary>
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
    public Type? ComponentType { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ComponentParameters"/>
    /// </summary>
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Items"/>
    /// </summary>
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Order"/>
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.Lookup"/>
    /// </summary>
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ShowSearchWhenSelect"/>
    /// </summary>
    public bool ShowSearchWhenSelect { get; set; }

    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    bool IEditorItem.IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.IsPopover"/>
    /// </summary>
    public bool IsPopover { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupStringComparison"/>
    /// </summary>
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupServiceKey"/>
    /// </summary>
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupServiceData"/>
    /// </summary>
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupService"/>
    /// </summary>
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.OnCellRender"/>
    /// </summary>
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ValidateRules"/>
    /// </summary>
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GroupName"/>
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GroupOrder"/>
    /// </summary>
    public int GroupOrder { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GetDisplayName"/>
    /// </summary>
    public string GetDisplayName() => Text;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GetFieldName"/>
    /// </summary>
    public string GetFieldName() => FieldName;
}

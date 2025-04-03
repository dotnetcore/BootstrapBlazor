// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IEditorItem interface
/// </summary>
public interface IEditorItem : ILookup
{
    /// <summary>
    /// Gets or sets the type of the bound column.
    /// </summary>
    Type PropertyType { get; }

    /// <summary>
    /// Gets or sets whether the current edit item is editable. Default is true.
    /// </summary>
    [Obsolete("Deprecated. Use the Visible parameter. IsVisibleWhenAdd should be used when creating a new one, and IsVisibleWhenEdit should be used when editing. Use the Readonly parameter for read-only. IsReadonlyWhenAdd should be used when creating a new one, and IsReadonlyWhenEdit should be used when editing.")]
    bool Editable { get; set; }

    /// <summary>
    /// Gets or sets whether the current edit item is read-only. Default is false.
    /// </summary>
    bool? Readonly { get; set; }

    /// <summary>
    /// Gets or sets whether the current edit item is ignored. Default is false. When set to true, the UI will not generate this column.
    /// </summary>
    bool? Ignore { get; set; }

    /// <summary>
    /// Gets or sets whether to skip validation. Default is false.
    /// </summary>
    bool SkipValidate { get; set; }

    /// <summary>
    /// Gets or sets the header display text.
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// Gets or sets whether to show label tooltip. Mostly used when the label text is too long and gets truncated. Default is null.
    /// </summary>
    bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text. Default is null.
    /// </summary>
    string? PlaceHolder { get; set; }

    /// <summary>
    /// Gets or sets the additional data source, generally used for components like Select or CheckboxList that require additional configuration.
    /// </summary>
    IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// Gets or sets the step. Default is null. When set to "any", validation is ignored.
    /// </summary>
    string? Step { get; set; }

    /// <summary>
    /// Gets or sets the number of rows for a Textarea. Default is 0.
    /// </summary>
    int Rows { get; set; }

    /// <summary>
    /// Gets or sets the field expand columns. Default is 0.
    /// </summary>
    int Cols { get; set; }

    /// <summary>
    /// Gets or sets the edit template.
    /// </summary>
    RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// Gets or sets the component type. Default is null.
    /// </summary>
    Type? ComponentType { get; set; }

    /// <summary>
    /// Gets or sets the custom component parameters. Default is null.
    /// </summary>
    IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// Gets or sets whether to show the search bar in the dropdown list. Default is false.
    /// </summary>
    bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// Gets or sets whether to allow fixed search box within dropdown. Default is false.
    /// </summary>
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// Gets or sets whether to use Popover to render the dropdown list. Default is false.
    /// </summary>
    bool IsPopover { get; set; }

    /// <summary>
    /// Gets or sets the custom validation rules.
    /// </summary>
    List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// Gets the display name of the bound field.
    /// </summary>
    string GetDisplayName();

    /// <summary>
    /// Gets the field information of the bound field.
    /// </summary>
    string GetFieldName();

    /// <summary>
    /// Gets or sets the order number.
    /// </summary>
    int Order { get; set; }

    /// <summary>
    /// Gets or sets the group name of the current property.
    /// </summary>
    string? GroupName { get; set; }

    /// <summary>
    /// Gets or sets the group order of the current property. Default is 0.
    /// </summary>
    int GroupOrder { get; set; }

    /// <summary>
    /// Gets or sets whether the field is required. Default is null.
    /// </summary>
    bool? Required { get; set; }

    /// <summary>
    /// Gets or sets the error message when the required field is missing. Default is null.
    /// </summary>
    string? RequiredErrorMessage { get; set; }
}

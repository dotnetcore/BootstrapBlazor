// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IEditorItem interface
///</para>
/// <para lang="en">IEditorItem interface
///</para>
/// </summary>
public interface IEditorItem : ILookup
{
    /// <summary>
    /// <para lang="zh">获得/设置 the 类型 of the bound column.
    ///</para>
    /// <para lang="en">Gets or sets the type of the bound column.
    ///</para>
    /// </summary>
    Type PropertyType { get; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the current edit item is editable. 默认为 true.
    ///</para>
    /// <para lang="en">Gets or sets whether the current edit item is editable. Default is true.
    ///</para>
    /// </summary>
    [Obsolete("Deprecated. Use the Visible parameter. IsVisibleWhenAdd should be used when creating a new one, and IsVisibleWhenEdit should be used when editing. Use the Readonly parameter for read-only. IsReadonlyWhenAdd should be used when creating a new one, and IsReadonlyWhenEdit should be used when editing.")]
    bool Editable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the current edit item is read-only. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether the current edit item is read-only. Default is false.
    ///</para>
    /// </summary>
    bool? Readonly { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the current edit item is ignored. 默认为 false. When set to true, the UI will not generate this column.
    ///</para>
    /// <para lang="en">Gets or sets whether the current edit item is ignored. Default is false. When set to true, the UI will not generate this column.
    ///</para>
    /// </summary>
    bool? Ignore { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to skip validation. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether to skip validation. Default is false.
    ///</para>
    /// </summary>
    bool SkipValidate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the header 显示 text.
    ///</para>
    /// <para lang="en">Gets or sets the header display text.
    ///</para>
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show label tooltip. Mostly used when the label text is too long and gets truncated. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether to show label tooltip. Mostly used when the label text is too long and gets truncated. Default is null.
    ///</para>
    /// </summary>
    bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the placeholder text. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets the placeholder text. Default is null.
    ///</para>
    /// </summary>
    string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the additional 数据 source, generally used for components like Select or CheckboxList that require additional configuration.
    ///</para>
    /// <para lang="en">Gets or sets the additional data source, generally used for components like Select or CheckboxList that require additional configuration.
    ///</para>
    /// </summary>
    IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the step. 默认为 null. When set to "any", validation is ignored.
    ///</para>
    /// <para lang="en">Gets or sets the step. Default is null. When set to "any", validation is ignored.
    ///</para>
    /// </summary>
    string? Step { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the number of rows for a Textarea. 默认为 0.
    ///</para>
    /// <para lang="en">Gets or sets the number of rows for a Textarea. Default is 0.
    ///</para>
    /// </summary>
    int Rows { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the field expand columns. 默认为 0.
    ///</para>
    /// <para lang="en">Gets or sets the field expand columns. Default is 0.
    ///</para>
    /// </summary>
    int Cols { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the edit 模板.
    ///</para>
    /// <para lang="en">Gets or sets the edit template.
    ///</para>
    /// </summary>
    RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the component 类型. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets the component type. Default is null.
    ///</para>
    /// </summary>
    Type? ComponentType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the custom component parameters. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets the custom component parameters. Default is null.
    ///</para>
    /// </summary>
    IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show the search bar in the dropdown list. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether to show the search bar in the dropdown list. Default is false.
    ///</para>
    /// </summary>
    bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to allow fixed search box within dropdown. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether to allow fixed search box within dropdown. Default is false.
    ///</para>
    /// </summary>
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to use Popover to render the dropdown list. 默认为 false.
    ///</para>
    /// <para lang="en">Gets or sets whether to use Popover to render the dropdown list. Default is false.
    ///</para>
    /// </summary>
    bool IsPopover { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the custom validation rules.
    ///</para>
    /// <para lang="en">Gets or sets the custom validation rules.
    ///</para>
    /// </summary>
    List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <para lang="zh">获得 the 显示 name of the bound field.
    ///</para>
    /// <para lang="en">Gets the display name of the bound field.
    ///</para>
    /// </summary>
    string GetDisplayName();

    /// <summary>
    /// <para lang="zh">获得 the field information of the bound field.
    ///</para>
    /// <para lang="en">Gets the field information of the bound field.
    ///</para>
    /// </summary>
    string GetFieldName();

    /// <summary>
    /// <para lang="zh">获得/设置 the order number.
    ///</para>
    /// <para lang="en">Gets or sets the order number.
    ///</para>
    /// </summary>
    int Order { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the group name of the current 属性.
    ///</para>
    /// <para lang="en">Gets or sets the group name of the current property.
    ///</para>
    /// </summary>
    string? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the group order of the current 属性. 默认为 0.
    ///</para>
    /// <para lang="en">Gets or sets the group order of the current property. Default is 0.
    ///</para>
    /// </summary>
    int GroupOrder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the field is required. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets whether the field is required. Default is null.
    ///</para>
    /// </summary>
    bool? Required { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the error message when the required field is missing. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets the error message when the required field is missing. Default is null.
    ///</para>
    /// </summary>
    string? RequiredErrorMessage { get; set; }
}

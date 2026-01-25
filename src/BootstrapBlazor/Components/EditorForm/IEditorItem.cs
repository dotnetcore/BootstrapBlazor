// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">EditorItem 接口</para>
/// <para lang="en">EditorItem interface</para>
/// </summary>
public interface IEditorItem : ILookup
{
    /// <summary>
    /// <para lang="zh">获得 绑定列的类型。</para>
    /// <para lang="en">Gets the type of the bound column.</para>
    /// </summary>
    Type PropertyType { get; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可编辑，默认为 true。</para>
    /// <para lang="en">Gets or sets whether the current edit item is editable. Default is true.</para>
    /// </summary>
    [Obsolete("Deprecated. Use the Visible parameter. IsVisibleWhenAdd should be used when creating a new one, and IsVisibleWhenEdit should be used when editing. Use the Readonly parameter for read-only. IsReadonlyWhenAdd should be used when creating a new one, and IsReadonlyWhenEdit should be used when editing.")]
    bool Editable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为只读，默认为 false。</para>
    /// <para lang="en">Gets or sets whether the current edit item is read-only. Default is false.</para>
    /// </summary>
    bool? Readonly { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否忽略当前编辑项，默认为 false，设置为 true 时 UI 不生成此列。</para>
    /// <para lang="en">Gets or sets whether the current edit item is ignored. Default is false. When true, the UI will not generate this column.</para>
    /// </summary>
    bool? Ignore { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否跳过校验，默认为 false。</para>
    /// <para lang="en">Gets or sets whether to skip validation. Default is false.</para>
    /// </summary>
    bool SkipValidate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 表头显示文本。</para>
    /// <para lang="en">Gets or sets the header display text.</para>
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签提示，常用于标签文本过长被截断时，默认为 null。</para>
    /// <para lang="en">Gets or sets whether to show the label tooltip, usually when the label text is too long and truncated. Default is null.</para>
    /// </summary>
    bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 占位符文本，默认为 null。</para>
    /// <para lang="en">Gets or sets the placeholder text. Default is null.</para>
    /// </summary>
    string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 附加数据源，通常用于 Select 或 CheckboxList 等需要额外配置的组件。</para>
    /// <para lang="en">Gets or sets the additional data source, generally used for components like Select or CheckboxList that require additional configuration.</para>
    /// </summary>
    IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 步长，默认为 null，设置为 "any" 时忽略校验。</para>
    /// <para lang="en">Gets or sets the step. Default is null. When set to "any", validation is ignored.</para>
    /// </summary>
    string? Step { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Textarea 的行数，默认为 0。</para>
    /// <para lang="en">Gets or sets the number of rows for a Textarea. Default is 0.</para>
    /// </summary>
    int Rows { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 字段的列跨度，默认为 0。</para>
    /// <para lang="en">Gets or sets the field column span. Default is 0.</para>
    /// </summary>
    int Cols { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑模板。</para>
    /// <para lang="en">Gets or sets the edit template.</para>
    /// </summary>
    RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件类型，默认为 null。</para>
    /// <para lang="en">Gets or sets the component type. Default is null.</para>
    /// </summary>
    Type? ComponentType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义组件参数，默认为 null。</para>
    /// <para lang="en">Gets or sets the custom component parameters. Default is null.</para>
    /// </summary>
    IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否在下拉列表中显示搜索框，默认为 false。</para>
    /// <para lang="en">Gets or sets whether to show the search bar in the dropdown list. Default is false.</para>
    /// </summary>
    bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否在下拉中使用固定搜索框，默认为 false。</para>
    /// <para lang="en">Gets or sets whether to allow a fixed search box within the dropdown. Default is false.</para>
    /// </summary>
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否使用 Popover 渲染下拉列表，默认为 false。</para>
    /// <para lang="en">Gets or sets whether to use Popover to render the dropdown list. Default is false.</para>
    /// </summary>
    bool IsPopover { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义验证规则。</para>
    /// <para lang="en">Gets or sets the custom validation rules.</para>
    /// </summary>
    List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <para lang="zh">获得 绑定字段的显示名称。</para>
    /// <para lang="en">Gets the display name of the bound field.</para>
    /// </summary>
    string GetDisplayName();

    /// <summary>
    /// <para lang="zh">获得 绑定字段的字段名。</para>
    /// <para lang="en">Gets the field name of the bound field.</para>
    /// </summary>
    string GetFieldName();

    /// <summary>
    /// <para lang="zh">获得/设置 顺序号。</para>
    /// <para lang="en">Gets or sets the order number.</para>
    /// </summary>
    int Order { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前属性的分组名称。</para>
    /// <para lang="en">Gets or sets the group name of the current property.</para>
    /// </summary>
    string? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前属性的分组顺序，默认为 0。</para>
    /// <para lang="en">Gets or sets the group order of the current property. Default is 0.</para>
    /// </summary>
    int GroupOrder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为必填项，默认为 null。</para>
    /// <para lang="en">Gets or sets whether the field is required. Default is null.</para>
    /// </summary>
    bool? Required { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 必填项缺失时的错误消息，默认为 null。</para>
    /// <para lang="en">Gets or sets the error message when the required field is missing. Default is null.</para>
    /// </summary>
    string? RequiredErrorMessage { get; set; }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// EditorItem 组件
/// </summary>
/// <remarks>用于 EditorForm 的 FieldItems 模板内</remarks>
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
    /// 获得/设置 绑定列类型
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
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    [Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 表头显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Step { get; set; }

    /// <summary>
    /// 获得/设置 Textarea行数
    /// </summary>
    [Parameter]
    public int Rows { get; set; }

    /// <summary>
    /// 获得/设置 编辑模板
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
    /// 获得/设置 placeholder 文本 默认为 null
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 显示顺序
    /// </summary>
    [Parameter]
    public int Order { get; set; }

    /// <summary>
    /// 获得/设置 额外数据源一般用于下拉框或者 CheckboxList 这种需要额外配置数据源组件使用
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

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
    /// 获得/设置 自定义验证集合
    /// </summary>
    [Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// 获得/设置 IEditorItem 集合实例
    /// </summary>
    /// <remarks>EditorForm 组件级联传参下来的值</remarks>
    [CascadingParameter]
    private List<IEditorItem>? EditorItems { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组
    /// </summary>
    [Parameter]
    public string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组排序 默认 0
    /// </summary>
    [Parameter]
    public int GroupOrder { get; set; }

    /// <summary>
    /// OnInitialized 方法
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
    /// 获取绑定字段显示名称方法
    /// </summary>
    public virtual string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? string.Empty;

    /// <summary>
    /// 获取绑定字段信息方法
    /// </summary>
    public string GetFieldName() => _fieldIdentifier?.FieldName ?? string.Empty;
}

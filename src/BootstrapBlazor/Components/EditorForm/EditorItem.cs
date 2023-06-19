// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// EditorItem 组件
/// </summary>
/// <remarks>用于 EditorForm 的 FieldItems 模板内</remarks>
#if NET6_0_OR_GREATER
public class EditorItem<TModel, TValue> : ComponentBase, IEditorItem
#else
public class EditorItem<TValue> : ComponentBase, IEditorItem
#endif
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
    /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
    /// </summary>
    [Parameter]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// 获得/设置 当前列编辑时是否为只读模式 默认为 false
    /// </summary>
    /// <remarks>此属性覆盖 <see cref="IsReadonlyWhenAdd"/> 与 <see cref="IsReadonlyWhenEdit"/> 即新建与编辑时均只读</remarks>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// 获得/设置 新建时此列只读 默认为 false
    /// </summary>
    public bool IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// 获得/设置 编辑时此列只读 默认为 false
    /// </summary>
    public bool IsReadonlyWhenEdit { get; set; }

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
    /// 获得/设置 步长
    /// </summary>
    [Parameter]
    public object? Step { get; set; }

    /// <summary>
    /// 获得/设置 Textarea行数
    /// </summary>
    [Parameter]
    public int Rows { get; set; }

    /// <summary>
    /// 获得/设置 编辑模板
    /// </summary>
    [Parameter]
#if NET5_0
    public RenderFragment<object>? EditTemplate { get; set; }
#elif NET6_0_OR_GREATER
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
#endif

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
    /// 获得/设置 字典数据源字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// 获得/设置 LookupService 服务获取 Lookup 数据集合键值 常用于外键自动转换为名称操作
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

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

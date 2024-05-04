// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IEditorItem 接口
/// </summary>
public interface IEditorItem
{
    /// <summary>
    /// 获得/设置 绑定列类型
    /// </summary>
    Type PropertyType { get; }

    /// <summary>
    /// 获得/设置 当前编辑项是否可编辑 默认为 true
    /// </summary>
    [Obsolete("已弃用，是否显示使用 Visible 参数，新建时使用 IsVisibleWhenAdd 编辑时使用 IsVisibleWhenEdit 只读使用 Readonly 参数，新建时使用 IsReadonlyWhenAdd 编辑时使用 IsReadonlyWhenEdit 参数; Discarded, use Visible parameter. IsVisibleWhenAdd should be used when creating a new one, and IsVisibleWhenEdit should be used when editing")]
    bool Editable { get; set; }

    /// <summary>
    /// 获得/设置 当前编辑项是否只读 默认为 false
    /// </summary>
    bool Readonly { get; set; }

    /// <summary>
    /// 获得/设置 当前编辑项是否忽略 默认为 false 当设置为 true 时 UI 不生成此列
    /// </summary>
    bool Ignore { get; set; }

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    bool SkipValidate { get; set; }

    /// <summary>
    /// 获得/设置 表头显示文字
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 placeholder 文本 默认为 null
    /// </summary>
    string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 额外数据源一般用于 Select 或者 CheckboxList 这种需要额外配置数据源组件使用
    /// </summary>
    IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 步长 默认为 null 设置 any 时忽略检查
    /// </summary>
    string? Step { get; set; }

    /// <summary>
    /// 获得/设置 Textarea 行数 默认为 0
    /// </summary>
    int Rows { get; set; }

    /// <summary>
    /// 获得/设置 编辑模板
    /// </summary>
    RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件类型 默认为 null
    /// </summary>
    Type? ComponentType { get; set; }

    /// <summary>
    /// 获得/设置 组件自定义类型参数集合 默认为 null
    /// </summary>
    IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源 常用于外键自动转换为名称操作
    /// </summary>
    IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 字段数据源下拉框是否显示搜索栏 默认 false 不显示
    /// </summary>
    bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// 获得/设置 是否使用 Popover 渲染下拉框 默认 false
    /// </summary>
    bool IsPopover { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源字符串比较规则 默认 <see cref="StringComparison.OrdinalIgnoreCase" /> 大小写不敏感 
    /// </summary>
    StringComparison LookupStringComparison { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值 常用于外键自动转换为名称操作，可以通过 <see cref="LookupServiceData"/> 传递自定义数据
    /// </summary>
    string? LookupServiceKey { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值自定义数据，通过 <see cref="LookupServiceKey"/> 指定键值
    /// </summary>
    object? LookupServiceData { get; set; }

    /// <summary>
    /// 获得/设置 自定义验证集合
    /// </summary>
    List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// 获取绑定字段显示名称方法
    /// </summary>
    string GetDisplayName();

    /// <summary>
    /// 获取绑定字段信息方法
    /// </summary>
    string GetFieldName();

    /// <summary>
    /// 获得/设置 顺序号
    /// </summary>
    int Order { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组
    /// </summary>
    string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组排序 默认 0
    /// </summary>
    int GroupOrder { get; set; }
}

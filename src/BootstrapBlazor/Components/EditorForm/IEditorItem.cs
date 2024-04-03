﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
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
    /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
    /// </summary>
    bool Editable { get; set; }

    /// <summary>
    /// 获得/设置 当前编辑项是否只读 默认为 false
    /// <para>新建与编辑时可单独设置 <see cref="IsReadonlyWhenAdd"/> 与 <seealso cref="IsReadonlyWhenEdit"/></para>
    /// </summary>
    bool Readonly { get; set; }

    /// <summary>
    /// 获得/设置 新建时此列只读 默认为 null 使用 <see cref="Readonly"/> 值
    /// </summary>
    bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// 获得/设置 编辑时此列只读 默认为 null 使用 <see cref="Readonly"/> 值
    /// </summary>
    bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// 获得/设置 当前编辑项是否显示 默认为 true
    /// <para>新建与编辑时可单独设置 <see cref="IsVisibleWhenAdd"/> 与 <seealso cref="IsVisibleWhenEdit"/></para>
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// 获得/设置 新建时是否此列显示  默认为 null 使用 <see cref="Visible"/> 值
    /// </summary>
    bool? IsVisibleWhenAdd { get; set; }

    /// <summary>
    /// 获得/设置 编辑时是否此列显示  默认为 null 使用 <see cref="Visible"/> 值
    /// </summary>
    bool? IsVisibleWhenEdit { get; set; }

    /// <summary>
    /// 获得/设置 渲染时是否忽略本列 默认为 false 不忽略
    /// </summary>
    /// <remarks>忽略列不会出现在 ColumnList 列表中，<see cref="Visible"/> 为 False 列会出现在 ColumnList 中</remarks>
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

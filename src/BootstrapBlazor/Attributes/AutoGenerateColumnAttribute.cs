// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoGenerateColumnAttribute : AutoGenerateBaseAttribute, ITableColumn
    {
        /// <summary>
        /// 获得/设置 显示顺序 ，规则如下：
        /// <para></para>
        /// &gt;0时排前面，1,2,3...
        /// <para></para>
        /// =0时排中间(默认)
        /// <para></para>
        /// &lt;0时排后面，...-3,-2,-1
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 获得/设置 是否忽略 默认为 false 不忽略
        /// </summary>
        public bool Ignore { get; set; }

        /// <summary>
        /// 获得/设置 是否为默认排序列 默认为 false
        /// </summary>
        public bool DefaultSort { get; set; }

        /// <summary>
        /// 获得/设置 是否不进行验证 默认为 false
        /// </summary>
        public bool SkipValidate { get; set; }

        /// <summary>
        /// 获得/设置 是否为默认排序规则 默认为 SortOrder.Unset
        /// </summary>
        public SortOrder DefaultSortOrder { get; set; }

        IEnumerable<SelectedItem>? IEditorItem.Data { get; set; }

        /// <summary>
        /// 获得/设置 列宽
        /// </summary>
        public int Width { get; set; }

        int? ITableColumn.Width
        {
            get => Width <= 0 ? null : Width;
            set => Width = value == null ? 0 : Width;
        }

        /// <summary>
        /// 获得/设置 是否固定本列 默认 false 不固定
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// 获得/设置 列是否显示 默认为 true 可见的
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 获得/设置 列 td 自定义样式 默认为 null 未设置
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// 获得/设置 显示节点阈值 默认值 BreakPoint.None 未设置
        /// </summary>
        public BreakPoint ShownWithBreakPoint { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        public string? FormatString { get; set; }

        /// <summary>
        /// 获得/设置 列格式化回调委托
        /// </summary>
        public Func<object?, Task<string>>? Formatter { get; set; }

        /// <summary>
        /// 获得/设置 编辑模板
        /// </summary>
        RenderFragment<object>? IEditorItem.EditTemplate { get; set; }

        /// <summary>
        /// 获得/设置 组件类型 默认为 null
        /// </summary>
        public Type? ComponentType { get; set; }

        RenderFragment<object>? ITableColumn.Template { get => Template; }

        /// <summary>
        /// 获得/设置 显示模板
        /// </summary>
        internal RenderFragment<object>? Template { get; set; }

        /// <summary>
        /// 获得/设置 搜索模板
        /// </summary>
        RenderFragment<object>? ITableColumn.SearchTemplate { get; set; }

        /// <summary>
        /// 获得/设置 过滤模板
        /// </summary>
        RenderFragment? ITableColumn.FilterTemplate { get; set; }

        /// <summary>
        /// 获得/设置 步长 默认为 1
        /// </summary>
        public object? Step { get; set; }

        /// <summary>
        /// 获得/设置 Textarea 行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 获得/设置 列过滤器
        /// </summary>
        public IFilter? Filter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        public Type? PropertyType { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        internal string? FieldName { get; set; }

        /// <summary>
        /// 获得/设置 字典数据源 常用于外键自动转换为名称操作
        /// </summary>
        IEnumerable<SelectedItem>? IEditorItem.Lookup { get; set; }

        /// <summary>
        /// 获得/设置 单元格回调方法
        /// </summary>
        Action<TableCellArgs>? ITableColumn.OnCellRender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string? GetDisplayName() => Text;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFieldName() => FieldName;
    }
}

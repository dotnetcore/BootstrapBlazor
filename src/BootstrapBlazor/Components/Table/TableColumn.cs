// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表头组件
    /// </summary>
    /// <typeparam name="TType">绑定字段值类型</typeparam>
    public class TableColumn<TType> : BootstrapComponentBase, ITableColumn
    {
        /// <summary>
        /// 获得/设置 相关过滤器
        /// </summary>
        public IFilter? Filter { get; set; }

        /// <summary>
        /// 获得/设置 绑定列类型
        /// </summary>
        [NotNull]
        public Type? PropertyType { get; set; }

        /// <summary>
        /// 获得/设置 数据绑定字段值
        /// </summary>
        [Parameter]
        [MaybeNull]
        public TType Field { get; set; }

        /// <summary>
        /// 获得/设置 ValueExpression 表达式
        /// </summary>
        [Parameter]
        public Expression<Func<TType>>? FieldExpression { get; set; }

        /// <summary>
        /// 获得/设置 是否排序 默认 false
        /// </summary>
        [Parameter]
        public bool Sortable { get; set; }

        /// <summary>
        /// 获得/设置 是否为默认排序列 默认为 false
        /// </summary>
        [Parameter]
        public bool DefaultSort { get; set; }

        /// <summary>
        /// 获得/设置 本列是否允许换行 默认为 false
        /// </summary>
        [Parameter]
        public bool AllowTextWrap { get; set; }

        /// <summary>
        /// 获得/设置 本列文本超出省略 默认为 false
        /// </summary>
        [Parameter]
        public bool TextEllipsis { get; set; }

        /// <summary>
        /// 获得/设置 是否为默认排序规则 默认为 SortOrder.Unset
        /// </summary>
        [Parameter]
        public SortOrder DefaultSortOrder { get; set; }

        /// <summary>
        /// 获得/设置 是否可过滤数据 默认 false
        /// </summary>
        [Parameter]
        public bool Filterable { get; set; }

        /// <summary>
        /// 获得/设置 是否参与搜索自动生成 默认 false
        /// </summary>
        [Parameter]
        public bool Searchable { get; set; }

        /// <summary>
        /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
        /// </summary>
        [Parameter]
        public bool Editable { get; set; } = true;

        /// <summary>
        /// 获得/设置 当前列编辑时是否为只读模式 默认为 false
        /// </summary>
        [Parameter]
        public bool Readonly { get; set; }

        /// <summary>
        /// 获得/设置 表头显示文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 列宽 默认为 auto
        /// </summary>
        [Parameter]
        public int? Width { get; set; }

        /// <summary>
        /// 获得/设置 是否固定本列 默认 false 不固定
        /// </summary>
        [Parameter]
        public bool Fixed { get; set; }

        /// <summary>
        /// 获得/设置 是否显示本列 默认 true 显示
        /// </summary>
        [Parameter]
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 获得/设置 字段鼠标悬停提示
        /// </summary>
        [Parameter]
        public bool ShowTips { get; set; }

        /// <summary>
        /// 获得/设置 列 td 自定义样式 默认为 null 未设置
        /// </summary>
        [Parameter]
        public string? CssClass { get; set; }

        /// <summary>
        /// 获得/设置 文字对齐方式 默认为 Alignment.None
        /// </summary>
        [Parameter]
        public Alignment Align { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        [Parameter]
        public string? FormatString { get; set; }

        /// <summary>
        /// 获得/设置 列格式化回调委托
        /// </summary>
        [Parameter]
        public Func<object?, Task<string>>? Formatter { get; set; }

        /// <summary>
        /// 获得/设置 显示模板
        /// </summary>
        [Parameter]
        public RenderFragment<TableColumnContext<object, TType>>? Template { get; set; }

        /// <summary>
        /// 获得/设置 编辑模板
        /// </summary>
        [Parameter]
        public RenderFragment<object>? EditTemplate { get; set; }

        /// <summary>
        /// 获得/设置 搜索模板
        /// </summary>
        /// <value></value>
        [Parameter]
        public RenderFragment<object>? SearchTemplate { get; set; }

        /// <summary>
        /// 获得/设置 过滤模板
        /// </summary>
        [Parameter]
        public RenderFragment? FilterTemplate { get; set; }

        /// <summary>
        /// 获得/设置 显示节点阈值 默认值 BreakPoint.None 未设置
        /// </summary>
        [Parameter]
        public BreakPoint ShownWithBreakPoint { get; set; }

        /// <summary>
        /// 获得/设置 显示顺序
        /// </summary>
        [Parameter]
        public int Order { get; set; }

        /// <summary>
        /// 获得/设置 Table 实例
        /// </summary>
        [CascadingParameter]
        protected ITable? Table { get; set; }

        /// <summary>
        /// 内部使用负责把 object 类型的绑定数据值转化为泛型数据传递给前端
        /// </summary>
        RenderFragment<object>? ITableColumn.Template
        {
            get => Template == null ? null : new RenderFragment<object>(context => builder =>
            {
                // 此处 context 为行数据
                // 将绑定字段值放入上下文中
                var invoker = GetPropertyCache.GetOrAdd((context.GetType(), GetFieldName()), key => context.GetPropertyValueLambda<object, TType>(key.FieldName).Compile());
                var value = invoker(context);
                builder.AddContent(0, Template.Invoke(new TableColumnContext<object, TType>(context, value)));
            });
        }

        /// <summary>
        /// 组件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            Table?.Columns.Add(this);
            if (FieldExpression != null) _fieldIdentifier = FieldIdentifier.Create(FieldExpression);

            // 获取模型属性定义类型
            PropertyType = typeof(TType);
        }

        private FieldIdentifier? _fieldIdentifier;
        /// <summary>
        /// 获取绑定字段显示名称方法
        /// </summary>
        public string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? "";

        /// <summary>
        /// 获取绑定字段信息方法
        /// </summary>
        public string GetFieldName() => _fieldIdentifier?.FieldName ?? "";

        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), Func<object, TType>> GetPropertyCache = new ConcurrentDictionary<(Type, string), Func<object, TType>>();
    }
}

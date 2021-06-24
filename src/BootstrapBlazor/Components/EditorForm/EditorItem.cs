// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// EditorItem 组件
    /// </summary>
    /// <remarks>用于 EditorForm 的 FieldItems 模板内</remarks>
    public class EditorItem<TValue> : ComponentBase, IEditorItem
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
        [Parameter]
        public bool Readonly { get; set; }

        /// <summary>
        /// 获得/设置 是否不进行验证 默认为 false
        /// </summary>
        [Parameter]
        public bool SkipValidate { get; set; }

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
        public RenderFragment<object>? EditTemplate { get; set; }

        /// <summary>
        /// 获得/设置 组件类型 默认为 null
        /// </summary>
        [Parameter]
        public Type? ComponentType { get; set; }

        /// <summary>
        /// 获得/设置 显示顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 获得/设置 额外数据源一般用于下拉框或者 CheckboxList 这种需要额外配置数据源组件使用
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem>? Data { get; set; }

        /// <summary>
        /// 获得/设置 字典数据源 常用于外键自动转换为名称操作
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem>? Lookup { get; set; }

        /// <summary>
        /// 获得/设置 IEditorItem 集合实例
        /// </summary>
        /// <remarks>EditorForm 组件级联传参下来的值</remarks>
        [CascadingParameter]
        private List<IEditorItem>? EditorItems { get; set; }

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
        public string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? string.Empty;

        /// <summary>
        /// 获取绑定字段信息方法
        /// </summary>
        public string GetFieldName() => _fieldIdentifier?.FieldName ?? string.Empty;

        /// <summary>
        /// 获得指定泛型的 IEditorItem 集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IEditorItem> GenerateEditorItems() => InternalTableColumn.GetProperties<TValue>();
    }
}

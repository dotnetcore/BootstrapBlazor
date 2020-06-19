using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表头组件
    /// </summary>
    public class TableColumn : BootstrapComponentBase, ITableColumn
    {
        /// <summary>
        /// 获得/设置 数据绑定字段值
        /// </summary>
        [Parameter]
        public object? Field { get; set; }

        /// <summary>
        /// 获得/设置 ValueExpression 表达式
        /// </summary>
        [Parameter]
        public Expression<Func<object?>>? FieldExpression { get; set; }

        /// <summary>
        /// 获得/设置 是否排序 默认 false
        /// </summary>
        [Parameter]
        public bool Sortable { get; set; }

        /// <summary>
        /// 获得/设置 表头显示文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 模板
        /// </summary>
        [Parameter]
        public RenderFragment<object>? Template { get; set; }

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Columns { get; set; }

        /// <summary>
        /// 组件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            Columns?.Columns.Add(this);
            _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
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
    }
}

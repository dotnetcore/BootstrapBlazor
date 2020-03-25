using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表头组件
    /// </summary>
    public class TableHeader<TItem> : BootstrapComponentBase, ITableHeader
    {
#nullable disable
        /// <summary>
        /// 获得/设置 数据绑定字段值
        /// </summary>
        [Parameter] public TItem Field { get; set; }
#nullable restore

        /// <summary>
        /// 获得/设置 ValueExpression 表达式
        /// </summary>
        [Parameter] public Expression<Func<TItem>>? FieldExpression { get; set; }

        /// <summary>
        /// 获得/设置 是否排序 默认 false
        /// </summary>
        [Parameter] public bool Sort { get; set; }

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableHeaderCollection? Headers { get; set; }

        /// <summary>
        /// 组件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            Headers?.AddHeader(this);
        }

        private FieldIdentifier? _fieldIdentifier;
        /// <summary>
        /// 获取绑定字段显示名称方法
        /// </summary>
        public string GetDisplayName()
        {
            if (_fieldIdentifier == null) _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
            return _fieldIdentifier?.GetDisplayName() ?? "";
        }

        /// <summary>
        /// 获取绑定字段信息方法
        /// </summary>
        public string GetFieldName()
        {
            if (_fieldIdentifier == null) _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
            return _fieldIdentifier?.FieldName ?? "";
        }
    }
}

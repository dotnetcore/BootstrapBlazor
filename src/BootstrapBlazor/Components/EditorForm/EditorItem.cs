using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class EditorItem<TValue> : ComponentBase, IEditorItem
    {
#nullable disable
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public TValue Field { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<TValue> FieldChanged { get; set; }

        /// <summary>
        /// 获得/设置 绑定列类型
        /// </summary>
        public Type FieldType { get; set; }
#nullable restore

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

        [CascadingParameter]
        private List<IEditorItem>? EditorItems { get; set; }

        /// <summary>
        /// 获得/设置 编辑模板
        /// </summary>
        [Parameter]
        public RenderFragment<object>? EditTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            EditorItems?.Add(this);
            if (FieldExpression != null) _fieldIdentifier = FieldIdentifier.Create(FieldExpression);

            // 获取模型属性定义类型
            FieldType = typeof(TValue);
        }

        private FieldIdentifier? _fieldIdentifier;
        /// <summary>
        /// 获取绑定字段显示名称方法
        /// </summary>
        public string GetDisplayName() => _fieldIdentifier?.GetDisplayName() ?? "";

        /// <summary>
        /// 获取绑定字段信息方法
        /// </summary>
        public string GetFieldName() => _fieldIdentifier?.FieldName ?? "";
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Header 组件
    /// </summary>
    public class TableColumnCollection : ComponentBase
    {
        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 添加表头组件到集合方法
        /// </summary>
        public void Add(ITableColumn header) => Columns.Add(header);

        /// <summary>
        /// 获得 表头集合
        /// </summary>
        public ICollection<ITableColumn> Columns { get; } = new HashSet<ITableColumn>();

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            builder.OpenComponent<CascadingValue<TableColumnCollection>>(index++);
            builder.AddAttribute(index++, "Value", this);
            builder.AddAttribute(index++, "IsFixed", true);
            builder.AddAttribute(index++, "ChildContent", ChildContent);
            builder.CloseComponent();
        }
    }
}

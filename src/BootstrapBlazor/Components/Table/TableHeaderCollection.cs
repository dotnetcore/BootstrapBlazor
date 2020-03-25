using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Header 组件
    /// </summary>
    public class TableHeaderCollection : ComponentBase
    {
        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 添加表头组件到集合方法
        /// </summary>
        public void AddHeader(ITableHeader header) => Headers.Add(header);

        /// <summary>
        /// 获得 表头集合
        /// </summary>
        public ICollection<ITableHeader> Headers { get; } = new HashSet<ITableHeader>();

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            builder.OpenComponent<CascadingValue<TableHeaderCollection>>(index++);
            builder.AddAttribute(index++, "Value", this);
            builder.AddAttribute(index++, "IsFixed", true);
            builder.AddAttribute(index++, "ChildContent", ChildContent);
            builder.CloseComponent();
        }
    }
}

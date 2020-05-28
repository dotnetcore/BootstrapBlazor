using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// 获得/设置 排序回调方法
        /// </summary>
        [Parameter] public Func<string, SortOrder, Task>? OnSortAsync { get; set; }

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

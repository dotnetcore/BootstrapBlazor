using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TableBodyContent<TItem> : ComponentBase where TItem : class
    {
        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        protected TableColumnCollection? Columns { get; set; }

        /// <summary>
        /// 获得/设置 行数据实例
        /// </summary>
        [Parameter]
        public TItem? Item { get; set; }

        /// <summary>
        /// 渲染组件方法
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 渲染正常按钮
            if (Columns != null)
            {
                var index = 0;
                foreach (var col in Columns.Columns)
                {
                    builder.OpenComponent<TableCell>(index++);
                    builder.AddAttribute(index++, nameof(TableCell.ChildContent), GetValue(col));
                    builder.CloseComponent();
                }
            }
        }

        private RenderFragment GetValue(ITableColumn col) => async builder =>
        {
            if (col.Template != null)
            {
                if (Item != null) builder.AddContent(0, col.Template.Invoke(Item));
            }
            else
            {
                string content = "";
                var val = GetItemValue(col.GetFieldName());
                if (col.Formatter != null)
                {
                    // 格式化回调委托
                    content = await col.Formatter(val);
                }
                else if (!string.IsNullOrEmpty(col.FormatString))
                {
                    // 格式化字符串
                    content = val?.Format(col.FormatString) ?? "";
                }
                else
                {
                    content = val?.ToString() ?? "";
                }
                builder.AddContent(0, content);
            }
        };

        private object? GetItemValue(string fieldName)
        {
            object? ret = null;
            if (Item != null)
            {
                var invoker = GetPropertyCache.GetOrAdd((typeof(TItem), fieldName), key => Item.GetPropertyValueLambda<TItem, object>(key.Item2).Compile());
                ret = invoker(Item);
            }
            return ret;
        }

        private static readonly ConcurrentDictionary<(Type, string), Func<TItem, object>> GetPropertyCache = new ConcurrentDictionary<(Type, string), Func<TItem, object>>();
    }
}

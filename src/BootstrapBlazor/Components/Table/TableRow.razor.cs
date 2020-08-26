using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TableRow<TItem> where TItem : class, new()
    {
        private string? ClassString => CssBuilder.Default("")
               .AddClass("active", IsActive)
               .Build();

        /// <summary>
        /// 获得/设置 当前行是否选中
        /// </summary>
        [Parameter]
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public TItem? Item { get; set; }

        /// <summary>
        /// 获得/设置 是否为多选模式
        /// </summary>
        [Parameter]
        public bool IsMultipleSelect { get; set; }

        /// <summary>
        /// 获得/设置 是否显示扩展按钮 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowExtendButtons { get; set; }

        /// <summary>
        /// 获得/设置 是否显示按钮列 默认为 true
        /// </summary>
        /// <remarks>本属性设置为 true 新建编辑删除按钮设置为 false 可单独控制每个按钮是否显示</remarks>
        [Parameter]
        public bool ShowDefaultButtons { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示新建按钮 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowNewButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示编辑按钮 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowEditButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示删除按钮 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowDeleteButton { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<TItem?, CheckboxState> RowCheckState { get; set; } = _ => CheckboxState.UnChecked;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<CheckboxState, TItem, Task> OnCheck { get; set; } = (s, t) => Task.CompletedTask;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<TItem, Task> OnClickEditButton { get; set; } = _ => Task.CompletedTask;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<TItem, Task> OnClickDeleteButton { get; set; } = _ => Task.CompletedTask;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<Task> OnDelete { get; set; } = () => Task.CompletedTask;

        /// <summary>
        /// 获得/设置 单选模式下点击行即选中本行 默认为 true
        /// </summary>
        [Parameter]
        public bool ClickToSelect { get; set; } = true;

        /// <summary>
        /// 获得/设置 单选模式下双击即编辑本行 默认为 false
        /// </summary>
        [Parameter]
        public bool DoubleClickToEdit { get; set; }

        /// <summary>
        /// 获得/设置 单击行回调委托方法
        /// </summary>
        [Parameter]
        public Func<TItem?, Task> OnSelectedRow { get; set; } = _ => Task.CompletedTask;

        /// <summary>
        /// 获得/设置 双击行回调委托方法
        /// </summary>
        [Parameter]
        public Func<TItem?, Task> OnEditRow { get; set; } = _ => Task.CompletedTask;

        /// <summary>
        /// 获得/设置 双击行回调委托方法
        /// </summary>
        [Parameter]
        public Func<TItem?, Task>? OnDoubleClickRowCallback { get; set; }

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        [CascadingParameter]
        private TableBase<TItem>? Table { get; set; }

        private string? GetCellClassString(ITableColumn col) => CssBuilder.Default("table-cell")
            .AddClass("justify-content-start", col.Align == Alignment.Left)
            .AddClass("justify-content-end", col.Align == Alignment.Right)
            .AddClass("justify-content-center", col.Align == Alignment.Center)
            .Build();

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
                else if (col.FieldType.IsEnum())
                {
                    content = col.FieldType.ToDescriptionString(val?.ToString());
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

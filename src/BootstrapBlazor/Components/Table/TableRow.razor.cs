using Microsoft.AspNetCore.Components;
using System;
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
        public Action<TItem> OnClickEditButton { get; set; } = t => { };

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Action<TItem> OnClickDeleteButton { get; set; } = t => { };

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
        public Action<TItem?> OnSelectedRow { get; set; } = _ => { };

        /// <summary>
        /// 获得/设置 单击行回调委托方法
        /// </summary>
        [Parameter]
        public Action<TItem?> OnEditRow { get; set; } = _ => { };
    }
}

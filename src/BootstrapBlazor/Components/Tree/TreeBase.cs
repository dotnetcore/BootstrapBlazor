// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Tree 组件基类
    /// </summary>
    public abstract class TreeBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 Tree 组件实例引用
        /// </summary>
        protected ElementReference TreeElement { get; set; }

        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        protected string? ClassString => CssBuilder.Default("tree")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 TreeItem 图标
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetIconClassString(TreeItem item) => CssBuilder.Default("tree-icon")
            .AddClass(item.Icon)
            .Build();

        /// <summary>
        /// 获得/设置 TreeItem 小箭头样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetCaretClassString(TreeItem item) => CssBuilder.Default("fa fa-caret-right")
            .AddClass("invisible", !item.Items.Any())
            .AddClass("fa-rotate-90", item.IsExpanded)
            .Build();

        /// <summary>
        /// 获得/设置 当前行样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetItemClassString(TreeItem item) => CssBuilder.Default("tree-item")
            .AddClass("active", ActiveItem == item)
            .Build();

        /// <summary>
        /// 获得/设置 TreeNode 样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetTreeNodeClassString(TreeItem item) => CssBuilder.Default("tree-ul")
            .AddClass("show", item.IsExpanded)
            .Build();

        /// <summary>
        /// 获得/设置 当前激活的节点实例
        /// </summary>
        protected TreeItem? ActiveItem { get; set; }

        /// <summary>
        /// 获得/设置 是否为手风琴效果 默认为 false
        /// </summary>
        [Parameter]
        public bool IsAccordion { get; set; }

        /// <summary>
        /// 获得/设置 是否点击节点时展开或者收缩子项 默认 false
        /// </summary>
        [Parameter]
        public bool ClickToggleNode { get; set; }

        /// <summary>
        /// 获得/设置 菜单数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<TreeItem> Items { get; set; } = new TreeItem[0];

        /// <summary>
        /// 获得/设置 是否显示 CheckBox 默认 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowCheckbox { get; set; }

        /// <summary>
        /// 获得/设置 是否显示 Icon 图标
        /// </summary>
        [Parameter]
        public bool ShowIcon { get; set; }

        /// <summary>
        /// 获得/设置 树形控件节点点击时回调委托
        /// </summary>
        [Parameter]
        public Func<TreeItem, Task> OnTreeItemClick { get; set; } = item => Task.CompletedTask;

        /// <summary>
        /// 获得/设置 树形控件节点选中时回调委托
        /// </summary>
        [Parameter]
        public Func<TreeItem, Task> OnTreeItemChecked { get; set; } = item => Task.CompletedTask;

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(TreeElement, "bb_tree");
            }
        }

        /// <summary>
        /// 选中节点时触发此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClick(TreeItem item)
        {
            ActiveItem = item;
            if (ClickToggleNode) OnExpandRow(item);
            if (OnTreeItemClick != null) await OnTreeItemClick.Invoke(item);
        }

        /// <summary>
        /// 更改节点是否展开方法
        /// </summary>
        /// <param name="item"></param>
        protected void OnExpandRow(TreeItem item)
        {
            if (IsAccordion)
            {
                if (Items.Contains(item))
                {
                    foreach (var rootNode in Items.Where(p => p.IsExpanded && p != item)) rootNode.IsExpanded = false;
                }
                else
                {
                    item.CollapseOtherNodes();
                }
            }
            item.IsExpanded = !item.IsExpanded;
        }

        /// <summary>
        /// 节点 Checkbox 状态改变时触发此方法
        /// </summary>
        /// <param name="state"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected async Task OnStateChanged(CheckboxState state, TreeItem item)
        {
            // 向下级联操作
            item.CascadeSetCheck(item.Checked);

            if (OnTreeItemChecked != null) await OnTreeItemChecked.Invoke(item);
        }
    }
}

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
    public abstract class TreeBase : CollapseBase
    {
        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        protected override string? ClassString => CssBuilder.Default("accordion tree")
            .AddClass("is-accordion", IsAccordion)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 TreeNode 样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetTreeItemClassString(TreeItem item) => CssBuilder.Default("tree-item")
            .AddClass("is-expanded", item.IsExpanded)
            .Build();

        /// <summary>
        /// 获得 SubTree 样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetSubTreeLinkClassString(TreeItem item) => CssBuilder.Default("nav-link show collapse")
            .AddClass("collapsed", !item.IsExpanded)
            .Build();

        /// <summary>
        /// 获得 SubTree 样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetSubTreeClassString(TreeItem item) => CssBuilder.Default("collapse-item collapse")
            .AddClass("show", item.IsExpanded)
            .Build();

        /// <summary>
        /// 获得 是否展开字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetExpandedString(TreeItem item) => item.IsExpanded ? "true" : "false";

        /// <summary>
        /// 获得/设置 TreeItem 图标
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetIconClassString(TreeItem item) => CssBuilder.Default("tree-icon")
            .AddClass(item.Icon)
            .Build();

        /// <summary>
        /// 获得/设置 菜单数据集合
        /// </summary>
        [Parameter]
        public new IEnumerable<TreeItem> Items { get; set; } = new TreeItem[0];

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
        /// 选中节点时触发此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClick(TreeItem item)
        {
            if (this.IsAccordion){
                foreach (var treeItem in Items.Where(p => p.IsExpanded && p!=item))
                    treeItem.IsExpanded = !treeItem.IsExpanded;
            }
            if (item.Items.Any()) item.IsExpanded = !item.IsExpanded;
            if (OnTreeItemClick != null) await OnTreeItemClick.Invoke(item);
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

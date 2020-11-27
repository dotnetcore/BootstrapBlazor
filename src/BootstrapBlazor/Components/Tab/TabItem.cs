// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TabItem 组件基类
    /// </summary>
    public class TabItem : ComponentBase
    {
        /// <summary>
        /// 获得/设置 文本文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 当前状态是否激活
        /// </summary>
        [Parameter]
        public bool IsActive { get; set; }

        /// <summary>
        /// 获得/设置 图标字符串 如 "fa fa"
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 组件内容
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 相关键值
        /// </summary>
        [Parameter]
        public object? Key { get; set; }

        /// <summary>
        /// 获得/设置 所属 Tab 实例
        /// </summary>
        [CascadingParameter]
        protected TabBase? TabSet { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TabSet?.AddItem(this);
        }

        /// <summary>
        /// 设置是否被选中方法
        /// </summary>
        /// <param name="active"></param>
        public virtual void SetActive(bool active) => IsActive = active;
    }
}

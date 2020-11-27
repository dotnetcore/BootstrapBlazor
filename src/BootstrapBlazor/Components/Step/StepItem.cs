// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Step 组件项类
    /// </summary>
    public class StepItem
    {
        /// <summary>
        /// 获得/设置 步骤显示文字
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 步骤显示图标
        /// </summary>
        public string Icon { get; set; } = "fa fa-check";

        /// <summary>
        /// 获得/设置 步骤状态
        /// </summary>
        public StepStatus Status { get; set; }

        /// <summary>
        /// 获得/设置 描述信息
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 获得/设置 每个 step 的间距不填写将自适应间距支持百分比
        /// </summary>
        public string? Space { get; set; }

        /// <summary>
        /// 获得/设置 进度条是否充满 默认 false
        /// </summary>
        internal bool Line { get; set; }
    }
}

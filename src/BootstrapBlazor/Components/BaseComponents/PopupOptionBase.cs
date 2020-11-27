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
    /// 弹窗类配置项基类
    /// </summary>
    public abstract class PopupOptionBase
    {
        /// <summary>
        /// 获得/设置 Toast Body 子组件
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 是否自动隐藏
        /// </summary>
        public bool IsAutoHide { get; set; } = true;

        /// <summary>
        /// 获得/设置 自动隐藏时间间隔
        /// </summary>
        public int Delay { get; set; } = 4000;

        /// <summary>
        /// 获得/设置 是否强制使用本实例的延时时间，防止值被全局配置覆盖 默认 false
        /// </summary>
        public bool ForceDelay { get; set; }
    }
}

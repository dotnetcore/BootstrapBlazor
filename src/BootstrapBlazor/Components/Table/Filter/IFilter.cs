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
    /// 过滤器接口
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 显示过滤窗口方法
        /// </summary>
        /// <returns></returns>
        void Show();

        /// <summary>
        /// 获得/设置 本过滤器相关 IFilterAction 实例
        /// </summary>
        IFilterAction? FilterAction { get; set; }
    }
}

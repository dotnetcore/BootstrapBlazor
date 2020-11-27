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
    /// Tag 组件类
    /// </summary>
    public partial class Tag
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected override string? ClassName => CssBuilder.Default("tag alert fade show")
            .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("is-close", ShowDismiss)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();
    }
}
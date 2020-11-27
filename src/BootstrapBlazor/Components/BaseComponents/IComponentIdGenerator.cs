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
    /// 组件 ID 生成器接口
    /// </summary>
    public interface IComponentIdGenerator
    {
        /// <summary>
        /// 生成组件 Id 方法
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        string Generate(ComponentBase component);
    }
}

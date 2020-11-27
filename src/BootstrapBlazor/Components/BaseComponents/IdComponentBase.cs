// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 自动生成客户端 ID 组件基类
    /// </summary>
    public abstract class IdComponentBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 组件 id 属性
        /// </summary>
        [Parameter]
        public virtual string? Id { get; set; }

        [Inject]
        [NotNull]
        private IComponentIdGenerator? ComponentIdGenerator { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Id ??= ComponentIdGenerator.Generate(this);
        }
    }
}

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
    /// Light 组件基类
    /// </summary>
    public abstract class LightBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("light")
            .AddClass("flash", IsFlash)
            .AddClass($"light-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();

        /// <summary>
        /// 获得/设置 组件是否闪烁 默认为 false 不闪烁
        /// </summary>
        [Parameter]
        public bool IsFlash { get; set; }

        /// <summary>
        /// 获得/设置 指示灯 Tooltip 显示文字
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 指示灯颜色 默认为 Success 绿色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Success;
    }
}

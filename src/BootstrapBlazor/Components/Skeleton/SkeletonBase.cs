using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 骨架屏组件基类
    /// </summary>
    public abstract class SkeletonBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 是否圆角 默认为 true
        /// </summary>
        [Parameter]
        public bool Round { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示动画 默认为 true
        /// </summary>
        [Parameter]
        public bool Active { get; set; } = true;

        /// <summary>
        /// 获得 样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("skeleton-content")
            .AddClass("active", Active)
            .AddClass("round", Round)
            .Build();
    }
}

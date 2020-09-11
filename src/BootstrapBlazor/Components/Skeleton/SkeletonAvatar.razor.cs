using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SkeletonAvatar
    {
        private string? AvatarClassString => CssBuilder.Default("skeleton-avatar")
            .AddClass("circle", Circle)
            .Build();

        /// <summary>
        /// 获得/设置 是否为圆形 默认为 false
        /// </summary>
        [Parameter]
        public bool Circle { get; set; }
    }
}

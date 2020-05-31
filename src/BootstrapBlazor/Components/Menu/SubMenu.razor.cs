using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SubMenu
    {
        /// <summary>
        /// 
        /// </summary>
        private string? ClassString => CssBuilder.Default("has-leaf")
            .AddClass("active", Item?.IsActive ?? false)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 组件数据源
        /// </summary>
        [Parameter]
        public MenuItem? Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        private string? GetClassString(string className, bool active) => CssBuilder.Default(className)
            .AddClass("active", active)
            .Build();

    }
}

using Microsoft.AspNetCore.Components;

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
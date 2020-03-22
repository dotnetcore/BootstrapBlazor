using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Size 枚举类型
    /// </summary>
    public enum Size
    {
        /// <summary>
        /// 无设置
        /// </summary>
        None,

        /// <summary>
        /// xs 超小设置
        /// </summary>
        [Description("xs")]
        ExtraSmall,

        /// <summary>
        /// xs 小设置
        /// </summary>
        [Description("sm")]
        Small,

        /// <summary>
        /// xs 中等设置
        /// </summary>
        [Description("md")]
        Medium,

        /// <summary>
        /// xs 大设置
        /// </summary>
        [Description("lg")]
        Large,

        /// <summary>
        /// xs 超大设置
        /// </summary>
        [Description("xl")]
        ExtraLarge
    }
}

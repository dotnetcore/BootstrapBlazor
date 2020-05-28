using System.ComponentModel;

namespace BootstrapBlazor.Shared.Common
{
    /// <summary>
    /// 属性说明类
    /// </summary>
    public class AttributeItem
    {
        /// <summary>
        /// 获得/设置 参数
        /// </summary>
        [DisplayName("参数")]
        public string Name { get; set; } = "";

        /// <summary>
        /// 获得/设置 说明
        /// </summary>
        [DisplayName("说明")]
        public string Description { get; set; } = "";

        /// <summary>
        /// 获得/设置 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; set; } = "";

        /// <summary>
        /// 获得/设置 可选值
        /// </summary>
        [DisplayName("可选值")]
        public string ValueList { get; set; } = "";

        /// <summary>
        /// 获得/设置 默认值
        /// </summary>
        [DisplayName("默认值")]
        public string DefaultValue { get; set; } = "";
    }
}

using System.ComponentModel;

namespace BootstrapBlazor.WebConsole.Common
{
    /// <summary>
    /// 方法说明类
    /// </summary>
    public class MethodItem
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
        /// 参数
        /// </summary>
        [DisplayName("参数")]
        public string Parameters { get; set; } = "";

        /// <summary>
        /// 返回值
        /// </summary>
        [DisplayName("返回值")]
        public string ReturnValue { get; set; } = "";
    }
}

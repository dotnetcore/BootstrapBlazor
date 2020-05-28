using System.ComponentModel;

namespace BootstrapBlazor.Shared.Common
{
    /// <summary>
    /// 方法说明类
    /// </summary>
    public class MethodItem : EventItem
    {
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

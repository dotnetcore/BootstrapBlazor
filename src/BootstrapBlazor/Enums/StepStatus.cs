using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 步骤状态枚举
    /// </summary>
    public enum StepStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        [Description("wait")]
        Wait,

        /// <summary>
        /// 进行中
        /// </summary>
        [Description("process")]
        Process,

        /// <summary>
        /// 
        /// </summary>
        [Description("finish")]
        Finish,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("success")]
        Success,

        /// <summary>
        /// 
        /// </summary>
        [Description("error")]
        Error,
    }
}

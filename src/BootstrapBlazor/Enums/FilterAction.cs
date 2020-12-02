// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 关系运算符
    /// </summary>
    public enum FilterAction
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal,

        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NotEqual,

        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        GreaterThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        [Description("大于等于")]
        GreaterThanOrEqual,

        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        LessThan,

        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("小于等于")]
        LessThanOrEqual,

        /// <summary>
        /// 包含
        /// </summary>
        [Description("包含")]
        Contains,

        /// <summary>
        /// 不包含
        /// </summary>
        [Description("不包含")]
        NotContains,

        /// <summary>
        /// 自定义条件
        /// </summary>
        [Description("自定义条件")]
        CustomPredicate
    }
}

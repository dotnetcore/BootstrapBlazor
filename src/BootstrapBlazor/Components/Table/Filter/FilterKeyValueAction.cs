// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Filter 过滤条件项目
    /// </summary>
    public class FilterKeyValueAction
    {
        /// <summary>
        /// 获得/设置 Filter 项字段名称
        /// </summary>
        public string? FieldKey { get; set; }

        /// <summary>
        /// 获得/设置 Filter 项字段值
        /// </summary>
        public object? FieldValue { get; set; }

        /// <summary>
        /// 获得/设置 Filter 项与其他 Filter 逻辑关系
        /// </summary>
        public FilterLogic FilterLogic { get; set; }

        /// <summary>
        /// 获得/设置 Filter 条件行为
        /// </summary>
        public FilterAction FilterAction { get; set; }
    }
}

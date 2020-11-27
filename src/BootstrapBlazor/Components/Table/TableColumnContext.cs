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
    /// TableColumn 上下文类
    /// </summary>
    public class TableColumnContext<TModel, TValue>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="value"></param>
        public TableColumnContext(TModel model, TValue value)
        {
            Row = model;
            Value = value;
        }

        /// <summary>
        /// 获得/设置 行数据实例
        /// </summary>
        public TModel Row { get; set; }

        /// <summary>
        /// 获得/设置 当前绑定字段数据实例
        /// </summary>
        public TValue Value { get; set; }
    }
}

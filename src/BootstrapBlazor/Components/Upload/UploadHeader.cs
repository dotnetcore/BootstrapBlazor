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
    /// Upload 组件 Http 请求头类
    /// </summary>
    public class UploadHeader
    {
        /// <summary>
        /// 获得 请求头名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 获得 请求头值
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public UploadHeader(string name, string value) => (Name, Value) = (name, value);
    }
}

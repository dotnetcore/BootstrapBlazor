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

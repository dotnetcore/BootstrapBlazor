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
    /// 上传组件返回类
    /// </summary>
    public class UploadFile
    {
        /// <summary>
        /// 获得/设置 文件名
        /// </summary>
        public string FileName { get; set; } = "";

        /// <summary>
        /// 获得/设置 原始文件名
        /// </summary>
        public string OriginFileName { get; set; } = "";

        /// <summary>
        /// 获得/设置 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 获得/设置 文件上传结果 0 表示成功 非零表示失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 获得/设置 文件预览地址
        /// </summary>
        public string PrevUrl { get; set; } = "";

        /// <summary>
        /// 获得/设置 错误信息
        /// </summary>
        public string? Error { get; set; }
    }
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

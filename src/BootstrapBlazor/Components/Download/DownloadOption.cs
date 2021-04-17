// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    public class DownloadOption
    {
        /// <summary>
        /// 获取/设置 要下载的文件数组
        /// </summary>
        [NotNull]
        public byte[]? File { get; set; }

        /// <summary>
        /// 获取/设置 要下载的文件名
        /// </summary>
        [NotNull]
        public string? FileName { get; set; }

        /// <summary>
        /// 获取/设置 要下载的文件MIME，默认application/octet-stream
        /// </summary>
        public string Mime { get; set; } = "application/octet-stream";
    }
}

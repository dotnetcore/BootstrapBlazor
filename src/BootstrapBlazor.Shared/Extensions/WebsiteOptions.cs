// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class WebsiteOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string ServerUrl { get; set; } = "https://www.blazor.zone";

        /// <summary>
        /// 
        /// </summary>
        public string WasmUrl { get; set; } = "https://wasm.blazor.zone";

        /// <summary>
        /// 
        /// </summary>
        public string AdminUrl { get; set; } = "https://admin.blazor.zone";

        /// <summary>
        /// 
        /// </summary>
        public string ImageLibUrl { get; set; } = "https://imgs.blazor.zone";

        /// <summary>
        /// 获得/设置 系统 wwwroot 文件夹路径 Server Side 模式下 Upload 使用
        /// </summary>
        [NotNull]
        public string? WebRootPath { get; set; }
    }
}

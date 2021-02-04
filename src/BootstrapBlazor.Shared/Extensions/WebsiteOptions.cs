// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
        /// 
        /// </summary>
        public string VideoLibUrl { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor/wikis/%E8%A7%86%E9%A2%91%E8%B5%84%E6%BA%90?sort_id=3300624";

        /// <summary>
        /// 
        /// </summary>
        public string RepositoryUrl { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor/raw/dev/src/BootstrapBlazor.Shared/Pages/Samples/";

        /// <summary>
        /// 获得/设置 系统 wwwroot 文件夹路径 Server Side 模式下 Upload 使用
        /// </summary>
        [NotNull]
        public string? WebRootPath { get; set; }

        /// <summary>
        /// 获得/设置 视频地址
        /// </summary>
        public string VideoUrl { get; set; } = "https://www.bilibili.com/video/";

        /// <summary>
        /// 获得/设置 资源配置集合
        /// </summary>
        [NotNull]
        public Dictionary<string, string> SourceCodes { get; set; }

        /// <summary>
        /// 获得/设置 资源配置集合
        /// </summary>
        [NotNull]
        public Dictionary<string, string>? Videos { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WebsiteOptions()
        {
            using var res = GetType().Assembly.GetManifestResourceStream($"{GetType().Assembly.GetName().Name}.docs.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(res)
                .Build();
            SourceCodes = config.GetSection("src").GetChildren().SelectMany(c => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(c.Key, c.Value) }).ToDictionary(item => item.Key, item => item.Value);
            Videos = config.GetSection("video").GetChildren().SelectMany(c => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(c.Key, c.Value) }).ToDictionary(item => item.Key, item => item.Value);
        }
    }
}

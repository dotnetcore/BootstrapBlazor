// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IP定位器配置项
    /// </summary>
    public class IPLocatorOption
    {
        /// <summary>
        /// 获得/设置 定位器名称
        /// </summary>
        public string LocatorName { get; set; } = nameof(BaiDuIPLocator);

        /// <summary>
        /// 获得/设置 定位器创建方法未设置使用内部定位器
        /// </summary>
        public Func<string, IIPLocator> LocatorFactory { get; set; } = _ => new BaiDuIPLocator();

        /// <summary>
        /// 获得/设置 IP定位器请求地址
        /// </summary>
        public string Url { get; set; } = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query={0}";

        /// <summary>
        /// 获得/设置 IP地址请求超时时间 默认为 3000 毫秒
        /// </summary>
        public int RequestTimeout { get; set; } = 3000;

        /// <summary>
        /// 获得 本机地址列表
        /// </summary>
        public List<string> Localhosts { get; } = new List<string>(new string[] { "::1", "127.0.0.1" });

        /// <summary>
        /// 获得/设置 IP地址
        /// </summary>
        internal string? IP { get; set; }

        /// <summary>
        /// 获得/设置 HttpClient 实体类
        /// </summary>
        internal HttpClient? HttpClient { get; set; }

        /// <summary>
        /// 获得/设置 ILogger 实体类
        /// </summary>
        internal ILogger<IIPLocatorProvider>? Logger { get; set; }
    }
}

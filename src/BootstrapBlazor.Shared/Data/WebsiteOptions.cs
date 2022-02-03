// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;

namespace BootstrapBlazor.Shared;

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
    public string BootstrapAdminLink { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapAdmin";

    /// <summary>
    /// 
    /// </summary>
    public string BootstrapBlazorLink { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor";

    /// <summary>
    /// 
    /// </summary>
    public string VideoLibUrl { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor/wikis/%E8%A7%86%E9%A2%91%E8%B5%84%E6%BA%90?sort_id=3300624";

    /// <summary>
    /// 
    /// </summary>
    public string RepositoryUrl { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor/raw/main/src/BootstrapBlazor.Shared/Samples/";

    /// <summary>
    /// 
    /// </summary>
    public string WikiUrl { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor/wikis/%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97?sort_id=4062034";

    /// <summary>
    /// 获得 QQ 1 群链接地址
    /// </summary>
    public string? QQGroup1Link { get; set; } = "https://qm.qq.com/cgi-bin/qm/qr?k=Geker7hCXK0HC-J8_974645j_n6w0OE0&jump_from=webapi";

    /// <summary>
    /// 获得 QQ 2 群链接地址
    /// </summary>
    public string? QQGroup2Link { get; set; } = "https://qm.qq.com/cgi-bin/qm/qr?k=Geker7hCXK0HC-J8_974645j_n6w0OE0&jump_from=webapi";

    /// <summary>
    /// 获得/设置 系统 wwwroot 文件夹路径 Server Side 模式下 Upload 使用
    /// </summary>
    [NotNull]
    public string? WebRootPath { get; set; }

    /// <summary>
    /// 获得/设置 当前程序文件夹
    /// </summary>
    [NotNull]
    public string? ContentRootPath { get; set; }

    /// <summary>
    /// 获得/设置 视频地址
    /// </summary>
    public string VideoUrl { get; set; } = "https://www.bilibili.com/video/";

    /// <summary>
    /// 获得/设置 资源配置集合
    /// </summary>
    [NotNull]
    public Dictionary<string, string> SourceCodes { get; private set; }

    /// <summary>
    /// 获得/设置 资源配置集合
    /// </summary>
    [NotNull]
    public Dictionary<string, string>? Videos { get; private set; }

    /// <summary>
    /// 获得/设置 当前主题
    /// </summary>
    public string CurrentTheme { get; set; } = "";

    /// <summary>
    /// 获得/设置 组件总数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 获得 当前环境配置
    /// </summary>
    public bool IsDevelopment { get; set; }

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

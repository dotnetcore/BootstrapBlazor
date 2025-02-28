// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Frozen;

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// WebsiteOptions 网站配置类
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
    public string AdminUrl { get; set; } = "https://admin.blazor.zone";

    /// <summary>
    /// 
    /// </summary>
    public string AdminProUrl { get; set; } = "https://pro.blazor.zone";

    /// <summary>
    /// 
    /// </summary>
    public string BootstrapAdminLink { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapAdmin";

    /// <summary>
    /// 
    /// </summary>
    public string GiteeRepositoryUrl { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor";

    /// <summary>
    /// 
    /// </summary>
    public string VideoLibUrl { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor/wikis/%E8%A7%86%E9%A2%91%E8%B5%84%E6%BA%90?sort_id=3300624";

    /// <summary>
    /// 仓库源码地址
    /// </summary>
    public string SourceUrl { get; set; } = "https://gitee.com/LongbowEnterprise/BootstrapBlazor/raw/main/src/";

    /// <summary>
    /// 源码地址
    /// </summary>
    public string SourceCodePath { get; set; } = "/root/BootstrapBlazor/src/";

    /// <summary>
    /// Github 仓库地址
    /// </summary>
    public string GithubRepositoryUrl { get; set; } = "https://github.com/dotnetcore/BootstrapBlazor?wt.mc_id=DT-MVP-5004174";

    /// <summary>
    /// 
    /// </summary>
    public string WikiUrl { get; set; } = "https://github.com/dotnetcore/BootstrapBlazor/releases?wt.mc_id=DT-MVP-5004174";

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
    /// 获得/设置 资源文件根目录 默认值为 "./"
    /// </summary>
    [NotNull]
    public string? AssetRootPath { get; set; } = "./";

    /// <summary>
    /// 获得/设置 脚本根路径
    /// </summary>
    [NotNull]
    public string JSModuleRootPath { get; set; } = "./Components/";

    /// <summary>
    /// 获得/设置 视频地址
    /// </summary>
    public string VideoUrl { get; set; } = "https://www.bilibili.com/video/";

    /// <summary>
    /// 获得/设置 资源配置集合
    /// </summary>
    public FrozenDictionary<string, string?> SourceCodes { get; private set; }

    /// <summary>
    /// 获得/设置 资源配置集合
    /// </summary>
    public FrozenDictionary<string, string?> Videos { get; private set; }

    /// <summary>
    /// 获得/设置 当前主题
    /// </summary>
    public string CurrentTheme { get; set; } = "";

    /// <summary>
    /// 获得/设置 组件总数
    /// </summary>
    public int TotalCount { get; set; } = 160;

    /// <summary>
    /// 获得 当前环境配置
    /// </summary>
    public bool IsDevelopment { get; set; }

    /// <summary>
    /// 获得/设置 当前网站友联集合
    /// </summary>
    public FrozenDictionary<string, string?> Links { get; set; }

    /// <summary>
    /// 获得/设置 网站主题配置集合
    /// </summary>
    [NotNull]
    public HashSet<ThemeOption>? Themes { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public WebsiteOptions()
    {
        var config = GetConfiguration("docs.json");
        SourceCodes = config.GetSection("src").GetChildren().Select(c => new KeyValuePair<string, string?>(c.Key, c.Value)).ToFrozenDictionary(item => item.Key, item => item.Value);
        Videos = config.GetSection("video").GetChildren().Select(c => new KeyValuePair<string, string?>(c.Key, c.Value)).ToFrozenDictionary(item => item.Key, item => item.Value);
        Links = config.GetSection("link").GetChildren().Select(c => new KeyValuePair<string, string?>(c.Key, c.Value)).ToFrozenDictionary(item => item.Key, item => item.Value);

#if DEBUG
        IsDevelopment = true;
#endif
        ContentRootPath = IsDevelopment ? Path.Combine(AppContext.BaseDirectory, "../../../") : AppContext.BaseDirectory;
        WebRootPath = Path.Combine(ContentRootPath, "wwwroot");
    }

    private IConfiguration GetConfiguration(string jsonFileName)
    {
        var assembly = GetType().Assembly;
        var assemblyName = assembly.GetName().Name;
        using var res = assembly.GetManifestResourceStream($"{assemblyName}.{jsonFileName}") ?? throw new InvalidOperationException();

        return new ConfigurationBuilder()
            .AddJsonStream(res)
            .Build();
    }

    /// <summary>
    /// 拼接静态资源文件路径
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public string? GetAssetUrl(string url) => $"{AssetRootPath}{url}";

    /// <summary>
    /// 获得头像地址字符串
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetAvatarUrl(int id) => $"{AssetRootPath}images/avatars/150-{Math.Max(1, id % 25)}.jpg";
}

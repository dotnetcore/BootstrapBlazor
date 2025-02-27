// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax配置类
/// </summary>
public class AjaxOption
{
    /// <summary>
    /// 获取/设置 要上传的参数类
    /// </summary>
    [NotNull]
    public object? Data { get; set; }

    /// <summary>
    /// 获取/设置 传输方式，默认为POST
    /// </summary>
    public string Method { get; set; } = "POST";

    /// <summary>
    /// 获取/设置 请求的URL
    /// </summary>
    [NotNull]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 是否获得序列化 Json 结果 参数 默认为 true
    /// </summary>
    [NotNull]
    public bool ToJson { get; set; } = true;
}

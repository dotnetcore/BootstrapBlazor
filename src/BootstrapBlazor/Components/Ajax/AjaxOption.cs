// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Ajax配置类</para>
/// <para lang="en">Ajax configuration class</para>
/// </summary>
public class AjaxOption
{
    /// <summary>
    /// <para lang="zh">获取/设置 要上传的参数类</para>
    /// <para lang="en">Gets or sets the parameter object to upload</para>
    /// </summary>
    [NotNull]
    public object? Data { get; set; }

    /// <summary>
    /// <para lang="zh">获取/设置 传输方式，默认为POST</para>
    /// <para lang="en">Gets or sets the transfer method. Default is POST</para>
    /// </summary>
    public string Method { get; set; } = "POST";

    /// <summary>
    /// <para lang="zh">获取/设置 请求的URL</para>
    /// <para lang="en">Gets or sets the request URL</para>
    /// </summary>
    [NotNull]
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否获得序列化 Json 结果 参数 默认为 true</para>
    /// <para lang="en">Gets or sets whether to get the serialized Json result. Default is true</para>
    /// </summary>
    [NotNull]
    public bool ToJson { get; set; } = true;
}

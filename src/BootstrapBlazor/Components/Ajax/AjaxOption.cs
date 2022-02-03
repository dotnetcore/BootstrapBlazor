// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
}

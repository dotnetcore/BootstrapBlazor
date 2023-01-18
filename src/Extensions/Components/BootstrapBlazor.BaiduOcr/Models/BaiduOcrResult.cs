// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 百度文字识别返回类
/// </summary>
public class BaiduOcrResult<TEntity>
{
    /// <summary>
    /// 获得/设置 错误码
    /// </summary>
    public int ErrorCode { get; set; }

    /// <summary>
    /// 获得/设置 错误描述信息
    /// </summary>
    /// <remarks>https://ai.baidu.com/ai-doc/OCR/dk3h7y5vr</remarks>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 获得/设置 返回实例
    /// </summary>
    public TEntity? Entity { get; set; }
}

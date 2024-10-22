// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音合成状态枚举
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RecognizerStatus
{
    /// <summary>
    /// 正在识别
    /// </summary>
    Start,

    /// <summary>
    /// 识别完毕
    /// </summary>
    Finished,

    /// <summary>
    /// 关闭
    /// </summary>
    Close,

    /// <summary>
    /// 出错
    /// </summary>
    Error
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音合成状态枚举
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SynthesizerStatus
{
    /// <summary>
    /// 正在合成
    /// </summary>
    Synthesizer,

    /// <summary>
    /// 朗读完毕
    /// </summary>
    Finished,

    /// <summary>
    /// 取消
    /// </summary>
    Cancel,

    /// <summary>
    /// 关闭
    /// </summary>
    Close,

    /// <summary>
    /// 出错
    /// </summary>
    Error
}

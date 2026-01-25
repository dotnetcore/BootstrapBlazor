// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">语音合成状态枚举</para>
/// <para lang="en">语音合成状态enum</para>
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SynthesizerStatus
{
    /// <summary>
    /// <para lang="zh">正在合成</para>
    /// <para lang="en">正在合成</para>
    /// </summary>
    Synthesizer,

    /// <summary>
    /// <para lang="zh">朗读完毕</para>
    /// <para lang="en">朗读完毕</para>
    /// </summary>
    Finished,

    /// <summary>
    /// <para lang="zh">取消</para>
    /// <para lang="en">取消</para>
    /// </summary>
    Cancel,

    /// <summary>
    /// <para lang="zh">出错</para>
    /// <para lang="en">出错</para>
    /// </summary>
    Error
}

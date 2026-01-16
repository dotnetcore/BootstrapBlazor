// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">语音合成状态枚举</para>
///  <para lang="en">语音合成状态enum</para>
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RecognizerStatus
{
    /// <summary>
    ///  <para lang="zh">正在识别</para>
    ///  <para lang="en">正在识别</para>
    /// </summary>
    Start,

    /// <summary>
    ///  <para lang="zh">识别完毕</para>
    ///  <para lang="en">识别完毕</para>
    /// </summary>
    Finished,

    /// <summary>
    ///  <para lang="zh">关闭</para>
    ///  <para lang="en">关闭</para>
    /// </summary>
    Close,

    /// <summary>
    ///  <para lang="zh">出错</para>
    ///  <para lang="en">出错</para>
    /// </summary>
    Error
}

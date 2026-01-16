// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">上传组件模式枚举类型
///</para>
/// <para lang="en">上传component模式enumtype
///</para>
/// </summary>
public enum UploadStyle
{
    /// <summary>
    /// <para lang="zh">正常模式
    ///</para>
    /// <para lang="en">正常模式
    ///</para>
    /// </summary>
    Normal,

    /// <summary>
    /// <para lang="zh">点击上传
    ///</para>
    /// <para lang="en">点击上传
    ///</para>
    /// </summary>
    ClickToUpload,

    /// <summary>
    /// <para lang="zh">上传头像模式
    ///</para>
    /// <para lang="en">上传头像模式
    ///</para>
    /// </summary>
    Avatar,

    /// <summary>
    /// <para lang="zh">预览卡片模式
    ///</para>
    /// <para lang="en">预览卡片模式
    ///</para>
    /// </summary>
    Card
}

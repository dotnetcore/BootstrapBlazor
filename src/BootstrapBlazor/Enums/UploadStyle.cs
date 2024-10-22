// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 上传组件模式枚举类型
/// </summary>
public enum UploadStyle
{
    /// <summary>
    /// 正常模式
    /// </summary>
    Normal,

    /// <summary>
    /// 点击上传
    /// </summary>
    ClickToUpload,

    /// <summary>
    /// 上传头像模式
    /// </summary>
    Avatar,

    /// <summary>
    /// 预览卡片模式
    /// </summary>
    Card
}

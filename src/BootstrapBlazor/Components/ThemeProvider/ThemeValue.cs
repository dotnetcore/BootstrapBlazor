// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Core.Converter;

namespace BootstrapBlazor.Components;

/// <summary>
/// 主题选项
/// </summary>
[JsonEnumConverter(true)]
public enum ThemeValue
{
    /// <summary>
    /// 自动
    /// </summary>
    Auto,

    /// <summary>
    /// 明亮主题
    /// </summary>
    Light,

    /// <summary>
    /// 暗黑主题
    /// </summary>
    Dark,

    /// <summary>
    /// 使用本地保存选项
    /// </summary>
    UseLocalStorage,
}

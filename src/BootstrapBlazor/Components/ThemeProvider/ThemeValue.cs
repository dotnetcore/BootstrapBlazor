// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Icon 主题服务
/// </summary>
public interface IIconTheme
{
    /// <summary>
    /// 获得所有图标
    /// </summary>
    /// <returns></returns>
    Dictionary<ComponentIcons, string> GetIcons();
}

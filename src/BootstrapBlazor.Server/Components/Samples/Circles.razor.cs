// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Services;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Circles
/// </summary>
public sealed partial class Circles
{
    private int _circleValue = 0;

    private void Add(int interval)
    {
        _circleValue += interval;
        _circleValue = Math.Min(100, Math.Max(0, _circleValue));
    }

    /// <summary>
    /// GetAttributes - 使用反射和缓存自动获取 Circle 组件属性
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes()
    {
        // 通过示例组件名称 Circles 确定组件类型为 Circle
        return ComponentAttributeCacheService.GetAttributes(typeof(BootstrapBlazor.Components.Circle));
    }
}


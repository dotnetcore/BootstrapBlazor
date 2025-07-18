﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Socket 数据转换器接口
/// </summary>
public interface ISocketDataPropertyConverter
{
    /// <summary>
    /// 将数据转换为指定类型的对象
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    object? Convert(ReadOnlyMemory<byte> data);
}

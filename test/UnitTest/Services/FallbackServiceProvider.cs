// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Services;

/// <summary>
/// 
/// </summary>
public class FallbackServiceProvider : IServiceProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public object? GetService(Type serviceType)
    {
        return null;
    }
}

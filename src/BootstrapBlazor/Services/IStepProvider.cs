// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IStepProvider 接口
/// </summary>
public interface IStepProvider
{
    /// <summary>
    /// 通过数据类型获得当前步长
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    string? GetStep(Type type);

    /// <summary>
    /// 设置指定数据类型步长数值
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    void SetStep(Type type, string? value);
}

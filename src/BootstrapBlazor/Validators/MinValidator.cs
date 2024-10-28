// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 选项最小数验证实现类
/// </summary>
public class MinValidator : MaxValidator
{
    /// <summary>
    /// 验证方法 大于等于 Value 时 返回 true
    /// </summary>
    protected override bool Validate(int count) => count >= Value;

    /// <summary>
    /// 获得 ErrorMessage 方法
    /// </summary>
    /// <returns></returns>
    protected override string GetErrorMessage() => ErrorMessage ?? "Select at least {0} items";
}

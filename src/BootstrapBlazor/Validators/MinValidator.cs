// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">选项最小数验证实现类</para>
///  <para lang="en">Min validator implementation class</para>
/// </summary>
public class MinValidator : MaxValidator
{
    /// <summary>
    ///  <para lang="zh">验证方法 大于等于 Value 时 返回 true</para>
    ///  <para lang="en">Validation method return true when greater than or equal to Value</para>
    /// </summary>
    protected override bool Validate(int count) => count >= Value;

    /// <summary>
    ///  <para lang="zh">获得 ErrorMessage 方法</para>
    ///  <para lang="en">Get ErrorMessage method</para>
    /// </summary>
    /// <returns></returns>
    protected override string GetErrorMessage() => ErrorMessage ?? "Select at least {0} items";
}

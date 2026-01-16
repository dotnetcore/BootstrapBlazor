// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapInputNumber 基类</para>
/// <para lang="en">BootstrapInputNumber Base Class</para>
/// </summary>
public class BootstrapInputNumberBase<TValue> : BootstrapInputEventBase<TValue>
{
    /// <summary>
    /// <para lang="zh">SetParametersAsync 方法</para>
    /// <para lang="en">SetParametersAsync Method</para>
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        // Unwrap Nullable<T>, because InputBase already deals with the Nullable aspect
        // of it for us. We will only get asked to parse the T for nonempty inputs.
        var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        if (!typeof(TValue).IsNumber())
        {
            throw new InvalidOperationException($"The type '{targetType}' is not a supported numeric type.");
        }

        return base.SetParametersAsync(parameters);
    }
}

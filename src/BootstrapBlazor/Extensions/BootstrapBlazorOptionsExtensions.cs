// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapBlazorOptions configuration class extension methods
///</para>
/// <para lang="en">BootstrapBlazorOptions configuration class extension methods
///</para>
/// </summary>
public static class BootstrapBlazorOptionsExtensions
{
    /// <summary>
    /// <para lang="zh">Get step size generic method
    ///</para>
    /// <para lang="en">Get step size generic method
    ///</para>
    /// </summary>
    /// <typeparam name="TType">The type parameter</typeparam>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    /// <returns>The step size as a string</returns>
    public static string? GetStep<TType>(this BootstrapBlazorOptions options) => options.GetStep(typeof(TType));

    /// <summary>
    /// <para lang="zh">Get step size method
    ///</para>
    /// <para lang="en">Get step size method
    ///</para>
    /// </summary>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    /// <param name="type">The data type</param>
    /// <returns>The step size as a string</returns>
    public static string? GetStep(this BootstrapBlazorOptions options, Type type)
    {
        var t = Nullable.GetUnderlyingType(type) ?? type;
        return options.StepSettings.GetStep(t);
    }

    /// <summary>
    /// <para lang="zh">Get Modal IsFade value
    ///</para>
    /// <para lang="en">Get Modal IsFade value
    ///</para>
    /// </summary>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    /// <param name="value">The default value</param>
    /// <returns>The IsFade value as a boolean</returns>
    public static bool GetIsFadeValue(this BootstrapBlazorOptions options, bool? value) => value ?? options.ModalSettings.IsFade ?? true;
}

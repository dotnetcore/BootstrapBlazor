// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorOptions configuration class extension methods
/// </summary>
public static class BootstrapBlazorOptionsExtensions
{
    /// <summary>
    /// Get step size generic method
    /// </summary>
    /// <typeparam name="TType">The type parameter</typeparam>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    /// <returns>The step size as a string</returns>
    public static string? GetStep<TType>(this BootstrapBlazorOptions options) => options.GetStep(typeof(TType));

    /// <summary>
    /// Get step size method
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
    /// Get Modal IsFade value
    /// </summary>
    /// <param name="options">The BootstrapBlazorOptions instance</param>
    /// <param name="value">The default value</param>
    /// <returns>The IsFade value as a boolean</returns>
    public static bool GetIsFadeValue(this BootstrapBlazorOptions options, bool? value) => value ?? options.ModalSettings.IsFade ?? true;
}
